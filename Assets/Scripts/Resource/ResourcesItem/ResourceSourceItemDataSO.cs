using BlueRacconGames.Pool;
using System;
using UnityEngine;

namespace BlueRacconGames.Resources.Data
{
    [Serializable]
    public class ResourceSourceItemDataSO
    {
        [field: SerializeField, Range(0f, 1f)] public float ChanceToSpawnPercent { get; private set; } = 0f;
        [field: SerializeField] public int MinSpawnAmount { get; private set; }
        [field: SerializeField] public int MaxSpawnAmount { get; private set; }
        [field: SerializeField] public ResourceInteractable Prefab { get; private set; }
    }
}
