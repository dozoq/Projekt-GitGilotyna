using System;
using System.Collections.Generic;
using Code.General;
using Code.Mobs;
using Code.Player;
using Code.Utilities.Extends;
using UnityEngine;

namespace Code.Pickup
{
    public class Pickup: MonoBehaviour
    {
        [SerializeField] private float expValue = 0;
        [SerializeField] private float cashValue = 0;
        [SerializeField] private List<string> tags;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareListOfTags(tags))
            {
                HandlePickup();
            }
        }

        private void HandlePickup()
        {
            AddExperience();
            AddCash();
            Destroy(gameObject);
        }

        private void AddExperience()
        {
            var levelSystem = FindObjectOfType<Player.Player>().GetComponent<LevelSystem>();
            levelSystem.AddExperience(expValue);
        }

        private void AddCash()
        {
            CashSystem.AddCash(cashValue);
        }
    }
}