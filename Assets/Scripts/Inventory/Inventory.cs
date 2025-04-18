using Game.Item.Factory;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BlueRacconGames.InventorySystem
{
    [Serializable]
    public class Inventory
    {
        private readonly string inventoryName;
        [SerializeField] private InventoryItem[] itemLUT;
        private readonly SerializedDictionary<ItemFactorySO, List<int>> itemPositions;

        public event Action<int, InventoryItem> OnItemChangedE;

        public InventoryItem[] ItemLUT => itemLUT;

        public Inventory(InventoryDataSO inventoryData)
        {
            this.inventoryName = inventoryData.InventoryName;

            itemLUT = new InventoryItem[inventoryData.InventoryCount];
            for (int i = 0; i < itemLUT.Length; i++)
                itemLUT[i] = new InventoryItem();

            itemPositions = new();
        }

        public void Resize(int newSize)
        {

        }
        public void ClearInventory()
        {
            int count = 0;
            Dictionary<IItemFactory, List<int>> copy = new();

            foreach (var kvp in itemPositions)
            {
                copy[kvp.Key] = new List<int>(kvp.Value);
            }

            foreach (KeyValuePair<IItemFactory, List<int>> pair in copy)
            {
                count++;

                if (pair.Key == new EmptyItemFactory()) continue;

                List<int> items = pair.Value;

                foreach (int pos in items)
                {
                    EraseItemInPosition(pos);
                }

                if (count == itemLUT.Length) return;
            }
        }
        public bool AddItemAuto(ItemFactorySO item, int amount = 1, bool updateUI = true)
        {
            if (!CheckAcceptance())
                return false;

            InventoryItem newItem = new(item, amount);

            if (!itemPositions.ContainsKey(newItem.ItemFactory))
                return AddLinearly(newItem, newItem.Amount);

            for (int i = 0; i < itemPositions[newItem.ItemFactory].Count; i++)
            {
                int position = itemPositions[newItem.ItemFactory][i];
                InventoryItem curItem = itemLUT[position];
                if (curItem.MaxStackAmount >= curItem.Amount)
                {
                    int diff = curItem.MaxStackAmount - curItem.Amount;
                    if (newItem.Amount > diff)
                    {
                        curItem.SetAmount(itemLUT[position].Amount + diff);
                        newItem.SetAmount(newItem.Amount - diff);
                    }
                    else
                    {
                        curItem.SetAmount(itemLUT[position].Amount + newItem.Amount);

                        if(updateUI)
                            OnItemChangedE?.Invoke(position, curItem);

                        return true;

                    }

                    if (updateUI)
                        OnItemChangedE?.Invoke(position, curItem);
                }
            }
             return AddLinearly(newItem, newItem.Amount);
        }
        public void AddItemPos(InventoryItem item, int pos)
        {
            if (itemLUT == null) return;

            if (!CheckAcceptance()) return;

            InventoryItem newItem = new(item, item.Amount);
            InventoryItem curItem = itemLUT[pos];

            if (curItem.IsNullOrEmpty())
            {
                AddItemHelper(newItem, pos);
                return;
            }

            if(curItem.ItemFactory != newItem.ItemFactory)
            {
                AddItemAuto(newItem.ItemFactory, newItem.Amount);
                return;
            }

            int newAmount = curItem.Amount + newItem.Amount;

            if (newAmount <= curItem.MaxStackAmount)
            {
                curItem.SetAmount(newAmount);
                OnItemChangedE?.Invoke(pos, curItem);
                return;
            }

            AddItemAuto(newItem.ItemFactory, newItem.Amount);
        }
        public void EraseItemInPosition(int pos)
        {
            RemoveItemHelper(itemLUT[pos], pos);
        }
        public void RemoveItemInPosition(int pos, int amount)
        {
            InventoryItem item = itemLUT[pos];

            if (item.IsNullOrEmpty()) return;

            if (!itemPositions.ContainsKey(item.ItemFactory))
            {
                Debug.LogError("ItemPositions Dictitonary Setup Incorrectly");
                return;
            }

            var newAmount = item.Amount - amount;
            if (newAmount <= 0)
            {
                RemoveItemHelper(item, pos);
                return;
            }

            item.SetAmount(newAmount);
            OnItemChangedE?.Invoke(pos, item);
        }
        public void RemoveItemInPosition(InventoryItem item, int amount)
        {
            int pos = item.SlotId;

            if (item.IsNullOrEmpty()) return;

            if (!itemPositions.ContainsKey(item.ItemFactory))
            {
                Debug.LogWarning("ItemPositions Dictitonary Setup Incorrectly");
                return;
            }

            var newAmount = item.Amount - amount;

            if (newAmount <= 0)
            {
                RemoveItemHelper(item, pos);
                return;
            }

            item.SetAmount(newAmount);
            OnItemChangedE?.Invoke(pos, item);
        }
        public void RemoveItemAuto(ItemFactorySO item, int amount)
        {
            if (!itemPositions.ContainsKey(item))
            {
                Debug.Log("No items of type " + item + " in " + inventoryName);
                return;
            }

            List<int> positions = itemPositions[item];
            List<int> delpos = new();
            int trackAmount = amount;

            foreach (int position in positions)
            {
                InventoryItem curItem = itemLUT[position];
                if (trackAmount - curItem.Amount < 0)
                {
                    curItem.SetAmount(itemLUT[position].Amount - trackAmount);
                    OnItemChangedE?.Invoke(position, curItem);
                    break;
                }
                trackAmount -= curItem.Amount;
                delpos.Add(position);
            }

            foreach (int position in delpos)
            {
                RemoveItemHelper(item, position);
            }
        }
        public InventoryItem GetItem(int pos)
        {
            if (!CheckVeriableToAdd(pos)) return null;

            return itemLUT[pos];
        }
        public InventoryItem GetItem(ItemFactorySO itemFactory)
        {
            return Array.Find(itemLUT, x => x.ItemFactory == itemFactory);
        }
        public bool IsFull(ItemFactorySO item)
        {
            EmptyItemFactorySO empty = new();
            if (item == null) return true;
            if (itemPositions[empty].Count > 0) return false;

            if (!itemPositions.ContainsKey(item))
            {
                return true;
            }
            List<int> itemPos = itemPositions[item];

            foreach (int pos in itemPos)
            {
                InventoryItem curItem = itemLUT[pos];
                if (curItem.Amount < curItem.MaxStackAmount)
                {
                    return false;
                }

            }
            return true;
        }
        public int Count(ItemFactorySO item)
        {
            if (item == null || !itemPositions.ContainsKey(item))
            {
                Debug.LogError($"Any Error with: { item }. Returning 0!!");
                return 0;
            }

            List<int> itemsPosition = itemPositions[item];
            int itemsTotal = 0;

            foreach (int pos in itemsPosition)
            {
                itemsTotal += itemLUT[pos].Amount;
            }

            return itemsTotal;
        }
        public InventoryItem[] ExportInventory()
        {
            int lenght = 0;

            for (int i = 0; i < itemLUT.Length; i++)
            {
                if (itemLUT[i].IsNullOrEmpty()) continue;

                lenght++;
            }

            InventoryItem[] itemsWithoutNull = new InventoryItem[lenght];

            var posInArray = 0;

            for (int i = 0; i < itemLUT.Length; i++)
            {
                if (itemLUT[i].IsNullOrEmpty()) continue;

                itemsWithoutNull[posInArray] = itemLUT[i];
                posInArray++;
            }

            return itemsWithoutNull;
        }


        private bool CheckVeriableToAdd(int pos)
        {
            if (itemLUT == null || pos > itemLUT.Length - 1 || pos < 0) return false;

            return true;
        }
        private bool AddLinearly(InventoryItem item, int amount = 1)
        {
            int trackAmount = amount;

            bool result = true;

            for (int i = 0; i < itemLUT.Length; i++)
            {
                if (!itemLUT[i].IsNullOrEmpty()) continue;

                if (item.MaxStackAmount < trackAmount)
                {
                    InventoryItem newItem = new(item, item.MaxStackAmount);
                    AddItemHelper(newItem, i);
                    trackAmount -= item.MaxStackAmount;

                }
                else
                {
                    InventoryItem newItem = new(item, trackAmount);
                    AddItemHelper(newItem, i);
                    trackAmount -= amount;

                }
                if (trackAmount <= 0)
                {
                    return true;
                }
            }

            return result;
        }
        private void AddItemHelper(InventoryItem item, int pos)
        {
            EmptyItemFactorySO empty = new();

            if (item.IsNullOrEmpty())
            {
                if (!itemPositions.ContainsKey(empty))
                {
                    itemPositions.Add(empty, new List<int>() { pos });
                }
                else
                {
                    itemPositions[empty].Add(pos);
                }
            }

            itemLUT[pos] = item;
            item.SetPosition(pos);

            if (!itemPositions.ContainsKey(item.ItemFactory))
            {
                if (itemPositions.ContainsKey(empty))
                {
                    itemPositions[empty].Remove(pos);
                }

                itemPositions.Add(item.ItemFactory, new List<int>());
                itemPositions[item.ItemFactory].Add(pos);

                OnItemChangedE?.Invoke(pos, item);
                return;
            }

            if (itemPositions.ContainsKey(empty))
            {
                itemPositions[empty].Remove(pos);
            }

            itemPositions[item.ItemFactory].Add(pos);

            OnItemChangedE?.Invoke(pos, item);
            /*
            if (invokeEnterExit && enterDict != null && enterDict.ContainsKey(pos))
            {
                if (itemAction[pos])
                {
                    newItem.Selected();
                }
                enterDict[pos].Invoke(newItem);
            }
            */
        }
        private void RemoveItemHelper(InventoryItem item, int pos)
        {
            EmptyItemFactorySO empty = new();

            if (!itemPositions.ContainsKey(item.ItemFactory)) return;

            itemPositions[item.ItemFactory].Remove(pos);

            if (itemPositions.ContainsKey(empty))
                itemPositions[empty].Add(pos);

            InventoryItem newItem = new();

            itemLUT[pos] = newItem;

            OnItemChangedE?.Invoke(pos, newItem);

            /*
            if (invokeEnterExit && exitDict != null && exitDict.ContainsKey(pos))
            {
                exitDict[pos].Invoke(inventoryList[pos]);
            }
            InventoryUIManagerInstance.UpdateSlot(pos);
            */
        }
        private void RemoveItemHelper(ItemFactorySO item, int pos)
        {
            EmptyItemFactorySO empty = new();

            if (!itemPositions.ContainsKey(item)) return;

            itemPositions[item].Remove(pos);

            if (itemPositions.ContainsKey(empty))
                itemPositions[empty].Add(pos);

            InventoryItem newItem = new();

            itemLUT[pos] = newItem;

            OnItemChangedE?.Invoke(pos, newItem);

            /*
            InventoryUIManagerInstance.UpdateSlot(pos);
            if (invokeEnterExit && exitDict != null && exitDict.ContainsKey(pos))
            {
                exitDict[pos].Invoke(inventoryList[pos]);
            }
            InventoryUIManagerInstance.UpdateSlot(pos);
            */
        }
        private bool CheckAcceptance()
        {
            return true;
        }
    }
}