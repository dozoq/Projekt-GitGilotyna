using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageChanger : MonoBehaviour
{

    [SerializeField] private Image image;

    public void ChangeSprite(Sprite sprite)
    {
        image.sprite = sprite;
    }
}
