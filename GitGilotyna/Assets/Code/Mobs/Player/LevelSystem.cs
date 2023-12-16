using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Code.Player
{
    public class LevelSystem: MonoBehaviour
    {
        public static readonly string LEVEL_KEY = "PLAYER_LEVEL";
        [SerializeField] private Slider _slider;
        [SerializeField] private TMP_Text _lvlCounter;
        [SerializeField] private float experienceAmount = 0;
        [SerializeField] private float experienceNeededForFirstLevel = 100;
        [SerializeField] private float levelRequirementsExponential = 1f;
        [SerializeField] private int level = 1;
        [SerializeField] private UnityEvent<GameObject> onLevelUp;
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
            onLevelUp?.Invoke(gameObject);
            _lvlCounter.text = level.ToString();
            experienceAmount -= experienceNeeded;
            experienceNeeded = experienceNeededForFirstLevel * Mathf.Pow(level, levelRequirementsExponential);
            PlayerPrefs.SetInt(LEVEL_KEY, level);
        }

        private void MoveSlider()
        {
            _slider.value = experienceAmount / experienceNeeded;
        }

    }
}