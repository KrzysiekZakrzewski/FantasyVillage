using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Unity.Tilemaps
{
    public static class TileMapExtensionMethods
    {
        //private static Vector2 offSetToCenter = new Vector2(0.5f, 0.5f);
        public static IEnumerable<Vector2Int> GetAllTilesPosition(this Tilemap tilemap)
        {
            var bounds = tilemap.cellBounds;
            for(int x = bounds.min.x; x < bounds.max.x; x++)
            {
                for (int y = bounds.min.y; y < bounds.max.y; y++)
                {
                    var cellPosition = new Vector3Int(x, y, 0);
                    var sprite = tilemap.GetSprite(cellPosition);
                    var tile = tilemap.GetTile(cellPosition);
                    if (tile == null && sprite == null)
                        continue;

                    Vector2Int tilePosition = new (x, y);

                    yield return tilePosition;
                }
            }
        }
    }
}
