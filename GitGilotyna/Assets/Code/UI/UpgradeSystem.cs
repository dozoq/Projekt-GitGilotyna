using System;
using System.Collections;
using System.Collections.Generic;
using Code.General;
using Unity.Services.Analytics;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
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

    private async void OnEnable()
    {
        DrawOutlines();
        #if ENABLE_CLOUD_SERVICES
                                                
                var options = new InitializationOptions();
                options.SetEnvironmentName("dev");
                await UnityServices.InitializeAsync(options);
                try
                {
                    await AuthenticationService.Instance.SignInAnonymouslyAsync();
                }
                catch (AuthenticationException ex)
                {
                                                            
                }
        #endif
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

    List<string> GetAllUpgradesForAnalytics()
    {
       var list = new List<string>();
       foreach (var skill in UpgradeButtons)
       {
           if(PlayerPrefs.HasKey(skill.name)) list.Add(skill.name + skill.value);
       }
       return list;
    }

    public void GetSkill(SkillSlot skill)
    {
        if (IsAfforadble(skill.cost) && !PlayerPrefs.HasKey(skill.name) && IsSkillReadyToUpgrade(skill))
        {
            CashSystem.RemoveCash(skill.cost);
            _cashUIUpdate.UpdateCashUI();
            PlayerPrefs.SetInt(skill.type.ToString(), skill.value);
            PlayerPrefs.SetInt(skill.name, skill.value);
            #if ENABLE_CLOUD_SERVICES_ANALYTICS
                        AnalyticsService.Instance.CustomData("UpgradePicked", new Dictionary<string, object>()
                        {
                            {
                                "UpgradeName", skill.name + skill.value
                            },
                            {
                                "UpgradeList", string.Join(", ",GetAllUpgradesForAnalytics())
                            }
                        });
                        AnalyticsService.Instance.Flush();
            #endif
            DrawOutlines();
        }
    }

    
}
