using Saves.Serializiation;
using System.Collections.Generic;
using UnityEngine;

namespace Saves
{
    [CreateAssetMenu(fileName = nameof(SaveAssetsContainer), menuName = nameof(Saves) + "/" + "Assets" + "/" + nameof(SaveAssetsContainer))]
    public class SaveAssetsContainer : SaveAsset
    {
        [SerializeField]
        private List<SaveAsset> saveAssets = new List<SaveAsset>();

        [SerializeField]
        [HideInInspector]
        public SerializableDictionary<string, List<SaveAsset>> LUT { private set; get; }

        public void GenerateSerializableDictionary()
        {
            if(saveAssets.Count == 0) return;

            LUT = new SerializableDictionary<string, List<SaveAsset>>();

            LUT.Clear();

            for (int i = 0; i < saveAssets.Count; i++)
            {
                var type = saveAssets[i].GetType().ToString();

                if (!LUT.ContainsKey(type))
                {
                    LUT.Add(type, new List<SaveAsset>() { saveAssets[i] });
                    continue;
                }

                LUT.TryGetValue(type, out var value);
                value.Add(saveAssets[i]);
            }
        }
    }
}
