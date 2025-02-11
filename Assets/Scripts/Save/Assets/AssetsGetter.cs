using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Saves
{
    public static class AssetsGetter
    {
        private const string AssetsPath = "SaveAssetsContainer";

        private static SaveAssetsContainer assetsCache;

        private static SaveAssetsContainer Assets
        {
            get
            {
                if (assetsCache != null) return assetsCache;
                assetsCache = (SaveAssetsContainer)Resources.Load(AssetsPath, typeof(SaveAssetsContainer));
                assetsCache.GenerateSerializableDictionary();
                return assetsCache;
            }
        }

        public static T GetAsset<T>() where T : SaveAsset
        {
            if (Assets.LUT.ContainsKey(typeof(T).ToString()))
            {
                return (T)Assets.LUT[typeof(T).ToString()][0];
            }

            return null;
        }
        public static List<T> GetAssets<T>() where T : SaveAsset
        {
            if (Assets.LUT.ContainsKey(typeof(T).ToString()))
            {
                return Assets.LUT[typeof(T).ToString()].Cast<T>().ToList();
            }

            return null;
        }
    }
}