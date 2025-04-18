using BlueRacconGames.InventorySystem;
using Game.Item.Factory;
using UnityEngine;
using UnityEngine.UI;

namespace BlueRacconGames.Crafting 
{
    public class OutputCraftSlot : MonoBehaviour
    {
        [SerializeField] protected DraggableInventoryOutputItem draggableItem;
        [SerializeField] private Sprite unknowItemIcon;

        public void Init()
        {
            draggableItem.Init(transform);
        }

        public void UpdateSlot(bool recipeKnown, ItemFactorySO item)
        {
            if (draggableItem.IsDragging) return;

            if(item == null)
            {
                draggableItem.ClearItem();
                return;
            }

            var icon = recipeKnown ? item.Icon : unknowItemIcon;

            draggableItem.UpdateItem(icon, 1);
        }
    }
}