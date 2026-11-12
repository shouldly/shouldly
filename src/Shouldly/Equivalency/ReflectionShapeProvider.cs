using System.Collections.Concurrent;

namespace Shouldly.Equivalency;

/// <summary>
/// The in-box, reflection-based shape provider. All reflection over user types is contained
/// here; the engine itself only consumes <see cref="EquivalencyTypeShape"/>.
/// </summary>
internal sealed class ReflectionShapeProvider : IEquivalencyShapeProvider
{
    public static readonly ReflectionShapeProvider Instance = new();

    private readonly ConcurrentDictionary<Type, EquivalencyTypeShape> _cache = new();

    private ReflectionShapeProvider()
    {
    }

    [UnconditionalSuppressMessage("Trimming", "IL2026",
        Justification = "This provider is only constructed while the Shouldly.Equivalency.IsReflectionEnabledByDefault feature switch is on. " +
                        "Trimmed publishes that disable the switch remove it entirely; see EquivalencyShapes.")]
    public EquivalencyTypeShape GetShape(Type type, Type? declaredType = null) =>
        _cache.GetOrAdd(type, static t => CreateShape(t));

    [RequiresUnreferencedCode("Reflects over the fields, properties, and interfaces of arbitrary types.")]
    private static EquivalencyTypeShape CreateShape(Type type)
    {
        var kind = GetKind(type);

        Type? elementType = null;
        Type? keyType = null;
        Type? valueType = null;
        PropertyInfo? kvpKeyProperty = null;
        PropertyInfo? kvpValueProperty = null;
        IReadOnlyList<EquivalencyMemberShape> members = [];
        IReadOnlyList<EquivalencyMemberShape> allMembers = [];

        switch (kind)
        {
            case EquivalencyNodeKind.Dictionary:
                var dictionaryInterface =
                    FindGenericInterface(type, typeof(IDictionary<,>)) ??
                    FindGenericInterface(type, typeof(IReadOnlyDictionary<,>));
                if (dictionaryInterface != null)
                {
                    var arguments = dictionaryInterface.GetGenericArguments();
                    keyType = arguments[0];
                    valueType = arguments[1];

                    // The already-constructed KeyValuePair<K,V> type comes from the enumerable
                    // interface, avoiding MakeGenericType (which is not AOT-safe).
                    var kvpType = FindGenericInterface(type, typeof(IEnumerable<>))?.GetGenericArguments()[0];
                    if (kvpType is { IsGenericType: true } && kvpType.GetGenericTypeDefinition() == typeof(KeyValuePair<,>))
                    {
                        kvpKeyProperty = kvpType.GetProperty(nameof(KeyValuePair<int, int>.Key));
                        kvpValueProperty = kvpType.GetProperty(nameof(KeyValuePair<int, int>.Value));
                    }
                }

                allMembers = CreateMembers(type);
                members = CreateCollectionMembers(type, allMembers);
                break;

            case EquivalencyNodeKind.Set:
            case EquivalencyNodeKind.Sequence:
                elementType = type.IsArray
                    ? type.GetElementType()
                    : FindGenericInterface(type, typeof(IEnumerable<>))?.GetGenericArguments()[0];
                allMembers = CreateMembers(type);
                members = CreateCollectionMembers(type, allMembers);
                break;

            case EquivalencyNodeKind.Complex:
                allMembers = members = CreateMembers(type);
                break;
        }

        return new ReflectionTypeShape(type, kind, members, allMembers, elementType, keyType, valueType, kvpKeyProperty, kvpValueProperty);
    }

    [RequiresUnreferencedCode("Reflects over the interfaces of arbitrary types.")]
    private static EquivalencyNodeKind GetKind(Type type)
    {
        if (IsLeaf(type))
            return EquivalencyNodeKind.Leaf;

        if (typeof(IDictionary).IsAssignableFrom(type)
            || FindGenericInterface(type, typeof(IDictionary<,>)) != null
            || FindGenericInterface(type, typeof(IReadOnlyDictionary<,>)) != null)
            return EquivalencyNodeKind.Dictionary;

        if (FindGenericInterface(type, typeof(ISet<>)) != null
            || FindGenericInterfaceByName(type, "System.Collections.Generic.IReadOnlySet`1") != null)
            return EquivalencyNodeKind.Set;

        if (typeof(IEnumerable).IsAssignableFrom(type))
            return EquivalencyNodeKind.Sequence;

        return EquivalencyNodeKind.Complex;
    }

