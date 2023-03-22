using UnityEngine;

namespace Code.Utilities
{
    public static class MathUtils
    {
        
        /// <summary>
        /// Gets random value between start - offset / 2 and start + offset / 2
        /// </summary>
        /// <param name="start">float that represents start value</param>
        /// <param name="offset">float that represents value that will be substracted/added to start</param>
        /// <returns></returns>
        public static float GetRandomFloatWithOffset(float start, float offset)
        {
            return Random.Range(start - offset / 2, start + offset / 2);
        }
    }
}