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
            if (Vector3.Distance(transform.position,_target.position)>50)
            {
                SelfDestruction();
            }
            MoveTo();
        }
        
        private void MoveTo()
        {
            if (_target == null) return;

            transform.LookAt(_target);
            float step = _enemyContainer.Speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, _target.position, step);
        }

        private void OnTriggerEnter(Collider other)
        {
            var obj = other.gameObject;
            
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
            _enemySpawner.Enemyes.Remove(gameObject);
            Destroy(gameObject);
        }
    }
}