using System.Collections;
using UnityEngine;

namespace fckingCODE
{
    public class TowerController : MonoBehaviour
    {
        public TowerContainer TowerContainer;

        private bool _isVacant;
        private GameObject _target;
        private bool _cooldown;

        private void Update()
        {
            if (_isVacant)
            {
                if (!_cooldown)
                {
                    FindTarget();
                }
                
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
            StartCoroutine(CooldownCounter());
        }

        private void FindTarget()
        {
                var enemyes = TowerContainer.EnemySpawner.Enemyes;
                _target = FindNearest.FindNearestObject(transform, enemyes);
                if (_target != null)
                {
                    _isVacant = false;
                }
        }

        private IEnumerator CooldownCounter()
        {
            while (_cooldown)
            {
                yield return new WaitForSeconds(1/TowerContainer.FireRate);
                _cooldown = false;
            }
        }
    }
}