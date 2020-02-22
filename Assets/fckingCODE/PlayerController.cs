using System.Collections.Generic;
using UnityEngine;

namespace fckingCODE
{
    public class PlayerController : MonoBehaviour
    {
        public PlayerContainer PlayerContainer;

        private List<TowerController> _towerControllers;

        public List<TowerController> TowerControllers
        {
            get => TowerControllers;
            set => TowerControllers = value;
        }
        
        

        private void InstantiateTower(int towerID)
        {
            var tower = Instantiate(PlayerContainer.Towers[towerID]);
            _towerControllers.Add(tower.GetComponent<TowerController>());
            tower.transform.position = GetTowerPlace(tower.transform);
        }

        private Vector3 GetTowerPlace(Transform transform)
        {
            var go = FindNearest.FindNearestObject(transform, PlayerContainer.TowerPlaces);
            
            return go.transform.position;
        }
    }
}
