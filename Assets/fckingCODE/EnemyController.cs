using UnityEngine;

namespace fckingCODE
{
    public class EnemyController : MonoBehaviour
    {
        private EnemyContainer _enemyContainer;
        private EnemySpawner _enemySpawner;
        private Transform _target;
        public EnemyContainer EnemyContainer => _enemyContainer;
    
        public void Init(EnemySpawner enemySpawner, Transform target)
        {
            _enemySpawner = enemySpawner;
            _target = target;
        }

        private void Update()
        {
            MoveTo();
        }

        private void TakeDamage(float damage)
        {
            _enemyContainer.Health -= damage;
            if (_enemyContainer.Health <= 0)
            {
                SelfDestruction();    
            }
        }
        
        private void MoveTo()
        {
            transform.LookAt(_target);
            transform.forward *= _enemyContainer.Speed;
        }

        private void SelfDestruction()
        {
            _enemySpawner.Enemyes.Remove(this.gameObject);
            GameObject.Destroy(this.gameObject);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.collider.gameObject.layer == 8)
            {
                //bullet damage
                return;
            }
            
            SelfDestruction();
        }
    }
}

