using UnityEngine;

namespace Code.General
{
    public class CashSystem : MonoBehaviour
    {
        private const string CASH_KEY = "Cash";
        public void AddCash(float amount)
        {
            if (PlayerPrefs.HasKey(CASH_KEY)) PlayerPrefs.SetFloat(CASH_KEY, PlayerPrefs.GetFloat(CASH_KEY) + amount);
            else PlayerPrefs.SetFloat(CASH_KEY, amount);
            PlayerPrefs.Save();
        }
    }
}