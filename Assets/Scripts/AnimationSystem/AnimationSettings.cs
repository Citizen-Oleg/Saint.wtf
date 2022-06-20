using System;
using UnityEngine;

namespace AnimationSystem
{
    [Serializable]
    public class AnimationSettings
    {
        public float TravelTime => _travelTime;
        public AnimationCurve YPositionCurve => _yPositionCurve;

        [SerializeField]
        private float _travelTime;
        [SerializeField]
        private AnimationCurve _yPositionCurve;
    }
}