    private static bool IsLeaf(Type type)
    {
        type = Nullable.GetUnderlyingType(type) ?? type;

        if (type.IsPrimitive || type.IsEnum)
            return true;

        if (type == typeof(string)
            || type == typeof(decimal)
            || type == typeof(Guid)
            || type == typeof(DateTime)
            || type == typeof(DateTimeOffset)
            || type == typeof(TimeSpan)
            || type == typeof(Uri)
            || type == typeof(Version))
            return true;

        // Reflection objects and delegates have well-defined identity semantics via Equals and
        // reflection-heavy object graphs; walking their members is never what the user wants.
        if (typeof(MemberInfo).IsAssignableFrom(type) || typeof(Delegate).IsAssignableFrom(type))
            return true;

        // Value-semantic System types that may not exist on every TFM Shouldly compiles against.
        return type.FullName is
            "System.DateOnly" or
            "System.TimeOnly" or
            "System.Half" or
            "System.Int128" or
            "System.UInt128" or
            "System.Text.Rune" or
            "System.Numerics.BigInteger";
    }

    [RequiresUnreferencedCode("Reflects over the interfaces of arbitrary types.")]
    private static Type? FindGenericInterface(Type type, Type genericDefinition)
    {
        if (type.IsInterface && type.IsGenericType && type.GetGenericTypeDefinition() == genericDefinition)
            return type;

        foreach (var candidate in type.GetInterfaces())
        {
            if (candidate.IsGenericType && candidate.GetGenericTypeDefinition() == genericDefinition)
                return candidate;
        }

        return null;
    }

    [RequiresUnreferencedCode("Reflects over the interfaces of arbitrary types.")]
    private static Type? FindGenericInterfaceByName(Type type, string genericDefinitionFullName)
    {
        if (type.IsInterface && type.IsGenericType && type.GetGenericTypeDefinition().FullName == genericDefinitionFullName)
            return type;

        foreach (var candidate in type.GetInterfaces())
        {
            if (candidate.IsGenericType && candidate.GetGenericTypeDefinition().FullName == genericDefinitionFullName)
                return candidate;
        }

        return null;
    }

    [RequiresUnreferencedCode("Reflects over the fields and properties of arbitrary types.")]
    private static IReadOnlyList<EquivalencyMemberShape> CreateMembers(Type type)
    {
        const BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance;

        // Interfaces don't inherit members through GetProperties; flatten the hierarchy.
        var sources = type.IsInterface
            ? new[] { type }.Concat(type.GetInterfaces())
            : [type];

        var members = new List<EquivalencyMemberShape>();
        var byName = new Dictionary<string, MemberInfo>(StringComparer.Ordinal);

        foreach (var source in sources)
        {
            foreach (var field in source.GetFields(bindingFlags))
                AddMember(field, field.DeclaringType);

            foreach (var property in source.GetProperties(bindingFlags))
            {
                if (property.GetIndexParameters().Length != 0 || !property.CanRead)
                    continue;

                AddMember(property, property.DeclaringType);
            }
        }

        return members;

        void AddMember(MemberInfo member, Type? declaringType)
        {
            if (byName.TryGetValue(member.Name, out var existing))
            {
                // A member can appear twice when shadowed with `new`; keep the most derived one.
                if (existing.DeclaringType != null && declaringType != null && existing.DeclaringType.IsAssignableFrom(declaringType))
                {
                    members[members.FindIndex(m => m.Name == member.Name)] = new ReflectionMemberShape(member);
                    byName[member.Name] = member;
                }

                return;
            }

            byName.Add(member.Name, member);
            members.Add(new ReflectionMemberShape(member));
        }
    }

    /// <summary>
    /// The state a collection type holds beyond its items (#1431): the members a type adds to the framework
    /// collection class it derives from, such as a <c>List&lt;T&gt;</c> subclass's total count, and a grouping's
    /// <c>Key</c>, which is state even though the framework declares it.
    /// <para>
    /// The framework base holds the items, so what a subclass adds is its own state. A member declared by a
    /// framework type (<c>List&lt;T&gt;.Capacity</c>, an array's <c>Length</c>) or named by a framework interface
    /// the type implements (<c>Count</c>, <c>SyncRoot</c>, <c>Keys</c>) is left out. A collection that implements
    /// the interfaces itself has no members here: nothing tells its state apart from how it holds its items or
    /// from views of them (a buffer's <c>Capacity</c>, a cons list's <c>Head</c> and <c>Tail</c>, a tree node's
    /// <c>Parent</c> and <c>Next</c>), and such getters can differ between equal collections, or throw.
    /// </para>
    /// </summary>
    [RequiresUnreferencedCode("Reflects over the interfaces of arbitrary types.")]
    private static IReadOnlyList<EquivalencyMemberShape> CreateCollectionMembers(Type type, IReadOnlyList<EquivalencyMemberShape> allMembers)
    {
        var isGrouping = FindGenericInterface(type, typeof(IGrouping<,>)) != null;
        var addsToFrameworkCollection = DerivesFromFrameworkCollection(type);
        if (!isGrouping && !addsToFrameworkCollection)
            return [];

        var interfaceMemberNames = new HashSet<string>(
            type.GetInterfaces()
                .Where(IsFrameworkType)
                .SelectMany(i => i.GetProperties().Select(p => p.Name)),
            StringComparer.Ordinal);

        return allMembers
            .Cast<ReflectionMemberShape>()
            .Where(member =>
                isGrouping && member.Name == nameof(IGrouping<int, int>.Key) ||
                addsToFrameworkCollection && !member.IsDeclaredByFrameworkType && !interfaceMemberNames.Contains(member.Name))
            .ToList<EquivalencyMemberShape>();
    }

