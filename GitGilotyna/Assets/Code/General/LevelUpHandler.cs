using System;
using System.Collections.Generic;
using System.Linq;
using Code.Enemy.WeaponTypes.WeaponDecorators;
using Code.Player;
using Unity.Services.Analytics;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using UnityEngine;
using UnityEngine.UI;

namespace Code.General
{
    public class LevelUpHandler : MonoBehaviour
    {
        private Action<WeaponDecoratorType> selectCallback;
        [SerializeField] private List<GameObject> attachmentSlots;
        private List<WeaponDecoratorType> _attachments;
        [SerializeField] private GameObject upgradePanel;

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

        public void ShowAttachments(List<WeaponDecoratorType> attachments, Action<WeaponDecoratorType> selectCallback)
        {
            _attachments = attachments;
            for (int i = 0; i < attachments.Count; i++)
            {
                var button = attachmentSlots[i].GetComponent<Button>();
                button.onClick = new();
                var sprite = 
                    Resources.Load<Sprite>($"AttachmentImages/{WeaponDecorator.GetResourceFromType(attachments[i])}");
                button.GetComponentInChildren<ImageChanger>().ChangeSprite(sprite);
                AttachEvent(button, i, selectCallback);
            }
            upgradePanel.SetActive(true);
        }

        private void AttachEvent(Button btn, int index, Action<WeaponDecoratorType> selectCallback)
        {
            btn.GetComponent<Button>().onClick.AddListener(()=>
            {
                selectCallback.Invoke(_attachments[index]);
                upgradePanel.SetActive(false);
                #if ENABLE_CLOUD_SERVICES_ANALYTICS
                                AnalyticsService.Instance.CustomData("AttachmentPicked", new Dictionary<string, object>()
                                {
                                    {
                                        "AttachmentPicked", _attachments[index].ToString()
                                    },
                                    {
                                        "AttachmentList", string.Join(", ",_attachments)
                                    },{
                                        "PlayerLevel", PlayerPrefs.GetInt(LevelSystem.LEVEL_KEY)
                                    },
                                    
                                });
                                AnalyticsService.Instance.Flush();
                #endif
            });
        }
    }
}