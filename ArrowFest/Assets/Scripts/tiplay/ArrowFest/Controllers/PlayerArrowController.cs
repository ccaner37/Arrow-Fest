using System;
using UnityEngine;
using tiplay.ArrowFest.Managers;
using tiplay.ArrowFest.Enum;

namespace tiplay.ArrowFest.Controllers
{
    public class PlayerArrowController : MonoBehaviour
    {
        [SerializeField]
        private Transform _arrow;

        private float _radius = 0.01f;
        private float _r = 12f;
        
        [HideInInspector]
        public int _arrowCount;
        
        private int _maxArrowCount = 199;
        private int _nextArrowCount;
        
        private bool _isChecked;

        public delegate void OnArrowsChanged(int arrowCount);

        public static event OnArrowsChanged ArrowsChanged;


        void Start()
        {
            _arrowCount = GameManager.Instance.OwnedArrows;
            SpawnOwnedArrows();
        }

        private void SpawnOwnedArrows()
        {
            for (int i = 0; i < _arrowCount; i++)
            {
                Vector3 rotation = new Vector3(-90f, transform.rotation.y, transform.rotation.z);
                Transform arrow = Instantiate(_arrow, transform.position, _arrow.rotation, transform);
                arrow.localEulerAngles = rotation;

                float theta = i * _r * Mathf.PI / _maxArrowCount;
                float x = Mathf.Sin(theta) * _radius;
                float y = Mathf.Cos(theta) * _radius;
            
                arrow.transform.position = new Vector3(x, y, 0) + transform.position;

                if (i == 0)
                {
                    arrow.transform.position = new Vector3(x, 0.002f, 0f) + transform.position;
                }

                if (i % 33 == 0)
                {
                    _radius += 0.03f;
                }
                
                ArrowsChanged(_arrowCount);
            }
        }
        
        public void AddArrows(int newArrowsCount)
        {
            int nextArrowCount = _arrowCount + newArrowsCount;

            for (int i = _arrowCount; i < nextArrowCount; i++)
            {
                if (_arrowCount >= _maxArrowCount)
                {
                    _arrowCount = nextArrowCount;
                    ArrowsChanged(_arrowCount);
                    return;
                }
                
                Vector3 rotation = new Vector3(-90f, transform.rotation.y, transform.rotation.z);
                Transform arrow = Instantiate(_arrow, transform.position, _arrow.rotation, transform);
                arrow.localEulerAngles = rotation;

                float theta = i * _r * Mathf.PI / _maxArrowCount;
                float x = Mathf.Sin(theta) * _radius;
                float y = Mathf.Cos(theta) * _radius;

                transform.localScale = Vector3.one;
                arrow.transform.position = new Vector3(x, y, 0) + transform.position;

                if (i % 33 == 0)
                {
                    _radius += 0.03f;
                }
                _arrowCount++;
            }
            
            ArrowsChanged(_arrowCount);
        }

        public void RemoveArrows(int removeArrowsCount)
        {
            _nextArrowCount = _arrowCount - removeArrowsCount;

            CheckGameEnded();

            for (int i = _arrowCount; i > _nextArrowCount; i--)
            {
                if (_arrowCount >= _maxArrowCount)
                {
                    if (_nextArrowCount >= _maxArrowCount)
                    {
                        _arrowCount = _nextArrowCount; 
                        ArrowsChanged(_arrowCount);
                        return;
                    }
                    else
                    {
                        _arrowCount--;
                        ArrowsChanged(_arrowCount);
                        continue;
                    }
                }
                
                Destroy(transform.GetChild(i-1).gameObject);

                if (i % 33 == 0)
                {
                    _radius -= 0.03f;
                }
                _arrowCount--;
            }
            
            ArrowsChanged(_arrowCount);
        }

        private void CheckGameEnded()
        {
            if (_nextArrowCount <= 0 && GameManager.Instance.IsReachedEnd == false && !_isChecked)
            {
                StartCoroutine(GameManager.Instance.RestartScene());
                _isChecked = true;
                RemoveAllArrows();
            }
            else if (_nextArrowCount <= 0 && GameManager.Instance.IsReachedEnd == true && !_isChecked)
            {
                StartCoroutine(GameManager.Instance.LevelCompleted());
                _isChecked = true;
                RemoveAllArrows();
            }
        }

        private void RemoveAllArrows()
        {
            RemoveArrows(_arrowCount);
        }

        public void HandleOperation(int operationNumber, Operators operators)
        {
            switch (operators)
            {
                case Operators.Addition:
                    AddArrows(operationNumber);
                    break;
                case Operators.Subtraction:
                    RemoveArrows(operationNumber);
                    break;
                case Operators.Division:
                    int dividedNumber = _arrowCount / operationNumber;
                    RemoveArrows(_arrowCount - dividedNumber);
                    break;
                case Operators.Multiplication:
                    AddArrows((_arrowCount * operationNumber) - _arrowCount);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(operators), operators, null);
            }
        }
    }
}
