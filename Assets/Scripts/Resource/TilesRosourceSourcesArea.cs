using Assets.Scripts.Unity.Tilemaps;
using BlueRacconGames.Pool;
using BlueRacconGames.Resources.Data;
using System.Collections;
using System.Linq;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace BlueRacconGames.Resources
{
    public class TilesRosourceSourcesArea : ResourcesSourceAreaBase
    {
        [SerializeField] private Tilemap area;

        protected override void Awake()
        {
            base.Awake();

            CacheAllTiles();

            StartCoroutine(Test());
        }

        protected override Vector2Int GetSpawnPosition()
        {
            return avaiablePositions[Random.Range(0, avaiablePositions.Count)];
        }

        protected override void UpdateAvaiablePositions(Vector2Int position)
        {
            for(int x = position.x - 1; x <= position.x + 1; x++)
            {
                for (int y = position.y - 1; y <= position.y + 1; y++)
                {
                    var positionToRemove = new Vector2Int(x, y);

                    if (!avaiablePositions.Contains(positionToRemove)) continue;

                    avaiablePositions.Remove(positionToRemove);
                }
            }
        }

        protected override ResourceSourceSpawnDataSO GetRandomResourceSource()
        {
            var randomizeValue = Random.value;

            ResourceSourceSpawnDataSO resourcesData = null;

            for(int i = 0; i < resourceSourcesDatas.Length; i++)
            {
                var tempData = resourceSourcesDatas[i];

                if (tempData.ChanceToSpawnPercent < randomizeValue) break;

                resourcesData = tempData;
            }

            return resourcesData;
        }

        private void CacheAllTiles()
        {
            avaiablePositions = area.GetAllTilesPosition().ToList();
        }

        private IEnumerator Test()
        {
            yield return new WaitForSeconds(2f);

            while (resourceSourcesLUT.Count < maxResourcesSourceAmount)
            {
                SpawnResouceSource();

                yield return new WaitForSeconds(1f);
            }
        }
    }
}
