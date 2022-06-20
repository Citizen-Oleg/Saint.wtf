using System;
using ResourceSystem;
using UnityEngine;

namespace AnimationSystem
{
    public struct AnimationResourceItem
    {
        public ResourceItem ResourceItem;
        public Vector3 StartPosition;
        public Quaternion StartRotation;
        public Vector3 OffSet;
        public Transform EndTransformPosition;
        public float Progress;
        public Action<Vector3> CallBack;
    }
}