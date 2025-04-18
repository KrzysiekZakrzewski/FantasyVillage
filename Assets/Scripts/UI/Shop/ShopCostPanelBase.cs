using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.UI.Shop
{
    public class ShopCostPanelBase : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI totalCostTxt;
        [SerializeField] private TextMeshProUGUI totalAmountTxt;
        [SerializeField] private Button plusButton, minusButton;

        public void SetupButtonAction(UnityAction plusButtonAction, UnityAction minusButtonAction)
        {
            plusButton.onClick.AddListener(plusButtonAction);
            minusButton.onClick.AddListener(minusButtonAction);
        }

        public void UpdatePanel(int totalAmount, int totalCost)
        {
            totalAmountTxt.text = totalAmount.ToString();
            totalCostTxt.text = totalCost.ToString();
        }
    }
}
