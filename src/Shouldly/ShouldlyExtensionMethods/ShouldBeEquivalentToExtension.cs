using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Shouldly;

namespace ShouldlyExtension
{
    public static class ShouldBeEquivalentToExtension
    {
        public static void ShouldBeEquivalentTo(
            this object firstObject,
            object secondObject,
            string[] includeProperties,
            string customMessage = null)
        {
            var firstProperties = firstObject.GetType().GetProperties().ToList();
            var secondProperties = secondObject.GetType().GetProperties().ToList();

            if (includeProperties.Any())
            {
                firstProperties = firstProperties.Where(x => includeProperties.Contains(x.Name)).ToList();
                secondProperties = secondProperties.Where(x => includeProperties.Contains(x.Name)).ToList();
            }

            if (!firstProperties.Any() || !secondProperties.Any())
                throw new ShouldAssertException(customMessage ?? "two objects does not any same properties");

            if (firstProperties.Count != secondProperties.Count)
                throw new ShouldAssertException(customMessage ?? "two objects does not any same properties");


            foreach (var firstProperty in firstProperties)
            {
                foreach (var secondProperty in secondProperties)
                {
                    if (firstProperty.Name.Equals(secondProperty.Name))
                    {
                        var secondValue = secondProperty.GetValue(secondObject, null);
                        var firstValue = firstProperty.GetValue(firstObject, null);
                        if (secondValue != null && !secondValue.Equals(firstValue))
                        {
                            throw new ShouldAssertException(customMessage ??
                                                            $"{firstProperty.Name} of first object is : {firstValue} " +
                                                            $"but {secondProperty.Name} of secend object is :  {secondValue}");
                        }

                        break;
                    }
                }
            }
        }
        
        public static void ShouldBeEquivalentTo<TType>(
            this object obj, 
            IEnumerable<TType> objects, 
            string[] includeProperties,
            string customMessage = null) where TType : class
        {
            foreach (var @object in objects)
            {
                obj.ShouldBeEquivalentTo(@object, includeProperties,customMessage);
            }
        }
    }
    
}
