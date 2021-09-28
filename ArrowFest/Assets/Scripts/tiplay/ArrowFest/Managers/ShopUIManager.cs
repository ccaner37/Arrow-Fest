using System;
using UnityEngine;
using UnityEngine.UI;

namespace tiplay.ArrowFest.Managers
{
    public class ShopUIManager : MonoBehaviour
    {
        [SerializeField]
        private Text _arrowLevelText;

        [SerializeField]
        private Text _arrowPriceText;

        [SerializeField]
        private Text _incomeLevelText;
        
        [SerializeField]
        private Text _incomePriceText;

        private void Start()
        {
            UpdateShopCanvas();
        }

        public void UpdateShopCanvas()
        {
            int arrowPrice = GameManager.Instance.CalculateArrowPrice();
            int incomePrice = GameManager.Instance.CalculateIncomePrice();

            _arrowLevelText.text = "Arrow Level: " + GameManager.Instance.OwnedArrows.ToString();
            _arrowPriceText.text = arrowPrice.ToString();

            _incomeLevelText.text = "Income Level: " + GameManager.Instance.Income.ToString();
            _incomePriceText.text = incomePrice.ToString();
        }
    }
}
