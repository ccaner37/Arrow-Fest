using System;
using tiplay.ArrowFest.Controllers;
using UnityEngine;
using UnityEngine.UI;

namespace tiplay.ArrowFest.Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        private Text _arrowCountText;

        [SerializeField]
        private Text _levelText;

        [SerializeField]
        private Text _levelCompletedText;

        [SerializeField]
        private Text _levelFailedText;
        
        [SerializeField]
        private Text _coinText;

        private void OnEnable() => PlayerArrowController.ArrowsChanged += UpdateArrowCountText;

        private void OnDisable() => PlayerArrowController.ArrowsChanged -= UpdateArrowCountText;

        private void UpdateArrowCountText(int arrowCount)
        {
            _arrowCountText.text = "Arrows: " + arrowCount.ToString();
        }

        public void LevelCompletedUI()
        {
            _levelCompletedText.gameObject.SetActive(true);
        }

        public void LevelFailedUI()
        {
            _levelFailedText.gameObject.SetActive(true);
        }
        
        public void UpdateCoinText(int coins)
        {
            _coinText.text = coins.ToString();
        }

        public void UpdateLevelText(int level)
        {
            _levelText.text = "Level: " + (level + 1).ToString();
        }
    }
}
