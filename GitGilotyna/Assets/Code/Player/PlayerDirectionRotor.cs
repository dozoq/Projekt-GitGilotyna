using System;
using UnityEngine;

namespace Code.Player
{
    public class PlayerDirectionRotor : MonoBehaviour
    {
        private          Player  player;
        private readonly Vector3 right                   = new Vector3(1f,  1f, 1f);
        private readonly Vector3 left                    = new Vector3(-1f, 1f, 1f);
        private const    float   MinimumVelocityToRotate = 0.01f;
        private void Start()
        {
            player = GetComponentInParent<Player>();
        }

        private void Update()
        {
            if (player.GetMoveVelocityX > MinimumVelocityToRotate)
            {
                transform.localScale = left;
            }
            else if (player.GetMoveVelocityX < -MinimumVelocityToRotate)
            {
                transform.localScale = right;
            }
        }
    }
}