using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using BuildingSystem.Models;

namespace BuildingSystem
{
    public class ConsructionLayer : TilemapLayer
    {
        private Dictionary<Vector3Int, Buildable> _buildables = new();
        public void Build(Vector3 worldCoords, BuildableTower item)
        {
            GameObject itemObject = null;
            var coords = _tilemap.WorldToCell(worldCoords);
            if (item.Tile != null)
            {
                var tileChangeData = new TileChangeData(
                    coords,
                    item.Tile,
                    Color.white, 
                    Matrix4x4.Translate(item.TileOffset)
                    );
                _tilemap.SetTile(tileChangeData, false);
            }

            if (item.GameObject != null)
            {
                itemObject = Instantiate(
                    item.GameObject,
                    _tilemap.CellToWorld(coords) + _tilemap.cellSize / 2 + item.TileOffset,
                    Quaternion.identity
                    );
            }
            var buildable = new Buildable(item, coords, _tilemap, itemObject);
            _buildables.Add(coords, buildable);
        }

        public void Destroy(Vector3 worldCoords)
        {
            var coords = _tilemap.WorldToCell(worldCoords);
            if (!_buildables.ContainsKey(coords)) return;

            var buildable = _buildables[coords];
            _buildables.Remove(coords);
            buildable.Destroy();
        }

        public bool IsEmpty(Vector3 worldCoords)
        {
            var coords = _tilemap.WorldToCell(worldCoords);
            return !_buildables.ContainsKey(coords) && _tilemap.GetTile(coords) == null;
        }
    }
}

