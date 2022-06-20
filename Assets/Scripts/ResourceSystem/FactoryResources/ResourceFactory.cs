using Pools;
using UnityEngine;
using Zenject;

namespace ResourceSystem.FactoryResources
{
    public class ResourceFactory : PlaceholderFactory<Transform, ResourceType, ResourceItem>
    {
        private ResourcePool _craftableResourcePool;

        public ResourceFactory(ResourcePool craftableResourcePool)
        {
            _craftableResourcePool = craftableResourcePool;
        }
        
        public override ResourceItem Create(Transform transform, ResourceType resourceType)
        {
            var resourceItem = _craftableResourcePool.GetResourceByResourceType(resourceType);
            resourceItem.transform.position = transform.position;
            return resourceItem;
        }
    }
}