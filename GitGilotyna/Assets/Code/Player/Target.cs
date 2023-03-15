using System;
using Code.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Player
{
    public class Target : MonoBehaviour
    {
        [SerializeField] private float MaxHealth = 100f;
        [SerializeField] private Image healthUI;
        [SerializeField] private Canvas canvas;
        private                  float health;

        private void Start()
        {
            health = MaxHealth;
        }

        public void TakeDamage(float amount)
        {
            if (canvas != null)
            {
                canvas.gameObject.SetActive(true);
                healthUI.fillAmount = health / MaxHealth;
            }

            health -= amount;
            if(health <= 0) MakeDead();
            Debug.Log(health);
        }

        private void MakeDead()
        {
            Destroy(gameObject);
        }
    }

    public class NotATargetException : CodeExecption
    {
        public NotATargetException() : base(10002)
        {
        }
    }
}