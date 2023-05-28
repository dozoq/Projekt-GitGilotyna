using System.Collections;
using System.Collections.Generic;
using Code.General;
using TMPro;
using UnityEngine;

public class HandleMoneyCount : MonoBehaviour
{
    [SerializeField] private TMP_Text counter;
    // Start is called before the first frame update
    void Start()
    {
        RefreshCounter();
        GameManager.instance.onStatsChangedAction += RefreshCounter;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void RefreshCounter()
    {
        counter.text = CashSystem.GetCash().ToString()+"$";
    }
}
