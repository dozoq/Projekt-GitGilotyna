using Code.Utilities;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float          speed = 10.0f;
        [SerializeField] private Transform      spriteTransform;
        private                  Vector2        moveVector;
        private                  DirectionRotor rotor;
        private                  Rigidbody2D    rb;

        // Start is called before the first frame update
        void Start()
        {
            rb    = GetComponent<Rigidbody2D>();
            rotor = new DirectionRotor();
        }

        // Update is called once per frame
        void Update()
        {
            rotor.SetDirectionByFlippingSprite(spriteTransform, moveVector.x);
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
