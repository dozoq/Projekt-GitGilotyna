using System;
using System.Collections;
using System.Collections.Generic;
using Code.General;
using TMPro;
using UnityEngine;

public class CashUIUpdate : MonoBehaviour
{
    [SerializeField] private TMP_Text label;

    private void OnEnable()
    {
        ReadAndSaveCashPrize();
        UpdateCashUI();
    }

    private void ReadAndSaveCashPrize()
    {
        if (PlayerPrefs.HasKey(CashSystem.CASH_PRIZE_KEY))
        {
            CashSystem.AddCash(PlayerPrefs.GetFloat(CashSystem.CASH_PRIZE_KEY));
            PlayerPrefs.DeleteKey(CashSystem.CASH_PRIZE_KEY);
        }
    }

    public void UpdateCashUI()
    {
        label.text = $"{CashSystem.GetCash():F}$";
    }

}
