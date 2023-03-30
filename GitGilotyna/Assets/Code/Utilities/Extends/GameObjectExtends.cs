using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Code.Utilities.Extends
{
    public static class GameObjectExtends
    {
        public static bool CompareListOfTags(this GameObject gameObject, List<string> tags)
        {
            return tags.Any(tag => gameObject.CompareTag(tag));
        }
    }
}