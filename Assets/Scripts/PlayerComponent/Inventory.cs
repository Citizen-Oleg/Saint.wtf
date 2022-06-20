using System;
using System.Collections.Generic;
using System.Linq;
using AnimationSystem;
using JetBrains.Annotations;
using ResourceSystem;
using UnityEngine;

namespace PlayerComponent
{
    public class Inventory
    {
        public bool IsInventoryFull => _resourceItems.Count == _capacity;
        
        private readonly int _capacity;
        private readonly List<ResourceItem> _resourceItems;
        private readonly Inventarizator _inventarizator;
        private readonly AnimationManager _animationManager;

        private List<ResourceType> _availableResourceTypes = new List<ResourceType>();

        public Inventory(InventorySettings inventorySettings, Inventarizator inventarizator, AnimationManager animationManager)
        {
            _capacity = inventorySettings.Capacity;
            _resourceItems = new List<ResourceItem>(_capacity);
            _inventarizator = inventarizator;
            _animationManager = animationManager;
        }

        public void ReplenishInventory(ResourceItem resourceItem)
        {
            _resourceItems.Add(resourceItem);
            CheckAvailableResourceTypes(resourceItem);
            
            _animationManager.ShowFlyingResource(resourceItem, _inventarizator.GetOffSetByIndex(_resourceItems.Count - 1), 
                _inventarizator.StartPositionResource, (offset) =>
                {
                    _inventarizator.InventarizationSpecificOffset(resourceItem, offset);
                });
        }

        public bool HasResource(ResourceType resourceType)
        {
            return _resourceItems.Any(resourceItem => resourceItem.ResourceType == resourceType);
        }

        public bool HasResource(List<ResourceType> resourceTypes)
        {
            foreach (var resourceType in resourceTypes)
            {
                if (HasResource(resourceType))
                {
                    return true;
                }
            }

            return false;
        }

        public ResourceItem TakeResource(ResourceType resourceType)
        {
            for (var i = _resourceItems.Count - 1; i >= 0; i--)
            {
                if (_resourceItems[i].ResourceType == resourceType)
                {
                    var item = _resourceItems[i];
                    _resourceItems.RemoveAt(i);
                    _inventarizator.FullInventoryFromTheCurrentIndex(_resourceItems, i);
                    CheckAvailableResourceTypes(item);
                    return item;
                }
            }
            
            return null;
        }

        private void CheckAvailableResourceTypes(ResourceItem resourceItem)
        {
            var hasResource = false;
            foreach (var resourceType in _availableResourceTypes)
            {
                if (resourceType == resourceItem.ResourceType)
                {
                    hasResource = true;
                }
            }

            if (!hasResource)
            {
                _availableResourceTypes.Add(resourceItem.ResourceType);
            }
        }
    }
    
    [Serializable]
    public class InventorySettings
    {
        public int Capacity => _capacity;

        [SerializeField]
        private int _capacity;
    }
}