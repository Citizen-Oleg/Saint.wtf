using System;
using PlayerComponent;
using ResourceSystem;
using UnityEngine;

namespace BuildingSystem
{
    public class PointOfIssue : MonoBehaviour
    {
        public event Action OnCollectingResources;
        
        [SerializeField]
        private float _resourceProcessingTime;
        [SerializeField]
        private Stock _stock;

        private float _startTime;
        private Player _player;
        
        public bool IsTopUpAvailable()
        {
            return !_stock.IsWarehouseFull;
        }

        private bool CanTakeResources()
        {
            return !_stock.IsWarehouseEmpty;
        }
        
        public void SendResourceToWarehouse(ResourceItem resourceItem)
        {
            if (CanDonateResource())
            {
                _player.Inventory.ReplenishInventory(resourceItem);
            }
            else
            {
                _stock.TopUp(resourceItem);
            }
        }

        private ResourceItem TakeResource()
        {
            return _stock.TakeResource();
        }

        private void OnTriggerStay(Collider other)
        {
            if (CanDonateResource())
            {
                _player.Inventory.ReplenishInventory(TakeResource());
                OnCollectingResources?.Invoke();
            }
        }

        private bool CanDonateResource()
        {
            return _player != null && !_player.Inventory.IsInventoryFull && CanTakeResources() && IsResourceProcessingTime();
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