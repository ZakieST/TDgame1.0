using BuildingSystem;
using BuildingSystem.Models;
using UnityEngine;

namespace BuildingSystem
{
    public class PreviewLayer : TilemapLayer
    {
        [SerializeField] 
        private SpriteRenderer _prviewRenderer;

        public void ShowPreview(BuildableTower item, Vector3 worldCoords, bool isValid)
        {
            var coords = _tilemap.WorldToCell(worldCoords);
            _prviewRenderer.enabled = true;
            _prviewRenderer.transform.position =
                _tilemap.CellToWorld(coords) +
                _tilemap.cellSize / 2 + item.TileOffset;
            _prviewRenderer.sprite = item.PreviewSprite;
            _prviewRenderer.color = isValid ? new Color(0, 1, 0, 0.5f) : new Color(1, 0, 0, 0.75f);
        }

        public void ClearPreview()
        {
            _prviewRenderer.enabled = false;
        }
    }
}

