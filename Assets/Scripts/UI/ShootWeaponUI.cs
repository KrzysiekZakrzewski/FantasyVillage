using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class ShootWeaponUI : MonoBehaviour
    {
        [SerializeField]
        private Image weaponImage;
        [SerializeField]
        private Image ammoImage;
        [SerializeField]
        private TextMeshProUGUI ammoAmountTxt;

        public void SetupWeapon(Sprite weaponIcon, Sprite ammoIcon)
        {
            weaponImage.sprite = weaponIcon;
            ammoImage.sprite = ammoIcon;
        }
    }
}