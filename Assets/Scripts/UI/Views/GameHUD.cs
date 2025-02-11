using ViewSystem.Implementation;
using UnityEngine;
using BlueRacconGames.UI.Bars;
using Interactable.UI;
using BlueRacconGames.UI.HUD;

namespace BlueRacconGames.View 
{
    public class GameHUD : BasicView
    {
        [field: SerializeField]
        public HealthBar PlayerHealthBar { get; private set; }
        [field: SerializeField]
        public GemsHUD GemsHUD { get; private set; }
        [field: SerializeField]
        public InteractionPromptUI InteractionPromptUI { get; private set; }

        public override bool Absolute => false;
    }
}