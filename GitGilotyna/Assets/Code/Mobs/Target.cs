using System;
using Code.Mobs;
using Code.Utilities;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Code.Mobs
{
    public class Target : MonoBehaviour
    {
        [SerializeField] private float MaxHealth = 100f;
        [SerializeField] private Image healthUI;
        [SerializeField] private Canvas canvas;
        [SerializeField] private TargetType type;
        [SerializeField] private UnityEvent<float> OnDamage;
        private                  float health;
        private IDeadable deadable;

        private void Start()
        {
            float modifier = 1f;
            if (type == TargetType.PLAYER && PlayerPrefs.HasKey(SkillType.HEALTH.ToString()))
            {
                modifier += 0.01f + PlayerPrefs.GetInt(SkillType.HEALTH.ToString());
            }

            MaxHealth *= modifier;
            health = MaxHealth;
            deadable = (IDeadable)gameObject.GetComponentInParent(typeof(IDeadable));
        }

        public void TakeDamage(float amount)
        {
            float modifier = 1f;
            if (type == TargetType.PLAYER)
            {     
                if (PlayerPrefs.HasKey(SkillType.ARMOR.ToString()))
                    modifier += 0.01f * PlayerPrefs.GetInt(SkillType.ARMOR.ToString());
            }
            else
            {
                canvas.gameObject.SetActive(true);
            }
            health -= amount/modifier;
            healthUI.fillAmount = health / MaxHealth;


            OnDamage.Invoke(amount);
            if(health <= 0) deadable.MakeDead();
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