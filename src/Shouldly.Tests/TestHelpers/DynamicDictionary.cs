using System.Linq.Expressions;
using System.Text;

namespace Shouldly.Tests.TestHelpers;

// From IDynamicMetaObjectProvider implementation example at https://learn.microsoft.com/en-us/dotnet/api/system.dynamic.dynamicmetaobject?view=net-9.0
class DynamicDictionary : IDynamicMetaObjectProvider
{
    public string Property1 { get; set; } = "property1";
    #region IDynamicMetaObjectProvider Members

    DynamicMetaObject IDynamicMetaObjectProvider.GetMetaObject(
        Expression parameter
    ) =>
        new DynamicDictionaryMetaObject(parameter, this);

    #endregion

    private class DynamicDictionaryMetaObject : DynamicMetaObject
    {
        internal DynamicDictionaryMetaObject(
            Expression parameter,
            DynamicDictionary value
        )
            : base(parameter, BindingRestrictions.Empty, value)
        {
        }

        public override DynamicMetaObject BindSetMember(
            SetMemberBinder binder,
            DynamicMetaObject value
        )
        {
            // Method to call in the containing class:
            var methodName = "SetDictionaryEntry";

            // setup the binding restrictions.
            var restrictions =
                BindingRestrictions.GetTypeRestriction(Expression, LimitType);

            // setup the parameters:
            var args = new Expression[2];
            // First parameter is the name of the property to Set
            args[0] = Expression.Constant(binder.Name);
            // Second parameter is the value
            args[1] = Expression.Convert(value.Expression, typeof(object));

            // Setup the 'this' reference
            Expression self = Expression.Convert(Expression, LimitType);

            // Setup the method call expression
            Expression methodCall = Expression.Call(self,
                typeof(DynamicDictionary).GetMethod(methodName)!,
                args
            );

            // Create a meta object to invoke Set later:
            var setDictionaryEntry = new DynamicMetaObject(
                methodCall,
                restrictions
            );
            // return that dynamic object
            return setDictionaryEntry;
        }

        public override DynamicMetaObject BindGetMember(GetMemberBinder binder)
        {
            // Method call in the containing class:
            var methodName = "GetDictionaryEntry";

            // One parameter
            var parameters = new Expression[]
            {
                Expression.Constant(binder.Name)
            };

            var getDictionaryEntry = new DynamicMetaObject(
                Expression.Call(
                    Expression.Convert(Expression, LimitType),
                    typeof(DynamicDictionary).GetMethod(methodName)!,
                    parameters
                ),
                BindingRestrictions.GetTypeRestriction(Expression, LimitType)
            );
            return getDictionaryEntry;
        }

        public override DynamicMetaObject BindInvokeMember(
            InvokeMemberBinder binder,
            DynamicMetaObject[] args
        )
        {
            var paramInfo = new StringBuilder();
            paramInfo.AppendFormat("Calling {0}(", binder.Name);
            foreach (var item in args)
                paramInfo.AppendFormat("{0}, ", item.Value);
            paramInfo.Append(")");

            var parameters = new Expression[]
            {
                Expression.Constant(paramInfo.ToString())
            };
            var methodInfo = new DynamicMetaObject(
                Expression.Call(
                    Expression.Convert(Expression, LimitType),
                    typeof(DynamicDictionary).GetMethod("WriteMethodInfo")!,
                    parameters
                ),
                BindingRestrictions.GetTypeRestriction(Expression, LimitType)
            );
            return methodInfo;
        }
    }

    private Dictionary<string, object> storage = new();

    public object SetDictionaryEntry(string key, object value)
    {
        if (!storage.TryAdd(key, value))
        {
            storage[key] = value;
        }
        return value;
    }

    public object GetDictionaryEntry(string key)
    {
        object result = null!;
        if (storage.ContainsKey(key))
        {
            result = storage[key];
        }

        return result;
    }

    public object WriteMethodInfo(string methodInfo)
    {
        Console.WriteLine(methodInfo);
        return 42; // because it is the answer to everything
    }

    public override string ToString()
    {
        var message = new StringWriter();
        foreach (var item in storage)
            message.WriteLine("{0}:\t{1}", item.Key, item.Value);
        return message.ToString();
    }
}