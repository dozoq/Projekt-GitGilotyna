using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MissionHandler : MonoBehaviour
{
    [SerializeField] private List<Quest> _quests;

    [SerializeField] private GameObject MissionSlot1, MissionSlot2, MissionSlot3;
    // Start is called before the first frame update
    private void OnEnable()
    {
        GenerateRandomMission(MissionSlot1);
        GenerateRandomMission(MissionSlot2);
        GenerateRandomMission(MissionSlot3);
    }

    private void GenerateRandomMission(GameObject missionSlot)
    {
        var randomMission = Random.Range(0, _quests.Count - 1);
        missionSlot.GetComponent<Button>().onClick.AddListener(() =>
        {
            SceneManager.LoadScene(_quests[randomMission].scene);
        });
        missionSlot.GetComponentInChildren<TMP_Text>().text = _quests[randomMission].Name;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
