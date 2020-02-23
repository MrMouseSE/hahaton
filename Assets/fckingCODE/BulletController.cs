using UnityEngine;

namespace fckingCODE
{
    public class BulletController : MonoBehaviour
    {
        public BulletContainer BulletContainer;

        private void OnTriggerEnter(Collider other)
        {
            DestroyThis();
        }

        private void DestroyThis()
        {
            Destroy(gameObject);
        }

        private void Update()
        {
            MoveTo();
            if (BulletContainer.LiveTime>0)
            {
                BulletContainer.LiveTime -= Time.deltaTime;
                return;
            }
            DestroyThis();
        }

        public void MoveTo()
        {
            transform.position += transform.forward * BulletContainer.Speed * Time.deltaTime;
        }
    }
}
