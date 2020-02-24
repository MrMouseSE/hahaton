﻿using UnityEditor;
using UnityEngine;

namespace fckingCODE
{
    public class TowerControlSystem : MonoBehaviour 
    {
        public Camera ControlSystemCamera;
        public Transform NewTowerPosition;
        public PlayerController Controller;

        private GameObject _towerPosition;
        private GameObject _tower;

        private TowerUpgradeSettingsContainer _towerUpgradeSettingsContainer;

        private void Awake()
        {
            _towerUpgradeSettingsContainer = AssetDatabase.LoadAssetAtPath<TowerUpgradeSettingsContainer>("Assets/Resources/TowerSettings/TowerUpgradeSettingsContainer.asset");
        }

        public void OnMouseDown()
        {
            var towerPosition = GetCastTarget();
            
            if (towerPosition != null)
            {
                _tower = GetTower(towerPosition);
                _towerPosition = towerPosition;
            }
        }

        public void OnMouseDrag()
        {
            if (_tower != null)
            {
                Ray ray = ControlSystemCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;
                if (Physics.Raycast(ray, out hitInfo, 1000f, 1<<10))
                {
                    _tower.GetComponent<TowerController>().IsActive = false;
                    _tower.transform.position = hitInfo.point;
                }
            }
        }

        private GameObject GetCastTarget()
        {
            Ray ray = ControlSystemCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, 1000f, 1<<9))
            {
                Debug.Log("target " + hitInfo.transform.gameObject);
                return hitInfo.transform.gameObject;
            }

            return null;
        }

        private GameObject GetTower(GameObject towerPosition)
        {
            if (towerPosition.transform.childCount==0)
            {
                return null;
            }
            var tower = towerPosition.GetComponentInChildren<TowerController>().gameObject;

            return tower;
        }

        private void OnMouseUp()
        {
                var newTowerPosition = GetCastTarget();

                if (newTowerPosition != null)
                {
                    SetTower(newTowerPosition);
                }
                else
                {
                    SetTower(_towerPosition);    
                }
        }

        private void SetTower(GameObject newTowerPosition)
        {
            var tower = _tower.GetComponent<TowerController>();
            Controller.UpdateRageValue(-tower.TowerRageCoast);
            tower.TowerRageCoast = 0;
            if (newTowerPosition == _towerPosition)
            {
                SetNewTower(_towerPosition);
            }
            else
            {
                tower.enabled = true;
                Controller.Container.NewTowerPlace.gameObject.SetActive(false);
                Controller.Container.TrunkController.SetTrigger("Close");
                if (newTowerPosition.transform.childCount > 0)
                {
                    UpgradeTower(newTowerPosition.transform.GetChild(0).gameObject);
                }
                else
                {
                    SetNewTower(newTowerPosition);
                }
            }
        }

        private void UpgradeTower(GameObject towerToUpgrade)
        {
            var container = towerToUpgrade.GetComponent<TowerContainer>();
            container.Level++;

            var upgradeSettings = GetTowerUprgadeSettings(_tower.GetComponent<TowerContainer>().TowerType);

            container.FireRate += upgradeSettings.ChangeFireRate;
            container.Damage += upgradeSettings.ChangeDamage;
            container.Mass += upgradeSettings.ChangeMass;
            
            Controller.UpdateTowerController(_tower.GetComponent<TowerController>(),true);
            Controller.UpdateMassDif();
            Destroy(_tower);
            _tower = null;
        }

        private TowerUpgradeSettings GetTowerUprgadeSettings(TowerType type)
        {
            switch (type)
            {
                case TowerType.Single:
                    return _towerUpgradeSettingsContainer.SingleTowerUpgradeSettings;
                    break;
                case TowerType.Gatling:
                    return _towerUpgradeSettingsContainer.GatlingTowerUpgradeSettings;
                    break;
                case TowerType.Rocket:
                    return _towerUpgradeSettingsContainer.RocketTowerUpgradeSettings;
                    break;
            }

            return null;
        }

        private void SetNewTower(GameObject towerPosition)
        {
            //if (_tower == null) return;
            var towerController = _tower.GetComponent<TowerController>();
            
            _tower.transform.parent = towerPosition.transform;
            _tower.transform.rotation = Quaternion.Euler(0,0,0);
            _tower.transform.position = towerPosition.transform.position;
            _tower.transform.localScale = Vector3.one;
            Controller.UpdateTowerController(towerController);
            Controller.UpdateMassDif();
        }
    }
}