using UnityEngine;

namespace fckingCODE
{
    public class TowerControlSystem : MonoBehaviour
    {
        public Camera ControlSystemCamera;

        private bool _hasTower;
        private GameObject _towerPosition;
        private GameObject _tower;
        
        private void OnMouseDown()
        {
            
            var towerPosition = GetCastTarget();
            
            if (towerPosition != null)
            {
                _tower = GetTower(towerPosition);
                _towerPosition = towerPosition;
            }
        }

        private GameObject GetCastTarget()
        {
            Ray ray = ControlSystemCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, 1000f, 1<<9))
            {
                return hitInfo.transform.gameObject;
            }

            return null;
        }

        private GameObject GetTower(GameObject towerPosition)
        {
            if (_hasTower) return _tower;

            var tower = towerPosition.GetComponentInChildren<TowerController>().gameObject;
            _hasTower = true;

            return tower;
        }

        private void OnMouseUp()
        {
            var newTowerPosition = GetCastTarget();
            
           
            if (newTowerPosition != null)
            {
                _towerPosition = newTowerPosition;
            }
            
            SetTower(_towerPosition);
        }

        private void SetTower(GameObject towerPosition)
        {
            if (_tower == null) return;
            _tower.transform.position = towerPosition.transform.position;
            _tower.transform.parent = towerPosition.transform;
            _hasTower = false;
        }
    }
}