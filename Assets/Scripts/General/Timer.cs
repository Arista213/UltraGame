using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace General
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private Text _timerText;

        public int TimeToStartRound = 20;
        private int _time;

        private void Start()
        {
            _time = TimeToStartRound;
            UpdateTimerText();
            InvokeRepeating(nameof(UpdateTime), 0f, 1);
        }

        private void UpdateTime()
        {
            _time--;
            UpdateTimerText();
            if (_time == 0)
            {
                Destroy(gameObject);
                CancelInvoke(nameof(UpdateTime));
            }
        }

        private void UpdateTimerText()
        {
            _timerText.text = $"{_time / 60:0}:{_time % 60:00}";
        }
    }
}