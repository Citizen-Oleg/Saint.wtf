using System;
using System.Collections.Generic;
using ResourceSystem;
using ResourceSystem.FactoryResources;
using UnityEngine;
using Zenject;

namespace BuildingSystem
{
    public class BuildingMiner : ITickable
    {
        public event Action OnOutputWarehouseFull;
        public event Action OnResourceScarce;

        private Transform _transform;
        private readonly float _timeSpawn;
        private readonly ResourceType _craftableResource;
        private readonly ReceptionPoint _receptionPoint;
        private readonly PointOfIssue _pointOfIssue;
        private readonly List<ResourceType> _resourcesForProduction;
        private ResourceFactory _resourceFactory;
        
        private float _startTime;

        public BuildingMiner(SettingsBuildingMiner settingsBuildingMiner, ResourceFactory resourceFactory)
        {
            _transform = settingsBuildingMiner.TransformBuilding;
            _timeSpawn = settingsBuildingMiner.TimeSpawn;
            _craftableResource = settingsBuildingMiner.CraftableResource;
            _receptionPoint = settingsBuildingMiner.ReceptionPoint;
            _pointOfIssue = settingsBuildingMiner.PointOfIssue;
            _resourcesForProduction = settingsBuildingMiner.ResourcesForProduction;
            _resourceFactory = resourceFactory;
            settingsBuildingMiner.NeedsDepartment.Initialize(this);
        }

        public void Tick()
        {
            if (IsTimeSpawn())
            {
                if (!_pointOfIssue.IsTopUpAvailable())
                {
                    OnOutputWarehouseFull?.Invoke();
                    return;
                }
                
                if (_receptionPoint == null)
                {
                    _pointOfIssue.SendResourceToWarehouse(_resourceFactory.Create(_transform, _craftableResource));
                    return;
                }
                
                if (_receptionPoint.HasResources(_resourcesForProduction))
                {
                    _receptionPoint.SpendResourceOnCrafting(_resourcesForProduction);
                    _pointOfIssue.SendResourceToWarehouse(_resourceFactory.Create(_transform, _craftableResource));
                }
                else
                {
                    OnResourceScarce?.Invoke();
                }
            }
        }
        
        private bool IsTimeSpawn()
        {
            if (Time.time > _startTime + _timeSpawn)
            {
                _startTime = Time.time;
                return true;
            }

            return false;
        }
    }

    [Serializable]
    public class SettingsBuildingMiner
    {
        public Transform TransformBuilding => _transformBuilding;
        public float TimeSpawn => _timeSpawn;
        public ResourceType CraftableResource => _craftableResource;
        public ReceptionPoint ReceptionPoint => _receptionPoint;
        public PointOfIssue PointOfIssue => _pointOfIssue;
        public List<ResourceType> ResourcesForProduction => _resourcesForProduction;
        public NeedsDepartment NeedsDepartment => _needsDepartment;

        [SerializeField]
        private Transform _transformBuilding;
        [SerializeField]
        private float _timeSpawn;
        [SerializeField]
        private NeedsDepartment _needsDepartment;
        [SerializeField]
        private PointOfIssue _pointOfIssue;  
        [SerializeField]
        private ReceptionPoint _receptionPoint;
        [SerializeField]
        private ResourceType _craftableResource;
        [SerializeField]
        private List<ResourceType> _resourcesForProduction = new List<ResourceType>();
      
    }
}