using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fckingCODE
{
    public class PlayerController : MonoBehaviour
    {
        public PlayerContainer PlayerContainer;

        private List<TowerController> _towerControllers = new List<TowerController>();

        public List<TowerController> TowerControllers
        {
            get { return _towerControllers; }
            set { TowerControllers = value; }
        }

        private Coroutine _coroutine;

        private void Awake()
        {
            InstantiateTower(0);
        }

        private void Update()
        {
            transform.position += transform.forward * PlayerContainer.Speed * Time.deltaTime;
            SideShift();
        }

        private void OnTriggerEnter(Collider other)
        {
            var obj = other.gameObject;
            
            if (obj.layer != 8)
            {
                TakeDamage(obj.GetComponent<EnemyContainer>().Damage);
                return;
            }
            
            SelfDestruction();
        }

        private void TakeDamage(float damage)
        {
            PlayerContainer.HitPoints -= damage;
            if (PlayerContainer.HitPoints<= 0)
            {
                SelfDestruction();
            }
        }

        private void SideShift()
        {
            float angle = 0;
            float massDif = GetMassDif();

            transform.position += transform.right * PlayerContainer.SideSpeed * massDif / GetTowersMass() * Time.deltaTime;
            
            angle = Mathf.Lerp(0,15f, Math.Abs(massDif/10)) * massDif>0? -1: 1;

            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }

            _coroutine = StartCoroutine(MassEffectAppend(angle));
        }

        private float GetTowersMass()
        {
            float mass = 1f;
            foreach (var towerController in _towerControllers)
            {
                mass += towerController.TowerContainer.Mass;
            }
            return mass;
        }

        private float GetMassDif()
        {
            float massDif = 0;
            foreach (var towerController in _towerControllers)
            {
                var towerMass = towerController.TowerContainer.Mass;
                massDif += towerMass * towerController.transform.position.x > transform.position.x ? 1 : -1;
            }

            return massDif;
        }

        public void InstantiateTower(int towerID)
        {
            var tower = Instantiate(PlayerContainer.Towers[towerID]);
            var towerController = tower.GetComponent<TowerController>();
            
            towerController.TowerContainer.EnemySpawner = PlayerContainer.EnemySpawner;
            _towerControllers.Add(towerController);
            tower.transform.position = GetTowerPlace(tower.transform);
        }

        private Vector3 GetTowerPlace(Transform transform)
        {
            var go = FindNearest.FindNearestObject(transform, PlayerContainer.TowerPlaces);
            if (go != null)
            {
                transform.parent = go.transform;
                return go.transform.position;
            }
            return Vector3.one;
        }

        private IEnumerator MassEffectAppend(float angle)
        {
            while (PlayerContainer.Mesh.transform.rotation != Quaternion.Euler(0,-angle,angle))
            {
                yield return new WaitForEndOfFrame();
                PlayerContainer.Mesh.transform.rotation = Quaternion.Lerp(PlayerContainer.Mesh.transform.rotation,
                    Quaternion.Euler(0, -angle*PlayerContainer.RotateAngle, angle*PlayerContainer.RollAngle), 0.01f);
            }
        }

        private void SelfDestruction()
        {
            var lights = FindObjectsOfType<Light>();
            
            PlayerContainer.EnemySpawner.doSpawns = false;
            PlayerContainer.Speed = 0;
            
            StartCoroutine(AddAlpha(3, lights));
        }

        private IEnumerator AddAlpha(float time, Light[] lights)
        {
            while (time>0)
            {
                foreach (var light in lights)
                {
                    light.intensity -= 0.05f;
                }
                
                var alpha = PlayerContainer.GameOverImage.color.a;
                alpha += 0.1f;
                PlayerContainer.GameOverImage.color = new Color(1,1,1,alpha);
                time -= 0.1f;
                yield return new WaitForEndOfFrame();
            }

            PlayerContainer.GameOverButton.SetActive(true);
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}