using System;
using System.Collections.Generic;
using AnimationSystem;
using PlayerComponent;
using ResourceSystem;
using UnityEngine;
using Zenject;

namespace BuildingSystem
{
    public class Stock : MonoBehaviour
    {
        public ResourceType ResourceType => _resourceType;
        public bool IsWarehouseFull => _capacity == _resourceItems.Count;
        public bool IsWarehouseEmpty => _resourceItems.Count == 0;

        [SerializeField]
        private ResourceType _resourceType;
        [SerializeField]
        private int _capacity;
        [SerializeField]
        private Inventarizator _inventarizator;
        
        private Stack<ResourceItem> _resourceItems;
        private AnimationManager _animationManager;

        private void Start()
        {
            _resourceItems = new Stack<ResourceItem>(_capacity);
        }

        [Inject]
        public void Constructor(AnimationManager animationManager)
        {
            _animationManager = animationManager;
        }

        public void TopUp(ResourceItem resourceItem)
        {
            _resourceItems.Push(resourceItem);
            _animationManager.ShowFlyingResource(resourceItem, _inventarizator.GetOffSetByIndex(_resourceItems.Count - 1), 
                _inventarizator.StartPositionResource, (offset) =>
                {
                    _inventarizator.InventarizationSpecificOffset(resourceItem, offset);
                });
        }

        public ResourceItem TakeResource()
        {
            var item = _resourceItems.Pop();
            return item;
        }
    }
}