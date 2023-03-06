using System;
using Code.Utilities;
using UnityEngine;

namespace Code.Player
{
    public class Target : MonoBehaviour
    {
        [SerializeField] private int MaxHealth = 100;
        private                  int health;

        private void Start()
        {
            health = MaxHealth;
        }

        public void TakeDamage(int amount)
        {
            health -= amount;
            Debug.Log("Health available: "+health);
        }
    }

    public class NotATargetException : CodeExecption
    {
        public NotATargetException() : base(10002)
        {
        }
    }
}