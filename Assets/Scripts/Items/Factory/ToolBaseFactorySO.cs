using Assets.Scripts.Events;
using BlueRacconGames.Hit.Data;
using BlueRacconGames.HitEffect.Factory;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Item.Factory.Implementation
{
    public abstract class ToolBaseFactorySO : ItemFactorySO
    {
        [field: SerializeField] public int HitPoint { get; private set; }
        [field: SerializeField] public string UseAnimation { get; private set; }
        [field: SerializeField] public List<HitEffectFactorySO> ToolTargetEffectFactory { get; private set; }
        [field: SerializeField] public BoolGameEvent TurnOnCanMoveEvent { get; private set; }
        [field: SerializeField] public BoolGameEvent TurnOffCanMoveEvent { get; private set; }
        [field: SerializeField] public ToolHitTargetValidatorFactorySO ValidatorFactory { get; private set; }
    }   
}