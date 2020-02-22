using fckingCODE;
using UnityEngine;

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
    }

    public void MoveTo()
    {
        transform.position += transform.forward * BulletContainer.Speed * Time.deltaTime;
    }
}
