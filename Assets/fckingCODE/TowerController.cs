using System.Collections;
using UnityEngine;

namespace fckingCODE
{
    public class TowerController : MonoBehaviour
    {
        public TowerContainer TowerContainer;

        private GameObject _target;
        private bool _cooldown;

        public float TowerRageCoast { get; set; } = 0;

        
        private void Awake()
        {
            UpdateTowerView();
        }

        private void Update()
        {
            FindTarget();
            if (!_cooldown)
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
            TowerContainer.BulletPosition.LookAt(_target.transform);
        }

        private void Fire()
        {
            var bullet = Instantiate(TowerContainer.Bullet);
            var bulletContainer = bullet.GetComponent<BulletContainer>();
            bulletContainer.Damage = TowerContainer.Damage;
            bullet.transform.position = TowerContainer.BulletPosition.position;
            bullet.transform.LookAt(_target.GetComponent<EnemyContainer>().RootPosition);
            bullet.transform.parent = GameObject.FindGameObjectWithTag("Chunk").transform;
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
                Destroy(TowerContainer.Hardpoint.GetChild(0).gameObject);
            }

            switch (TowerContainer.Level)
            {
                case 1:
                    SetParrentToObject(Instantiate(TowerContainer.Level1Mesh));
                    break;
                case 2:
                    SetParrentToObject(Instantiate(TowerContainer.Level2Mesh));
                    break;
                case 3:
                    SetParrentToObject(Instantiate(TowerContainer.Level3Mesh));
                    break;
            }
        }

        private void SetParrentToObject(GameObject go)
        {
            go.transform.SetParent(TowerContainer.Hardpoint);
            go.transform.localPosition = Vector3.zero;
            go.transform.localRotation = new Quaternion(0,0,0,0);
            go.transform.localScale = Vector3.one;
        }

        public void SelfDestroy()
        {
            Destroy(gameObject);
        }
    }
}