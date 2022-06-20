using Pools;
using UnityEngine;

namespace ResourceSystem
{
    public class ResourceItem : MonoBehaviour
    {
        public ResourceType ResourceType => _resourceType;

        [SerializeField]
        private ResourceType _resourceType;
        
        private ResourcePool _resourcePool;

        private Vector3 _startScale;
        private Quaternion _startQuaternion;
        
        public void Initialize(ResourcePool resourcePool)
        {
            _resourcePool = resourcePool;
        }

        public void Release()
        {
            _resourcePool.ReleaseResource(_resourceType, this);
            _resourcePool = null;
        }
    }
}