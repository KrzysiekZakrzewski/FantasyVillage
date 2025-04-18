using System.Collections;
using UnityEngine;

namespace RDG.Platforms
{
    public class CorutineSystem : MonoBehaviour
    {
        private static GameObject corutineSystemGameObject;
        private static CorutineSystemObject corutineSystem;
        public static void Create()
        {
            if (corutineSystemGameObject != null)
                return;

            corutineSystemGameObject = new GameObject("CorutineSystemObject");
            corutineSystem = corutineSystemGameObject.AddComponent<CorutineSystemObject>();
        }

        public static void StartSequnce(IEnumerator enumerator)
        {
            if (corutineSystemGameObject == null)
                Create();

            corutineSystem.StartCoroutine(enumerator);
        }

        private class CorutineSystemObject : MonoBehaviour
        {
            
        }
    }
}