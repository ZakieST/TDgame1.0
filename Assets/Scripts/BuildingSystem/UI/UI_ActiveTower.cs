using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BuildingSystem.UI
{
    public class UI_ActiveTower : MonoBehaviour
    {
        [SerializeField] 
        private BuildingPlacer _buildingPlacer;

        private Image _icon;

        private void Awake()
        {
            _icon = GetComponent<Image>();
            _buildingPlacer.ActiveBuildableChanged += OnActiveBuildableChanged;
        }

        private void Start()
        {
            OnActiveBuildableChanged();
        }

        private void OnActiveBuildableChanged()
        {
            if (_buildingPlacer.ActiveBuildableTower != null)
            {
                _icon.enabled = true;
                _icon.sprite = _buildingPlacer.ActiveBuildableTower.UiIcon;
            }
            else
            {
                _icon.enabled = false;
            }
            
        }
    }
}

