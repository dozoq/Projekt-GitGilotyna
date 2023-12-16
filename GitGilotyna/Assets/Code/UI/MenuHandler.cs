using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MenuHandler : MonoBehaviour
{
    [SerializeField] private GameObject WeaponPanel;
    [SerializeField] private GameObject UpgradePanel;
    [SerializeField] private GameObject MissionPanel;
    private readonly int ABOVE_SCREEN_POSITION = -1090;

    public void ToggleWeaponPanel()
    {
        var rtransform = WeaponPanel.GetComponent<RectTransform>();
        if (WeaponPanel.activeSelf)
        {
            rtransform.position = new Vector3(rtransform.position.x, 0);
            rtransform.DOLocalMove(new Vector3(0, ABOVE_SCREEN_POSITION), 1f).OnComplete(() => WeaponPanel.SetActive(false));      
            

        }
        else
        {
            WeaponPanel.SetActive(true);
            rtransform.position = new Vector3(rtransform.position.x, ABOVE_SCREEN_POSITION);
            rtransform.DOLocalMove(new Vector3(0, 0), 1f);    
        }
    }
    public void ToggleUpgradePanel()
    {
        var rtransform = UpgradePanel.GetComponent<RectTransform>();
        if (UpgradePanel.activeSelf)
        {
            UpgradePanel.SetActive(false);
            rtransform.DOLocalMove(new Vector3(0, ABOVE_SCREEN_POSITION), 1f).OnComplete(() => WeaponPanel.SetActive(false));  
        }
        else
        {
            UpgradePanel.SetActive(true);
            rtransform.position = new Vector3(rtransform.position.x, ABOVE_SCREEN_POSITION);
            rtransform.DOLocalMove(new Vector3(0, 0), 1f);    
        }
    }
    public void ToggleMissionPanel()
    {
        var rtransform = MissionPanel.GetComponent<RectTransform>();
        if (MissionPanel.activeSelf)
        {
            MissionPanel.SetActive(false);
            rtransform.DOLocalMove(new Vector3(0, ABOVE_SCREEN_POSITION), 1f).OnComplete(() => WeaponPanel.SetActive(false));  
        }
        else
        {
            MissionPanel.SetActive(true);
            rtransform.position = new Vector3(rtransform.position.x, ABOVE_SCREEN_POSITION);
            rtransform.DOLocalMove(new Vector3(0, 0), 1f);    
        }
    }
}
