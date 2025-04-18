using System.Linq;
using Game.Item.Factory.Implementation;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts
{
    public class CSVImport
    {
        [MenuItem("CsvImport/Generate/Items")]
        public static void GenerateItems()
        {
            var path = "/Editor/Csv/Items.csv";
            var destinationPath = "Assets/Data/Items/";

            ImportItem(path, destinationPath);
        }
        public static void ImportItem(string path, string destinationPath)
        {
            string[] allLines = File.ReadAllLines(Application.dataPath + path);
            allLines = allLines.Skip(1).ToArray();

            foreach (string line in allLines)
            {
                string[] splitData = line.Split(',');

                var newItem = ScriptableObject.CreateInstance<ResourceItemFactorySO>();

                int id = int.Parse(splitData[0]);
                
                bool isDefault = bool.Parse(splitData[1]);
                string name = splitData[2];
                string description = splitData[3];
                //iconka splitData[4]
                int baseBuyCost = int.Parse(splitData[5]);
                int baseSellCost = int.Parse(splitData[6]);
                int maxStackAmount = int.Parse(splitData[7]);

                newItem.ImportFromCSV(id, isDefault, name, description, null, baseBuyCost, baseSellCost, maxStackAmount);
                AssetDatabase.CreateAsset(newItem, destinationPath + $"{name}.asset");
            }

            AssetDatabase.SaveAssets();
        }
    }
}