    private static bool DerivesFromFrameworkCollection(Type type)
    {
        for (var baseType = type.BaseType; baseType != null; baseType = baseType.BaseType)
        {
            if (IsFrameworkType(baseType) && typeof(IEnumerable).IsAssignableFrom(baseType))
                return true;
        }

        return false;
    }

    /// <summary>A type that ships with .NET itself, as opposed to one from the user or a third-party library.</summary>
    private static bool IsFrameworkType(Type type) =>
        type.Assembly.GetName().Name is { } name
        && (name is "mscorlib" or "netstandard" or "System" || name.StartsWith("System.", StringComparison.Ordinal));

    private sealed class ReflectionTypeShape : EquivalencyTypeShape
    {
        private readonly IReadOnlyList<EquivalencyMemberShape> _members;
        private readonly IReadOnlyList<EquivalencyMemberShape> _allMembers;
        private readonly PropertyInfo? _kvpKeyProperty;
        private readonly PropertyInfo? _kvpValueProperty;

        public ReflectionTypeShape(
            Type type,
            EquivalencyNodeKind kind,
            IReadOnlyList<EquivalencyMemberShape> members,
            IReadOnlyList<EquivalencyMemberShape> allMembers,
            Type? elementType,
            Type? keyType,
            Type? valueType,
            PropertyInfo? kvpKeyProperty,
            PropertyInfo? kvpValueProperty)
        {
            Type = type;
            Kind = kind;
            _members = members;
            _allMembers = allMembers;
            ElementType = elementType;
            KeyType = keyType;
            ValueType = valueType;
            _kvpKeyProperty = kvpKeyProperty;
            _kvpValueProperty = kvpValueProperty;
        }

        public override Type Type { get; }

        public override EquivalencyNodeKind Kind { get; }

        public override IReadOnlyList<EquivalencyMemberShape> Members => _members;

        public override Type? ElementType { get; }

        public override Type? KeyType { get; }

        public override Type? ValueType { get; }

        public override EquivalencyMemberShape? FindMember(string name)
        {
            foreach (var member in _allMembers)
            {
                if (string.Equals(member.Name, name, StringComparison.Ordinal))
                    return member;
            }

            return null;
        }

        public override IEnumerable<KeyValuePair<object?, object?>> GetEntries(object instance)
        {
            if (instance is IDictionary dictionary)
            {
                foreach (DictionaryEntry entry in dictionary)
                    yield return new(entry.Key, entry.Value);

                yield break;
            }

            if (_kvpKeyProperty == null || _kvpValueProperty == null)
                throw new InvalidOperationException($"Cannot enumerate entries of dictionary type {Type.FullName}.");

            foreach (var pair in (IEnumerable)instance)
                yield return new(_kvpKeyProperty.GetValue(pair), _kvpValueProperty.GetValue(pair));
        }
    }

    private sealed class ReflectionMemberShape : EquivalencyMemberShape
    {
        private readonly MemberInfo _member;

        public ReflectionMemberShape(MemberInfo member) =>
            _member = member;

        public override string Name => _member.Name;

        public bool IsDeclaredByFrameworkType => _member.DeclaringType is { } declaringType && IsFrameworkType(declaringType);

        public override Type DeclaredType => _member switch
        {
            PropertyInfo property => property.PropertyType,
            _ => ((FieldInfo)_member).FieldType,
        };

        public override object? GetValue(object instance) => _member switch
        {
            PropertyInfo property => property.GetValue(instance),
            _ => ((FieldInfo)_member).GetValue(instance),
        };
    }
}
