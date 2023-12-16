using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using UnityEngine;
#if ENABLE_CLOUD_SERVICES_ANALYTICS
using UnityEngine.Analytics;            
#endif
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MissionHandler : MonoBehaviour
{
    [SerializeField] private List<Quest> _quests;

    [SerializeField] private GameObject MissionSlot1, MissionSlot2, MissionSlot3;
    // Start is called before the first frame update

    private async void Awake()
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

    private void OnEnable()
    {
        GenerateRandomMission(MissionSlot1);
        GenerateRandomMission(MissionSlot2);
        GenerateRandomMission(MissionSlot3);
    }

    private void GenerateRandomMission(GameObject missionSlot)
    {
        var randomMission = Random.Range(0, 10000)%_quests.Count;
        missionSlot.GetComponent<Button>().onClick.AddListener(() =>
        {
            SceneManager.LoadScene(_quests[randomMission].scene);
            #if ENABLE_CLOUD_SERVICES_ANALYTICS
            Debug.Log("Event Pushed");
            Analytics.CustomEvent("LevelBegin", new Dictionary<string, object>()
            {
                { "LevelName", _quests[randomMission].Name}
            });
            #endif
        });
        missionSlot.GetComponentInChildren<TMP_Text>().text = _quests[randomMission].Name;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
