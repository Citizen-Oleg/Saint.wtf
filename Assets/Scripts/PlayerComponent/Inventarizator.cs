using System;
using System.Collections.Generic;
using ResourceSystem;
using UnityEngine;

namespace PlayerComponent
{
    public class Inventarizator : MonoBehaviour
    {
        public Transform StartPositionResource => _startPositionResource;

        [SerializeField]
        private Transform _startPositionResource;
        [Range(1, 12)]
        [SerializeField]
        private int _numberOfLines = 1;
        [Range(1, 12)]
        [SerializeField]
        private int _amountResourcesLines = 1;
        [SerializeField]
        private float _offsetX;
        [SerializeField]
        private float _offsetY;
        [SerializeField]
        private float _offsetZ;
        
        public void FullInventoryFromTheCurrentIndex(List<ResourceItem> resourceItems, int index)
        {
            for (int i = index; i < resourceItems.Count; i++)
            {
                InventarizationSpecificIndex(resourceItems[i], i);
            }
        }

        public void InventarizationSpecificIndex(ResourceItem resourceItem, int index)
        {
            resourceItem.transform.parent = _startPositionResource.transform;
            resourceItem.transform.localPosition = _startPositionResource.localPosition + GetOffSetByIndex(index);
            resourceItem.transform.localRotation = Quaternion.identity;
        }
        
        public void InventarizationSpecificOffset(ResourceItem resourceItem, Vector3 offset)
        {
           resourceItem.transform.parent = _startPositionResource.transform;
           resourceItem.transform.localPosition = _startPositionResource.localPosition + offset;
           resourceItem.transform.localRotation = Quaternion.identity;
        }

        public Vector3 GetOffSetByIndex(int index)
        {
            var offsetY = index / (_numberOfLines * _amountResourcesLines) * _offsetY;
            var offsetX = index % _numberOfLines * _offsetX;
            var offsetZ = index / _numberOfLines % _amountResourcesLines * _offsetZ;

            return new Vector3(offsetX, offsetY, offsetZ);
        }
    }
}