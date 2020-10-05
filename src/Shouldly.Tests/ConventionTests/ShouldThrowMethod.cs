using System;
using System.Linq;
using System.Reflection;

namespace Shouldly.Tests.ConventionTests
{
    public class ShouldThrowMethod
    {
        private readonly MethodInfo _throwMethod;

        public ShouldThrowMethod(MethodInfo throwMethod)
        {
            _throwMethod = throwMethod;
            Name = throwMethod.Name.Replace("Should", string.Empty);
            IsShouldlyExtension = throwMethod.Name.StartsWith("Should");
            Parameters = throwMethod.GetParameters();
        }

        public bool IsShouldlyExtension { get; }
        public string Name { get; }
        public ParameterInfo[] Parameters { get; }

        protected bool Equals(ShouldThrowMethod other)
        {
            return string.Equals(Name, other.Name) && Parameters.All(p => other.Parameters.Any(op => p.Name == op.Name && TypeEqual(p.ParameterType, op.ParameterType)));
        }

        private static bool TypeEqual(Type pt1, Type pt2)
        {
            return pt1.FormatType() == pt2.FormatType();
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((ShouldThrowMethod) obj);
        }

        public override string ToString()
        {
            return _throwMethod.FormatMethod();
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Name != null ? Name.GetHashCode() : 0)*397) ^ (Parameters != null ? Parameters.GetHashCode() : 0);
            }
        }
    }
}