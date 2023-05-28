using System.Collections;
using System.Collections.Generic;
using Code.General;
using TMPro;
using UnityEngine;

public class HandleKillCount : MonoBehaviour
{
    [SerializeField] private TMP_Text counter;
    // Start is called before the first frame update
    void Start()
    {
        RefreshCounter();
        GameManager.instance.onStatsChangedAction += RefreshCounter;
    }


    private void RefreshCounter()
    {
        counter.text = GameManager.instance.killCount.ToString();
    }
}
