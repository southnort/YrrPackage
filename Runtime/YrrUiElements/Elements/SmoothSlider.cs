﻿using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


namespace Yrr.UI.Elements
{
    public sealed class SmoothSlider : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] private bool onlyIncrement;
        private float _targetValue;
        private bool _isMoving;

        public UnityEvent onSliderMoveUp;
        public UnityEvent onSliderMoveDown;
        public UnityEvent onSliderStop;


        private void Start()
        {
            slider.interactable = false;
        }


        private void Update()
        {
            if (slider.value.Equals(_targetValue)) return;

            if (!_isMoving)
            {
                if (_targetValue > slider.value)
                    onSliderMoveUp?.Invoke();

                else
                    onSliderMoveDown?.Invoke();
                _isMoving = true;
            }

            var delta = Mathf.Abs(slider.value - _targetValue) * 2;
            if (delta < 0.45) delta = 0.45f;
            slider.value = Mathf.MoveTowards(slider.value, _targetValue, delta * Time.unscaledDeltaTime);

            if (!slider.value.Equals(_targetValue)) return;
            _isMoving = false;
            onSliderStop?.Invoke();
        }



        /// <summary>
        /// Value must be in range 0-1
        /// </summary>
        /// <param name="value"></param>
        public void InitValue(float value)
        {
            if (value < 0)
            {
                value = 0;
                Debug.LogError("Value must be 0..1");
            }
            if (value > 1)
            {
                value = 1;
                Debug.LogError("Value must be 0..1");
            }

            _targetValue = value;
            slider.value = value;
        }

        /// <summary>
        /// Value must be in range 0-1
        /// </summary>
        /// <param name="value"></param>
        public void ChangeValue(float value)
        {
            if (value < 0)
            {
                value = 0;
                Debug.LogError("Value must be 0..1");
            }
            if (value > 1)
            {
                value = 1;
                Debug.LogError("Value must be 0..1");
            }

            if (value < _targetValue && onlyIncrement)
            {
                _targetValue = 1;
                onSliderStop.AddListener(ResetSlider);
            }
            else
            {
                _targetValue = value;
            }
        }

        private void ResetSlider()
        {
            _targetValue = 0;
            onSliderStop.RemoveListener(ResetSlider);
        }
    }
}