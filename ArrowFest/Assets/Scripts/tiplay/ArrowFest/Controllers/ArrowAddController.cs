using System.Collections;
using tiplay.ArrowFest.Enum;
using UnityEngine;

namespace tiplay.ArrowFest.Controllers
{
    public class ArrowAddController : MonoBehaviour
    {
        [SerializeField]
        private int _operationNumber;
        
        private float _delay = 0.01f;
        
        [SerializeField]
        private Operators _operators;

        private TriggerUIController _triggerUIController;

        private void Awake()
        {
            _triggerUIController = gameObject.GetComponent<TriggerUIController>();
            _triggerUIController.Operator = _operators;
            _triggerUIController.OperationNumberUI = _operationNumber;
        }
        
        IEnumerator OnTriggerEnter(Collider other)
        {
            yield return new WaitForSeconds(_delay);
            other.transform.GetComponent<PlayerArrowController>()
              .HandleOperation(_operationNumber, _operators);
        }
    }
}
