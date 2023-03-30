using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Code.Player
{
    public class LevelSystem: MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private float experienceAmount = 0;
        [SerializeField] private float experienceNeededForFirstLevel = 100;
        [SerializeField] private float levelRequirementsExponential = 1f;
        [SerializeField] private float level = 1;
        private float experienceNeeded;

        private void Start()
        {
            experienceNeeded = experienceNeededForFirstLevel;
        }

        public void AddExperience(float amount)
        {
            experienceAmount += amount;
            if (experienceAmount >= experienceNeeded)
            {
                LevelUp();
            }
            MoveSlider();
        }

        private void OnGUI()
        {
            if (GUILayout.Button("Add Experience"))
            {
                AddExperience(100);
            }
        }

        private void LevelUp()
        {
            level++;
            experienceAmount -= experienceNeeded;
            experienceNeeded = experienceNeededForFirstLevel * Mathf.Pow(level, levelRequirementsExponential);
        }

        private void MoveSlider()
        {
            _slider.value = experienceAmount / experienceNeeded;
        }

    }
}