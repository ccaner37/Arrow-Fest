using System;
using System.Collections;
using tiplay.ArrowFest.Controllers;
using tiplay.ArrowFest.Json;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

namespace tiplay.ArrowFest.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private UIManager _uiManager;

        [SerializeField]
        private ArrowMovementController _arrowMovementController;

        [SerializeField]
        private PlayerArrowController _arrowController;

        [SerializeField]
        private Canvas _shopCanvas;
        
        public float CoinMultiplier = 1f;

        public bool IsReachedEnd;

        public bool IsGameStarted;
        
        private static bool _isSceneLoaded;

        private int _coins;
        private int _arrowPrice;
        private int _incomePrice;

        public int Level;
        public int OwnedArrows;
        public int Income;

        public delegate void OnDataChanged(int coins, int level, int ownedArrows, int income);
        public static event OnDataChanged DataChanged;
        
        public static GameManager Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            ReadJsonData();
        }

        private void ReadJsonData()
        {
            JsonObject jsonObject = gameObject.GetComponent<GameSaveManager>().ReadJson();
            _coins = jsonObject.Coins;
            Level = jsonObject.Level;
            OwnedArrows = jsonObject.OwnedArrows;
            Income = jsonObject.Income;

            if (!_isSceneLoaded)
            {
                LoadScene(Level);
                _isSceneLoaded = true;
            }
            _uiManager.UpdateCoinText(_coins);
            _uiManager.UpdateLevelText(Level);
        }

        public IEnumerator RestartScene()
        {
            StopArrow();
            IsGameStarted = false;
            _uiManager.LevelFailedUI();
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public IEnumerator LevelCompleted()
        {
            int arrowsReward = Mathf.RoundToInt((_arrowController._arrowCount + 50) * 0.10f);
            _coins += (int)(CoinMultiplier * arrowsReward) + (Income * arrowsReward);
            Level += 1;
            _uiManager.UpdateCoinText(_coins);
            _uiManager.LevelCompletedUI();
            StopArrow();
            UpdateData();
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        
        public void UpdateData()
        {
            DataChanged(_coins, Level, OwnedArrows, Income);
        }

        public void StopArrow()
        {
            _arrowMovementController._speedModifier = 0f;
        }

        private void LoadScene(int levelIndex)
        {
            SceneManager.LoadScene(levelIndex);
        }
        
        public void BuyArrow()
        {
            int arrowPrice = CalculateArrowPrice();
            if (_coins >= arrowPrice)
            {
                OwnedArrows += 1;
                _arrowController.AddArrows(1);
                _coins -= arrowPrice;
                _uiManager.UpdateCoinText(_coins);
                UpdateData();
            }
        }

        public int CalculateArrowPrice()
        {
            _arrowPrice = (Level * 20) + (OwnedArrows * 100) + OwnedArrows * Level;
            return _arrowPrice;
        }
        
        public void BuyIncome()
        {
            int incomePrice = CalculateIncomePrice();
            if (_coins >= incomePrice)
            {
                Income += 1;
                _coins -= incomePrice;
                _uiManager.UpdateCoinText(_coins);
                UpdateData();
            }
        }

        public int CalculateIncomePrice()
        {
            _incomePrice = (Level * 20) + (Income * 100) + Income * Level;
            return _incomePrice;
        }

        public void HideShopCanvas()
        {
            _shopCanvas.gameObject.SetActive(false);
        }
    }
}
