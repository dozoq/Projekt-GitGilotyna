using System;
using Code.Mobs;
using Code.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Mobs
{
    public class Target : MonoBehaviour
    {
        [SerializeField] private float MaxHealth = 100f;
        [SerializeField] private Image healthUI;
        [SerializeField] private Canvas canvas;
        [SerializeField] private TargetType type;
        private                  float health;
        private IDeadable deadable;

        private void Start()
        {
            if (type == TargetType.PLAYER && PlayerPrefs.HasKey(SkillType.HEALTH.ToString()))
            {
                MaxHealth *= 1.0f + 100.0f/PlayerPrefs.GetInt(SkillType.HEALTH.ToString());
            }
            health = MaxHealth;
            deadable = (IDeadable)gameObject.GetComponent(typeof(IDeadable));
        }

        public void TakeDamage(float amount)
        {
            float modifier = 1f;
            if (type == TargetType.PLAYER)
            {
                canvas.gameObject.SetActive(true);
                healthUI.fillAmount = health / MaxHealth;
                if (PlayerPrefs.HasKey(SkillType.ARMOR.ToString()))
                    modifier += 100f / PlayerPrefs.GetInt(SkillType.ARMOR.ToString());
            }

            health -= amount/modifier;
            if(health <= 0) deadable.MakeDead();
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

    enum TargetType
    {
        PLAYER,ENEMY
    }
}