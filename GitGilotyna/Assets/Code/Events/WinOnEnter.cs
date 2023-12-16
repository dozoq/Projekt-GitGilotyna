using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Code.General;
using Unity.Services.Analytics;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinOnEnter : MonoBehaviour
{
    [SerializeField] private float cashPrize = 0f;

    private async void OnEnable()
    {
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

    public void Win()
    {
        PlayerPrefs.SetFloat(CashSystem.CASH_PRIZE_KEY, cashPrize);
        #if ENABLE_CLOUD_SERVICES_ANALYTICS
                AnalyticsService.Instance.CustomData("LevelEnd", new Dictionary<string, object>()
                {
                    {
                        "WinFlag", true
                    },
                    {
                        "WeaponPicked", PlayerPrefs.GetString(ApplyWeaponForPlayer.WEAPON_SHORT_KEY)
                    }
                });
                AnalyticsService.Instance.Flush();
        #endif
        SceneManager.LoadScene(0);
    }
}
