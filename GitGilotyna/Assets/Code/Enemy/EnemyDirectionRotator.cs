using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

namespace Code.Enemy
{
    public class EnemyDirectionRotator : MonoBehaviour
    {
        [SerializeField] private AIPath path;

        private readonly Vector3 right                   = new Vector3(1f,  1f, 1f);
        private readonly Vector3 left                    = new Vector3(-1f, 1f, 1f);
        private const    float   MinimumVelocityToRotate = 0.01f;

        // Start is called before the first frame update
        void Start()
        {
            path = GetComponentInParent<AIPath>();
        }

        // Update is called once per frame
        void Update()
        {
            if (path.desiredVelocity.x > MinimumVelocityToRotate)
            {
                transform.localScale = left;
            }
            else if (path.desiredVelocity.x < -MinimumVelocityToRotate)
            {
                transform.localScale = right;
            }

        }
    }
}
