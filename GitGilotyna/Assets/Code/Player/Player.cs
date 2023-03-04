using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Player
{
    public class Player : MonoBehaviour
    {
        public                   float       GetMoveVelocityX => moveVector.x;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private float       speed = 10.0f;
        [SerializeField] private Vector2     moveVector;

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void FixedUpdate()
        {
            transform.Translate(moveVector * (speed * Time.fixedDeltaTime));
        }

        public void Move(InputAction.CallbackContext ctx)
        {
            moveVector = ctx.ReadValue<Vector2>();
        }
    }
}
