using System;
using tiplay.ArrowFest.Managers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace tiplay.ArrowFest.Controllers
{
    public class ArrowsPositionController : MonoBehaviour
    {
        private Vector3 _targetPos;

        private float _speed = 5.0f;
        private float _boundsPos = 0.3f;
        private float _distance;
        private float _arrowSpeed = 0.09f;

        void Start()
        {
            _targetPos = transform.position;
        }
        private void Update()
        {
            CalculateMousePosition();
            StartArrowMove();
            MoveArrowsPosition();
        }

        private void LateUpdate()
        {
            CheckPositionBounds();
        }

        private void CalculateMousePosition()
        {
            _distance = transform.position.z - Camera.main.transform.position.z;
            _targetPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _distance);
            _targetPos = Camera.main.ScreenToWorldPoint(_targetPos);
        }

        private void MoveArrowsPosition()
        {
            if (Input.GetMouseButton(0) && GameManager.Instance.IsGameStarted)
            {
                Vector3 followXonly = new Vector3(_targetPos.x, transform.position.y, transform.position.z);
                transform.position = Vector3.Lerp(transform.position, followXonly, _speed * Time.deltaTime);
            }
        }
        
        private void StartArrowMove()
        {
            if (Input.GetMouseButtonDown(0) && !GameManager.Instance.IsGameStarted && EventSystem.current.currentSelectedGameObject == null)
            {
                GameManager.Instance.IsGameStarted = true;
                GameManager.Instance.HideShopCanvas();
                transform.parent.GetComponent<ArrowMovementController>()._speedModifier = _arrowSpeed;
            }
        }
        
        private void CheckPositionBounds()
        {
            Vector3 arrowTransform = transform.localPosition;
            
            if (arrowTransform.x < -_boundsPos)
            {
                transform.localPosition = new Vector3(-_boundsPos, arrowTransform.y, 0);
            }
            else if (arrowTransform.x > _boundsPos)
            {
                transform.localPosition = new Vector3(_boundsPos, arrowTransform.y, 0);
            }
        }
    }
}
