using UnityEngine;

namespace tiplay.ArrowFest.Controllers
{
    public class SpartanCollisionController : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            PlaySpartanAnimation(other.transform);
        }

        private void PlaySpartanAnimation(Transform arrows)
        {
            int animation = Animator.StringToHash("SpartanDie");
        
            if (transform.CompareTag("DummySpartan"))
            {
                transform.GetComponent<Animator>().SetTrigger(animation);
            }
            else
            {
                arrows.GetComponent<PlayerArrowController>()
                    .RemoveArrows(1);
                transform.GetComponent<Animator>().SetTrigger(animation);
                Destroy(gameObject, 1);
            }
        }
    }
}
