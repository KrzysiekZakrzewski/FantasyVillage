using ViewSystem.Implementation;
using UnityEngine;
using BlueRacconGames.UI.Bars;
using Interactable.UI;
using BlueRacconGames.UI.HUD;
using Game.View;
using BlueRacconGames.InventorySystem;

namespace BlueRacconGames.View 
{
    public class GameHUD : BasicView
    {
        [field: SerializeField] public HealthBar PlayerHealthBar { get; private set; }
        [field: SerializeField] public MoneyHUD GemsHUD { get; private set; }
        [field: SerializeField] public InteractionPromptUI InteractionPromptUI { get; private set; }
        [field: SerializeField] public ToolbarInventoryView ToolbarInventoryView { get; private set; }

        public override bool Absolute => false;
    }
}