using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Code.General;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinOnEnter : MonoBehaviour
{
    [SerializeField] private float cashPrize = 0f;

    public void Win()
    {
        PlayerPrefs.SetFloat(CashSystem.CASH_PRIZE_KEY, cashPrize);
        SceneManager.LoadScene(0);
    }
}
