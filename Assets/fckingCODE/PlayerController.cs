using System;
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

        private void Awake()
        {
            InstantiateTower(0);
        }

        private void Update()
        {
            transform.position += transform.forward * PlayerContainer.Speed * Time.deltaTime;
            SideShift();
        }

        private void SideShift()
        {
            float angle = 0;
            float massDif = GetMassDif();

            transform.position += transform.right * massDif * Time.deltaTime;

            if (massDif < 0)
            {
                angle = Mathf.Lerp(0,15f, Math.Abs(massDif));    
            }
            else
            {
                angle = Mathf.Lerp(0,-15f, Math.Abs(massDif));
            }
            
            PlayerContainer.Mesh.transform.rotation = Quaternion.Euler(0,-angle,angle);
            
        }

        private float GetMassDif()
        {
            float massDif = 0;
            foreach (var towerController in _towerControllers)
            {
                if (towerController.transform.position.x > transform.position.x)
                {
                    massDif += towerController.TowerContainer.Mass;
                }
                else
                {
                    massDif -= towerController.TowerContainer.Mass;
                }
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
    }
}
