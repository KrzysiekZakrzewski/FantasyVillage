using BlueRacconGames.Inventory;
using Game.Item.Factory;
using System;
using UnityEngine;
using Zenject;

namespace BlueRacconGames.Equipment
{
    public class EquipmentManager : MonoBehaviour
    {
        private EquipmentFactorySO[] currentEquipment;

        private InventoryManager inventory;

        public event Action<EquipmentFactorySO, EquipmentFactorySO> OnEquipmentChanged;

        [Inject]
        private void Inject(InventoryManager inventory)
        {
            this.inventory = inventory;
        }

        private void Start()
        {
            int numSlots = System.Enum.GetNames(typeof(EquipmentFactorySO)).Length;
            currentEquipment = new EquipmentFactorySO[numSlots];
        }

        public void Equip(EquipmentFactorySO newItem)
        {
            int slotIndex = (int)newItem.EquipmentSlot;

            EquipmentFactorySO oldItem = null;

            if (currentEquipment[slotIndex] != null) 
            {
                oldItem = currentEquipment[slotIndex];
                inventory.Add(oldItem);
            }

            OnEquipmentChanged?.Invoke(newItem, oldItem);

            currentEquipment[slotIndex] = newItem;
        }

        public void Unequip(int slotIndex) 
        {
            if (currentEquipment[slotIndex] == null) return;

            EquipmentFactorySO oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);

            OnEquipmentChanged?.Invoke(null, oldItem);

            currentEquipment[slotIndex] = null;
        }

        public void UnequipAll()
        {
            for (int i = 0; i < currentEquipment.Length; i++)
            {
                Unequip(i);
            }
        }
    }
}