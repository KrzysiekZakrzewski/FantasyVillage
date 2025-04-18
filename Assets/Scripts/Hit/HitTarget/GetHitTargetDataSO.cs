using BlueRacconGames.Hit.Data;
using BlueRacconGames.HitEffect.Factory;
using System.Collections.Generic;
using UnityEngine;

namespace BlueRacconGames.HitEffect.Data
{
    public abstract class GetHitTargetDataSO : ScriptableObject
    {
        [field: SerializeField] public HitTargetType TargetType { get; private set; }
        [field: SerializeField] public List<HitEffectFactorySO> GetHitTargetEffectFactory { get; private set; }
    }
}