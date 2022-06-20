using System;
using System.Collections.Generic;
using ResourceSystem;
using UnityEngine;

namespace Pools
{
    public class ResourcePool
    {
        private readonly Dictionary<ResourceType, MonoBehaviourPool<ResourceItem>> _monoBehaviourPools = 
            new Dictionary<ResourceType, MonoBehaviourPool<ResourceItem>>();

        public ResourcePool(SettingsResourcePool settings)
        {
            var parent = new GameObject
            {
                name = "ResourcePool"
            };

            foreach (var resourceItem in settings.ResourceItems)
            {
                var pool = new MonoBehaviourPool<ResourceItem>(resourceItem, parent.transform, settings.PullSize);
                _monoBehaviourPools.Add(resourceItem.ResourceType, pool);
            }
        }

        public ResourceItem GetResourceByResourceType(ResourceType resourceType)
        {
            var item = _monoBehaviourPools[resourceType].Take();
            item.Initialize(this);
            return item;
        }
        
        public void ReleaseResource(ResourceType resourceType, ResourceItem resource)
        {
            _monoBehaviourPools[resourceType].Release(resource);
        }
    }
    
    [Serializable]
    public class SettingsResourcePool
    {
        public List<ResourceItem> ResourceItems => _resourceItems;
        public int PullSize => _pullSize;

        [SerializeField]
        private int _pullSize;
        [SerializeField]
        private List<ResourceItem> _resourceItems = new List<ResourceItem>();
    }
}