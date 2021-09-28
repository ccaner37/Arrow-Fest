using tiplay.ArrowFest.Managers;
using UnityEngine;

namespace tiplay.ArrowFest.Controllers
{
    public class GameEndController : MonoBehaviour
    {
        private float _radius = 0.01f;
        private float _r = 12f;
    
        private int _maxArrowCount = 10;
        private int _fieldOfView = 75;

        private readonly Vector3 _cameraOffset = new Vector3(0, 1f, -1.6f);
        private readonly Vector3 _cameraRotation = new Vector3(14f, 0, 0);
    
        private void OnTriggerEnter(Collider other)
        {
            if (!GameManager.Instance.IsReachedEnd)
            {
                GameManager.Instance.IsReachedEnd = true;
            }
            
            AdjustCamera();
            GiveArrowsShape(other.transform);
        }

        private void AdjustCamera()
        {
            Camera.main.transform.localPosition = _cameraOffset;
            Camera.main.transform.localEulerAngles = _cameraRotation;
            Camera.main.fieldOfView = _fieldOfView;
        }
        
        private void GiveArrowsShape(Transform arrow)
        {
            for (int i = 0; i < arrow.childCount - 1; i++)
            {
                float theta = i * _r * Mathf.PI / _maxArrowCount;
                float x = Mathf.Sin(theta) * _radius;
                float y = Mathf.Cos(theta) * _radius;

                arrow.localScale = Vector3.one;
                arrow.GetChild(i).position = new Vector3(x, y, 0) + arrow.transform.position;

                if (i % 33 == 0)
                {
                    _radius += 0.03f;
                }
            }
        }
    }
}
