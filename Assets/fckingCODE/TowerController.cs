using System.Collections;
using UnityEngine;

namespace fckingCODE
{
    public class TowerController : MonoBehaviour
    {
        public TowerContainer TowerContainer;

        private GameObject _target;
        private bool _cooldown;

        public bool IsActive { get; set; }
        
        private void Awake()
        {
            UpdateTowerView();
        }

        private void Update()
        {
            if (!IsActive) return;
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

        public void UpdateTowerView()
        {
            if (TowerContainer.Hardpoint.childCount > 0)
            {
                Destroy(TowerContainer.Hardpoint.GetChild(0));
            }
            switch (TowerContainer.Level)
            {
                case 1:
                    Instantiate(TowerContainer.Level1Mesh).transform.SetParent(TowerContainer.Hardpoint);
                    break;
                case 2:
                    Instantiate(TowerContainer.Level2Mesh).transform.SetParent(TowerContainer.Hardpoint);
                    break;
                case 3:
                    Instantiate(TowerContainer.Level3Mesh).transform.SetParent(TowerContainer.Hardpoint);
                    break;
            }
        }

        public void SelfDestroy()
        {
            Destroy(gameObject);
        }
    }
}