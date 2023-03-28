using System;
using System.Collections;
using System.Collections.Generic;
using BuildingSystem.Models;
using GameInput;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BuildingSystem
{
    public class BuildingSelector : MonoBehaviour
    {
        [SerializeField] 
        private List<BuildableTower> _buildables;

        [SerializeField] 
        private BuildingPlacer _buildingPlacer;

        private int _activeBuildableIndex;

        private void OnEnable()
        {
            Controls.Instance.Gameplay.ChangeAction.performed += OnChangeActionPerformed;
        }

        private void OnChangeActionPerformed(InputAction.CallbackContext ctx)
        {
            NextItem();
        }

        private void NextItem()
        {
            _activeBuildableIndex = (_activeBuildableIndex + 1) % _buildables.Count;
            _buildingPlacer.SetActiveBuildable(_buildables[_activeBuildableIndex]);
        }
            
    }
}

