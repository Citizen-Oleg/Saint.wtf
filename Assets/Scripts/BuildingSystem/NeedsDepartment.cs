using Events;
using UnityEngine;
using Zenject;

namespace BuildingSystem
{
    public class NeedsDepartment : MonoBehaviour
    {
        [SerializeField]
        private string _nameBuild;
        [SerializeField]
        private PointOfIssue _pointOfIssue;
        [SerializeField]
        private ReceptionPoint _receptionPoint;
        
        private BuildingMiner _buildingMiner;

        private bool _isFullWarehouseMessageSent;
        private bool _isResourceShortageMessageSent;
        
        public void Initialize(BuildingMiner buildingMiner)
        {
            _buildingMiner = buildingMiner;

            _buildingMiner.OnOutputWarehouseFull += NotifyWhenStockIsFull;
            _pointOfIssue.OnCollectingResources += ResetNotifyWhenStockIsFull;

            if (_receptionPoint != null)
            {
                _receptionPoint.OnFilledWithResources += ResetNotifyAboutLackOfResources;
                _buildingMiner.OnResourceScarce += NotifyAboutLackOfResources;
            }
        }

        private void NotifyAboutLackOfResources()
        {
            if (!_isResourceShortageMessageSent)
            {
                EventStreams.UserInterface.Publish(new EventResourceShortage("Недостаточно ресурсов в " + _nameBuild));
            }

            _isResourceShortageMessageSent = true;
        }

        private void NotifyWhenStockIsFull()
        {
            if (!_isFullWarehouseMessageSent)
            {
                EventStreams.UserInterface.Publish(new EventMessageFullStock("Склад производства " + _nameBuild + " переполнен"));
            }
            
            _isFullWarehouseMessageSent = true;
        }

        private void ResetNotifyAboutLackOfResources()
        {
            var needResource = _receptionPoint.GetRequiredResources();
            if (_receptionPoint.HasResources(needResource))
            {
                _isResourceShortageMessageSent = false;
            }
        }
        
        private void ResetNotifyWhenStockIsFull()
        {
            if (_pointOfIssue.IsTopUpAvailable())
            {
                _isFullWarehouseMessageSent = false;
            }
        }
    }
}