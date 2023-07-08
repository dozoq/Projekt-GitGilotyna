using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimationScript : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void ReadInput(InputAction.CallbackContext ctx)
    {
        var value = ctx.ReadValue<Vector2>();
        _animator.SetFloat("ForwardSpeed", Mathf.Abs(value.y));
        _animator.SetFloat("StrifeSpeed", Mathf.Abs(value.x));
        _animator.SetBool("PointsDown", value.y<0);
    }
}
