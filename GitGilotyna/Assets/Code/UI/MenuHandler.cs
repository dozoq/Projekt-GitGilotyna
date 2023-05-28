using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandler : MonoBehaviour
{
    [SerializeField] private GameObject WeaponPanel;
    [SerializeField] private GameObject UpgradePanel;
    [SerializeField] private GameObject MissionPanel;

    public void ToggleWeaponPanel()
    {
        if(WeaponPanel.activeSelf) WeaponPanel.SetActive(false);
        else WeaponPanel.SetActive(true);
    }
    public void ToggleUpgradePanel()
    {
        if(UpgradePanel.activeSelf) UpgradePanel.SetActive(false);
        else UpgradePanel.SetActive(true);
    }
    public void ToggleMissionPanel()
    {
        if(MissionPanel.activeSelf) MissionPanel.SetActive(false);
        else MissionPanel.SetActive(true);
    }
}
