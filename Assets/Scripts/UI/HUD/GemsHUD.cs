using TMPro;
using UnityEngine;

namespace BlueRacconGames.UI.HUD
{
    public class GemsHUD : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI gemsValueTxt;

        public void RefreshGemsValue(int value)
        {
            gemsValueTxt.text = value.ToString();
        }
    }
}