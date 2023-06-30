using System;
using System.Collections;
using System.Collections.Generic;
using Code.General;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSystem : MonoBehaviour
{
    [SerializeField] private List<SkillSlot> UpgradeButtons;
    [SerializeField] private CashUIUpdate _cashUIUpdate;

    [SerializeField] private Color UpgradedColor;
    [SerializeField] private Color NotUpgradedColor;
    [SerializeField] private Color PossibleUpgradeColor;
    // Start is called before the first frame update

    private void OnEnable()
    {
        DrawOutlines();
    }

    void DrawOutlines()
    {
        foreach (var upgrade in UpgradeButtons)
        {
            var outline = upgrade.GetComponent<Image>();
            var outlineExtra = upgrade.GetComponent<Outline>();
            if (PlayerPrefs.HasKey(upgrade.name))
            {
                outline.color = UpgradedColor;
                outlineExtra.effectColor = UpgradedColor;
            }
            else if(IsAfforadble(upgrade.cost) && IsSkillReadyToUpgrade(upgrade)) 
            {
                outline.color = PossibleUpgradeColor;
                outlineExtra.effectColor = PossibleUpgradeColor;
            }
            else
            {
                // outline.color = NotUpgradedColor;
            }
        }
    }

    private bool IsAfforadble(float cost)
    {
        return PlayerPrefs.HasKey(CashSystem.CASH_KEY) && PlayerPrefs.GetFloat(CashSystem.CASH_KEY) > cost;
    }

    private bool IsSkillReadyToUpgrade(SkillSlot skill)
    {
        if (skill.previousNeededSkill != null)
            return PlayerPrefs.HasKey(skill.previousNeededSkill.name);
        else return true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetSkill(SkillSlot skill)
    {
        if (IsAfforadble(skill.cost) && !PlayerPrefs.HasKey(skill.name) && IsSkillReadyToUpgrade(skill))
        {
            CashSystem.RemoveCash(skill.cost);
            _cashUIUpdate.UpdateCashUI();
            PlayerPrefs.SetInt(skill.type.ToString(), skill.value);
            PlayerPrefs.SetInt(skill.name, skill.value);
            DrawOutlines();
        }
    }

    
}
