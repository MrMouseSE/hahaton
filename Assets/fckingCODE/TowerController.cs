using UnityEngine;

namespace fckingCODE
{
    public class TowerController : MonoBehaviour
    {
        public TowerContainer TowerContainer;

        private bool _isVacant;
        private GameObject _target;

        private void Init()
        {
            
        }

        private void Update()
        {
            if (_isVacant)
            {
                FindTarget();
            }
            else
            {
                if (_target != null)
                {
                    FireAction();
                }
            }
        }

        private void FireAction()
        {
            RotateTowerToTarget();
            Fire();
        }

        private void RotateTowerToTarget()
        {
            transform.LookAt(_target.transform);
        }

        private void Fire()
        {
            var bullet = Instantiate(TowerContainer.Bullet);
            bullet.transform.LookAt(_target.transform);
        }

        private void FindTarget()
        {
                var enemyes = TowerContainer.EnemySpawner.Enemyes;
                _target = FindDistanceToEnemy.FindNearestEnemy(transform, enemyes);
                if (_target != null)
                {
                    _isVacant = false;
                }
        }
    }
}
