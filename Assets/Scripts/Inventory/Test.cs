using BlueRacconGames.InventorySystem;
using Game.Item.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.InventoryNew
{
    public class Test : MonoBehaviour
    {
        [SerializeField] private InventoryController controllerNew;
        [SerializeField] private ItemFactorySO testItem;
        [SerializeField] private InventoryUniqueId uniqueId;
        public void AddItem()
        {
            controllerNew.AddItem(uniqueId, testItem, 1);
        }
    }
}
