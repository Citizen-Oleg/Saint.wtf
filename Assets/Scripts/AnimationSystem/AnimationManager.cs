using System;
using System.Collections.Generic;
using ResourceSystem;
using UnityEngine;
using Zenject;

namespace AnimationSystem
{
    public class AnimationManager : ITickable
    {
        private readonly float _travelTime;
        private readonly AnimationCurve _yPositionCurve;
        private readonly List<AnimationResourceItem> _animationResourceItems = new List<AnimationResourceItem>();
        
        public AnimationManager(AnimationSettings animationSettings)
        {
            _travelTime = animationSettings.TravelTime;
            _yPositionCurve = animationSettings.YPositionCurve;
        }

        public void ShowFlyingResource(ResourceItem resourceItem, Vector3 offset, Transform endTransformPosition, Action<Vector3> callBack)
        {
            _animationResourceItems.Add(new AnimationResourceItem
            {
                ResourceItem = resourceItem,
                StartPosition = resourceItem.transform.position,
                StartRotation = resourceItem.transform.rotation,
                CallBack = callBack,
                EndTransformPosition = endTransformPosition,
                OffSet = offset,
                Progress = 0
            });
        }
        
        public void Tick()
        {
            for (int i = 0; i < _animationResourceItems.Count; i++)
            {
                var positionYcurve = Vector3.zero;
                var information = _animationResourceItems[i];
                positionYcurve = _yPositionCurve.Evaluate(information.Progress) * Vector3.up;
                
                information.Progress += Time.deltaTime / _travelTime;

                var endPosition = information.EndTransformPosition.TransformPoint(information.EndTransformPosition.localPosition + information.OffSet);
                var positionItem = Vector3.Lerp(information.StartPosition, endPosition, information.Progress)
                                   + positionYcurve;
                information.ResourceItem.transform.position = positionItem;
                
                var rotationItem = Quaternion.Lerp(information.StartRotation, information.EndTransformPosition.rotation,
                    information.Progress);
                
                information.ResourceItem.transform.rotation = rotationItem;
                
                _animationResourceItems[i] = information;
                if (information.Progress >= 1)
                {
                    information.CallBack?.Invoke(information.OffSet);
                    _animationResourceItems.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}