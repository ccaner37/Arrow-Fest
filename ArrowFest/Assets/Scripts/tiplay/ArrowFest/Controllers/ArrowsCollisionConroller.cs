using UnityEngine;

namespace tiplay.ArrowFest.Controllers
{
    public class ArrowsCollisionConroller : MonoBehaviour
    {
        private float _minimumArrows = 1f;
        private float _maximumArrows = 200f;
        private float _scaleMultiplier = 0.7f;
        private float _scaleLimit = 0.25f;
        
        private void OnEnable() => PlayerArrowController.ArrowsChanged += ChangeCollisionScale;

        private void OnDisable() => PlayerArrowController.ArrowsChanged -= ChangeCollisionScale;
    
        private void ChangeCollisionScale(int arrowCount)
        {
            if (arrowCount > 200)
            {
                return;
            }
            else
            {
                float scaleSize = Mathf.InverseLerp(_minimumArrows, _maximumArrows, (float)arrowCount) * _scaleMultiplier;
                BoxCollider collider = gameObject.GetComponent<BoxCollider>();
                if (scaleSize > _scaleLimit)
                {
                    collider.size = new Vector3(scaleSize, collider.size.y, collider.size.z);
                }
            }
        }
    }
}
