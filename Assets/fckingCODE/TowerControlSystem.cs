using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

namespace fckingCODE
{
    public class TowerControlSystem : MonoBehaviour
    {
        public Camera ControlSystemCamera;
        public Transform NewTowerPosition;
        public PlayerController Controller;

        private GameObject _towerPosition;
        private GameObject _tower = null;

        private TowerUpgradeSettingsContainer _towerUpgradeSettingsContainer;
        private bool _qWasPressed;
        private bool _wWasPressed;
        private bool _eWasPressed;
        private bool _rWasPressed;
        private bool _aWasPressed;

        private void Awake()
        {
            _towerUpgradeSettingsContainer = (TowerUpgradeSettingsContainer) Resources.Load("TowerUpgradeSettingsContainer");
        }

        private void Start()
        {
            MouseAction.Instance.OnMouseDwn += MouseDownMethod;
            MouseAction.Instance.OnMouseUp += MouseUpMethod;
            //MouseAction.Instance.OnMouseDrg += MouseDragMethod;
        }

        /*public void Update()
        {
            if (Input.GetButtonUp("qpress"))
            {
                if (_wWasPressed)
                {
                    SetTower(Controller.Container.TowerPlaces[0]);
                    _qWasPressed = false;
                }
                if (Controller.Container.NewTowerPlace.childCount>0)
                {
                    _tower = Controller.Container.TowerPlaces[0].transform.GetChild(0).gameObject;
                    _wWasPressed = true;
                }
                if (_tower != null)
                {
                    SetTower(Controller.Container.TowerPlaces[0]);
                }
            }
            
            if (Input.GetButtonUp("wpress"))
            {
                if (_wWasPressed)
                {
                    SetTower(Controller.Container.TowerPlaces[1]);
                    _wWasPressed = false;
                }
                if (Controller.Container.NewTowerPlace.childCount > 0)
                {
                    _tower = Controller.Container.TowerPlaces[1].transform.GetChild(0).gameObject;
                    _wWasPressed = true;
                }
                if (_tower != null)
                {
                    SetTower(Controller.Container.TowerPlaces[1]);
                }
            }
            
            if (Input.GetButtonUp("epress"))
            {
                if (_rWasPressed)
                {
                    SetTower(Controller.Container.TowerPlaces[2]);
                    _eWasPressed = false;
                }
                else if (Controller.Container.NewTowerPlace.childCount > 0)
                {
                    _tower = Controller.Container.TowerPlaces[2].transform.GetChild(0).gameObject;
                    _eWasPressed = true;
                }
                if (_tower != null)
                {
                    SetTower(Controller.Container.TowerPlaces[2]);
                }
            }
            
            if (Input.GetButtonUp("rpress"))
            {
                if (_rWasPressed)
                {
                    SetTower(Controller.Container.TowerPlaces[3]);
                    _rWasPressed = false;
                }
                else if (Controller.Container.NewTowerPlace.childCount > 0)
                {
                    _tower = Controller.Container.TowerPlaces[3].transform.GetChild(0).gameObject;
                    _rWasPressed = true;
                }

                if (_tower != null)
                {
                    SetTower(Controller.Container.TowerPlaces[3]);
                }
            }
            if (Input.GetButtonUp("apress"))
            {
                if (_aWasPressed)
                {
                    SetTower(Controller.Container.NewTowerPlace.gameObject);
                }
                if (Controller.Container.NewTowerPlace.childCount > 0)
                {
                    _tower = Controller.Container.NewTowerPlace.GetChild(0).gameObject;
                    _aWasPressed = true;
                }
            }
        }*/
        

        public void MouseDownMethod()
        {
            var towerPosition = GetCastTarget();
            if (towerPosition != null)
            {
                _tower = GetTower(towerPosition);
                _towerPosition = towerPosition;
            }
        }

        public void MouseDragMethod()
        {
            if (_tower != null)
            {
                Ray ray = ControlSystemCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;
                if (Physics.Raycast(ray, out hitInfo,1000f, 1<<10))
                {
                    _tower.GetComponent<TowerController>().enabled = false;
                    _tower.transform.position = hitInfo.point;
                }
            }
        }

        private GameObject GetCastTarget()
        {
            Ray ray = ControlSystemCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            var info = Physics.RaycastAll(ray, float.MaxValue);

            foreach (var inf in info)
            {
                
                if (inf.transform.gameObject.layer == 9)
                {
                    Debug.Log(inf.transform.gameObject.name);
                    return inf.transform.gameObject;
                }
            }

            /*if (Physics.Raycast(ray, out hitInfo, 1000f, 1<<9))
            {
                Debug.LogError(hitInfo.transform.gameObject.name, hitInfo.transform.gameObject);
                return hitInfo.transform.gameObject;
            }*/
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

        private void MouseUpMethod()
        {
            if (_tower == null) return;
            var newTowerPosition = GetCastTarget(); 
            //GameObject newTowerPosition = FindNearest.FindNearestObject(_tower.transform, Controller.Container.TowerPlaces);

            if (newTowerPosition.transform != Controller.Container.NewTowerPlace)
            {
                _tower.GetComponent<TowerController>().enabled = true;
            }

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
                if (Controller.IsBusy && Controller.Container.NewTowerPlace.GetChild(0).gameObject == _tower)
                {
                    tower.enabled = true;
                    Controller.Container.NewTowerPlace.gameObject.SetActive(false);
                    Controller.Container.TrunkController.SetTrigger("Close");
                    Controller.IsBusy = false;
                }
                
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
            towerToUpgrade.GetComponent<TowerController>().UpdateTowerView();

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
            var towerController = _tower.GetComponent<TowerController>();
            
            _tower.transform.parent = towerPosition.transform;
            _tower.transform.rotation = Quaternion.Euler(0,0,0);
            _tower.transform.position = towerPosition.transform.position;
            _tower.transform.localScale = Vector3.one;
            Controller.UpdateTowerController(towerController);
            Controller.UpdateMassDif();
        }

        private void OnDestroy()
        {
            MouseAction.Instance.OnMouseDwn -= MouseDownMethod;
            MouseAction.Instance.OnMouseUp -= MouseUpMethod;
            MouseAction.Instance.OnMouseDrg -= MouseDragMethod;
        }
    }
}