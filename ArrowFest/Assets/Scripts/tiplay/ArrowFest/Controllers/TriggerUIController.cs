using System;
using tiplay.ArrowFest.Enum;
using UnityEngine;
using UnityEngine.UI;

namespace tiplay.ArrowFest.Controllers
{
    public class TriggerUIController : MonoBehaviour
    {
        [SerializeField]
        private Text _arrowCountText;

        private string _operatorString;
        
        [HideInInspector]
        public int OperationNumberUI;

        [HideInInspector]
        public Operators Operator;

        void Start()
        {
            ImplementOperationToUI();
        }

        private void ImplementOperationToUI()
        {
            switch (Operator)
            {
                case Operators.Addition:
                    _operatorString = "+";
                    break;
                case Operators.Subtraction:
                    _operatorString = "-";
                    break;
                case Operators.Division:
                    _operatorString = "÷";
                    break;
                case Operators.Multiplication:
                    _operatorString = "x";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            _arrowCountText.text = _operatorString + OperationNumberUI.ToString();
        }
    }
}
