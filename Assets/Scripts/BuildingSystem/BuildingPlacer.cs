using System;
using BuildingSystem.Models;
using GameInput;
using UnityEngine;

namespace BuildingSystem
{
    public class BuildingPlacer : MonoBehaviour
    {
        public event Action ActiveBuildableChanged;
        
        [field: SerializeField]
        public BuildableTower ActiveBuildableTower { get; private set; }

        [SerializeField] 
        private float _maxBuildingDistance = 3f;

        [SerializeField] 
        private ConsructionLayer _consructionLayer;
        
        [SerializeField] 
        private PreviewLayer _previewLayer;
        
        [SerializeField]
        private MouseInput _mouseInput;

        private void Update()
        {
            if (!IsMouseWithinBuildableRange() || _consructionLayer == null || ActiveBuildableTower == null)
            {
                _previewLayer.ClearPreview();
                return;
            }
            
            var mousePos = _mouseInput.MouseInWorldPosition;
            
            if (_mouseInput.IsMouseButtonPressed(MouseButton.Right))
            {
                _consructionLayer.Destroy(mousePos);
            }
            
            _previewLayer.ShowPreview(ActiveBuildableTower, 
                mousePos,
                _consructionLayer.IsEmpty(mousePos)
                );
            
            if (_mouseInput.IsMouseButtonPressed(MouseButton.Left) &&
                _consructionLayer.IsEmpty(mousePos))
            {
                _consructionLayer.Build(mousePos, ActiveBuildableTower);
            }
        }

        private bool IsMouseWithinBuildableRange()
        {
            return Vector3.Distance(_mouseInput.MouseInWorldPosition, transform.position) <= _maxBuildingDistance;
        }

        public void SetActiveBuildable(BuildableTower item)
        {
            ActiveBuildableTower = item;
            ActiveBuildableChanged?.Invoke();
        }
    }
}

