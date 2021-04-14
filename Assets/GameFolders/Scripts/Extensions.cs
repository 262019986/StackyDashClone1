using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameFolders.Scripts
{
    public static class Extensions
    {
        public static void RemoveAndParentNull<T>(this IList<T> list) where T : Transform
        {
            list.Last().SetParent(null);
            list.Remove(list.Last());
        }
    }
}