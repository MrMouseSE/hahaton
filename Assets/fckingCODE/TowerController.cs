using System.Collections;
using UnityEngine;

namespace fckingCODE
{
    public class TowerController : MonoBehaviour
    {
        public TowerContainer TowerContainer;

        private GameObject _target;
        private bool _cooldown;

        private void Update()
        {
            if (_target == null)
            {
                FindTarget();
            }
            else
            {
                if (!_cooldown)
                {
                    if (_target != null)
                    {
                        FireAction();
                    }
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
            TowerContainer.BulletPosition.LookAt(_target.transform);
        }

        private void Fire()
        {
            var bullet = Instantiate(TowerContainer.Bullet);
            var bulletContainer = bullet.GetComponent<BulletContainer>();
            bulletContainer.Damage = TowerContainer.Damage;
            bullet.transform.position = TowerContainer.BulletPosition.position;
            bullet.transform.LookAt(_target.GetComponent<EnemyContainer>().RootPosition);
            _cooldown = true;
            StartCoroutine(CooldownCounter());
        }

        private void FindTarget()
        {
                var enemyes = TowerContainer.EnemySpawner.Enemyes;
                if (enemyes.Count == 0) return;
                _target = FindNearest.FindNearestObject(transform, enemyes);
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