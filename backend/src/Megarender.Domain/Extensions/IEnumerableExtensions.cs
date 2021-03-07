using System;
using System.Collections.Generic;
using System.Linq;

namespace Megarender.Domain.Extensions {
    public static class IEnumerableExtensions {
        public static bool HaveDuplicates<TEntity, TProperty> (this IEnumerable<TEntity> list, Func<TEntity, TProperty> expression) {
            return list.Count () != list.Select (expression).Distinct ().Count ();
        }

        public static IEnumerable<TEntity> ForEach<TEntity>(this IEnumerable<TEntity> list, Action<TEntity> action)
        {            
            foreach(var element in list)
            {
                action(element);
            }
            return list;
        }
    }
}