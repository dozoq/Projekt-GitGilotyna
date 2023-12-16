using System;
using System.Collections;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;
using UnityEngine.U2D;

namespace Code.General
{
    public class DayCycle : MonoBehaviour
    {
        [SerializeField] private Light2D _sun;
        [SerializeField, Range(0, 1)] private float decreaseLightStep = 0.0001f;
        [SerializeField] private float lightUpdateWaitTime = 0.0001f;
        [SerializeField] private float timeUntilDawn = 10f;
        [SerializeField] private UnityEvent startNight;
        [SerializeField] private TMP_Text timer;
        private float _actualTime;

        private void Start()
        {
            StartCoroutine(Cycle());
        }

        private IEnumerator Cycle()
        {
            while (true)
            {
                RefreshTimer();
                if (_actualTime < timeUntilDawn)
                {
                    _actualTime += lightUpdateWaitTime;
                    yield return new WaitForSeconds(lightUpdateWaitTime);
                }
                else if (_sun.color.b > 0)
                {
                    DarkenSunByDecreaseStep(decreaseLightStep);
                    yield return new WaitForSeconds(lightUpdateWaitTime);
                }
                else
                {
                    startNight.Invoke();
                    StopCoroutine(Cycle());
                }
            }
        }

        private void RefreshTimer()
        {
            timer.text = RemapTime(timeUntilDawn-_actualTime);
        }

        private string RemapTime(float time)
        {
            int wholeNumber = (int)Math.Floor(time);
            int decimalPoint = (int)((time % 1) * 100);
            int seconds = (int)Remap(decimalPoint, 0, 100, 0, 60);
            return $"{wholeNumber}:{seconds}";
        }
        
        private float Remap (float value, float from1, float to1, float from2, float to2) {
            return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        }


        private void DarkenSunByDecreaseStep(float step)
        {
            var color = _sun.color;
            color.r -= step;
            color.g -= step;
            color.b -= step;
            _sun.color = color;
        }
    }
}
