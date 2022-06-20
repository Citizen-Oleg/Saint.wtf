using System;
using System.Collections.Generic;
using System.Linq;
using PlayerComponent;
using ResourceSystem;
using UnityEngine;

namespace BuildingSystem
{
    public class ReceptionPoint : MonoBehaviour
    {
        public event Action OnFilledWithResources;
        
        [SerializeField]
        private float _resourceProcessingTime;
        [SerializeField]
        private List<Stock> _stocks = new List<Stock>();
        
        private float _startTime;
        private Player _player;

        private readonly List<ResourceType> _requiredResources = new List<ResourceType>();
        
        private void Start()
        {
            foreach (var stock in _stocks)
            {
                _requiredResources.Add(stock.ResourceType);
            }
        }
        
        public void SpendResourceOnCrafting(List<ResourceType> resourcesType)
        {
            foreach (var resourceType in resourcesType)
            {
                var stock = GetStockByResourceType(resourceType);
                if (stock != null)
                {
                    if (stock.IsWarehouseFull)
                    {
                        _requiredResources.Add(stock.ResourceType);
                    }
                    
                    var resource = stock.TakeResource();
                    resource.Release();
                }
            }
        }

        public bool HasResources(List<ResourceType> resourcesTypes)
        {
            foreach (var resourceType in resourcesTypes)
            {
                var stock = GetStockByResourceType(resourceType);
                
                if (stock == null)
                {
                    return false;
                }

                if (stock.IsWarehouseEmpty)
                {
                    return false;
                }
            }

            return true;
        }

        private void OnTriggerStay(Collider other)
        {
            if (CanTakeResource())
            {
                foreach (var stock in _stocks)
                {
                    if (_player.Inventory.HasResource(stock.ResourceType) && !stock.IsWarehouseFull)
                    {
                        ReplenishWarehouse(_player.Inventory.TakeResource(stock.ResourceType));
                        OnFilledWithResources?.Invoke();
                        break;
                    }
                }
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                _player = player;
            }
        }
        

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                _player = null;
            }
        }

        private bool CanTakeResource()
        {
            return _player != null && _player.Inventory.HasResource(GetRequiredResources()) && IsResourceProcessingTime();
        }

        private void ReplenishWarehouse(ResourceItem resourceItem)
        {
            var stock = GetStockByResourceType(resourceItem.ResourceType);
            stock.TopUp(resourceItem);

            if (stock.IsWarehouseFull)
            {
                _requiredResources.Remove(stock.ResourceType);
            }
        }

        public List<ResourceType> GetRequiredResources()
        {
            return _requiredResources;
        }

        private Stock GetStockByResourceType(ResourceType resourceType)
        {
            return _stocks.FirstOrDefault(stock => stock.ResourceType == resourceType);
        }
        
        private bool IsResourceProcessingTime()
        {
            if (Time.time > _startTime + _resourceProcessingTime)
            {
                _startTime = Time.time;
                return true;
            }

            return false;
        }
    }
}