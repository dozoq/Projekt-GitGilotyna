using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchGraphicsOnClick : MonoBehaviour
{
    [SerializeField]private Sprite onToggleSprite;
    private Sprite old;
    private bool isToggled = false;
    [SerializeField] private UnityEngine.UI.Image component;

    private void Start()
    {
        old = component.sprite;
    }

    public void Toggle()
    {
        isToggled = !isToggled;
        if (!isToggled) component.sprite = old;
        if (isToggled) component.sprite = onToggleSprite;
    }
}
