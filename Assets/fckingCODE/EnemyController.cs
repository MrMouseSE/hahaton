using UnityEngine;

namespace fckingCODE
{
    public class EnemyController : MonoBehaviour
    {
        private EnemySpawner _enemySpawner;
        private Transform _target;
        
        public EnemyContainer _enemyContainer;

        public void Init(EnemySpawner enemySpawner, Transform target)
        {
            _enemySpawner = enemySpawner;
            _target = target;
        }

        private void Update()
        {
            MoveTo();
        }
        
        private void MoveTo()
        {
            transform.LookAt(_target);
            transform.forward *= _enemyContainer.Speed;
        }

        private void OnCollisionEnter(Collision other)
        {
            var obj = other.collider.gameObject;
            
            if (obj.layer == 8)
            {
                TakeDamage(obj.GetComponent<BulletContainer>().Damage);
                return;
            }
            
            SelfDestruction();
        }

        private void TakeDamage(float damage)
        {
            _enemyContainer.Health -= damage;
            if (_enemyContainer.Health <= 0)
            {
                SelfDestruction();    
            }
        }

        private void SelfDestruction()
        {
            _enemySpawner.Enemyes.Remove(this.gameObject);
            GameObject.Destroy(this.gameObject);
        }
    }
}

