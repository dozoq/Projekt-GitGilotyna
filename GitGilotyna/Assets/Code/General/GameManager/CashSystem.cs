using System;
using UnityEngine;

namespace Code.General
{
    public class CashSystem : MonoBehaviour
    {
        public static readonly string CASH_KEY = "Cash";
        public static readonly string CASH_PRIZE_KEY = "CASH_PRIZE";

        public static void AddCash(float amount)
        {
            if (PlayerPrefs.HasKey(CASH_KEY)) PlayerPrefs.SetFloat(CASH_KEY, PlayerPrefs.GetFloat(CASH_KEY) + amount);
            else PlayerPrefs.SetFloat(CASH_KEY, amount);
        }

        public static void RemoveCash(float amount)
        {
            PlayerPrefs.SetFloat(CASH_KEY, PlayerPrefs.GetFloat(CASH_KEY) - amount);

        }
        public static float GetCash()
        {
            if (PlayerPrefs.HasKey(CASH_KEY)) return PlayerPrefs.GetFloat(CASH_KEY);
            else return 0;

        }

        private void OnGUI()
        {
            GUILayout.BeginVertical();
            GUILayout.Space(20);
            if (GUILayout.Button("Get Cash"))
            {
                AddCash(500);
                Debug.Log(PlayerPrefs.GetFloat(CASH_KEY));
            }
            GUILayout.EndHorizontal();
        }
    }
}