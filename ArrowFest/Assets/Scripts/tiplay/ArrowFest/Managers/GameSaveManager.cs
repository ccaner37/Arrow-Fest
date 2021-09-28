using System;
using System.IO;
using tiplay.ArrowFest.Json;
using UnityEngine;

namespace tiplay.ArrowFest.Managers
{
    public class GameSaveManager : MonoBehaviour
    {
        private void OnEnable() => GameManager.DataChanged += SaveToJson;
        private void OnDisable() => GameManager.DataChanged -= SaveToJson;

        private string _path;

        private void Awake()
        {
            _path = Application.persistentDataPath + "/save.json";
            Debug.Log(_path);
            
            if (!File.Exists(_path))
            {
                CreateSaveFile();
            }
        }

        private void SaveToJson(int coins, int level, int ownedArrows, int income)
        {
            JsonObject jsonObject = new JsonObject();
            jsonObject.Coins = coins;
            jsonObject.Level = level;
            jsonObject.OwnedArrows = ownedArrows;
            jsonObject.Income = income;
            
            string json = JsonUtility.ToJson(jsonObject);
            File.WriteAllText(_path, json);
        }

        public JsonObject ReadJson()
        {
            string savedJson = File.ReadAllText(_path);
            JsonObject jsonObject = JsonUtility.FromJson<JsonObject>(savedJson);
            return jsonObject;
        }

        private void CreateSaveFile()
        {
            JsonObject jsonObject = new JsonObject();
            jsonObject.Coins = 0;
            jsonObject.Level = 0;
            jsonObject.OwnedArrows = 1;
            jsonObject.Income = 1;
            
            string json = JsonUtility.ToJson(jsonObject);
            File.WriteAllText(_path, json);
        }
    }
}
