using System;
using System.Collections;
using System.Collections.Generic;
using Code.Weapon.WeaponData;
using UnityEngine;
using UnityEngine.UI;

public class ApplyWeaponForPlayer : MonoBehaviour
{
    public static readonly string WEAPON_KEY = "WEAPON_KEY";
    [SerializeField] private WeaponData weapon;
    [SerializeField] private List<GameObject> siblings;
    [SerializeField] private Color selectedColor = Color.green;
    [SerializeField] private Color deselectedColor = Color.black;
    [SerializeField] private bool isDefault = false;

    private void Start()
    {
        if(isDefault) SaveWeaponOnClick();
    }

    public void SaveWeaponOnClick()
    {
        PlayerPrefs.SetString(WEAPON_KEY, $"SO/{weapon.fileName}");
        ChangeOutlineToSelected();
    }

    private void ChangeOutlineToSelected()
    {
        foreach (var sibling in siblings)
        {
            sibling.GetComponentInChildren<Outline>().effectColor = deselectedColor;
        }

        GetComponentInChildren<Outline>().effectColor = selectedColor;
    }
}
