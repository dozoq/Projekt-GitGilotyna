using System;
using System.Collections;
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
        private float _actualTime;

        private void Start()
        {
            StartCoroutine(Cycle());
        }

        private IEnumerator Cycle()
        {
            while (true)
            {
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
