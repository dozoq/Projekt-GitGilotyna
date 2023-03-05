using UnityEngine;

namespace Code.Utilities
{
    public class DirectionRotor
    {
        private readonly Vector3 right                   = new Vector3(1f,  1f, 1f);
        private readonly Vector3 left                    = new Vector3(-1f, 1f, 1f);
        private const    float   MinimumVelocityToRotate = 0.01f;

        public void SetDirectionByFlippingSprite(Transform transform, float velocityX)
        {
            if (velocityX > MinimumVelocityToRotate)
            {
                transform.localScale = left;
            }
            else if (velocityX < -MinimumVelocityToRotate)
            {
                transform.localScale = right;
            }
        }
    }
}