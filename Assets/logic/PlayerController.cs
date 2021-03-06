﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace fckingCODE
{
    public class PlayerController : MonoBehaviour
    {
        [FormerlySerializedAs("PlayerContainer")] public PlayerContainer Container;

        public AnimationCurve TowerIndexField;

        private List<TowerController> _towerControllers = new List<TowerController>();

        private Coroutine _coroutine;
        private float _massDif;

        private bool _isBusy;

        public bool IsBusy
        {
            get => _isBusy;
            set => _isBusy = value;
        }

        private void Awake()
        {
            InstantiateTower(0);
        }

        private void Update()
        {
            transform.position += transform.forward * Container.Speed * Time.deltaTime;
            SideShift();
        }

        private void OnTriggerEnter(Collider other)
        {
            var obj = other.gameObject;

            if (obj.layer == 12)
            {
                Debug.Log(other);
                SelfDestruction();
            }
            
            if (obj.layer != 0) return;
            var enemy = obj.GetComponent<EnemyContainer>();
            TakeEnemyEffect(enemy);
        }

        public void UpdateRageValue(float rage)
        {
            Container.Rage += rage;
            Container.RageDisplay.value = Container.Rage;
        }

        private void TakeEnemyEffect(EnemyContainer enemy)
        {
            UpdateRageValue(enemy.Rage);
            Container.HitPoints -= enemy.Damage;
            if (Container.Rage >= Container.TowerCoast)
            {
                InstantiateTower((int)TowerIndexField.Evaluate(Random.Range(0f,1f)));
            }
            if (Container.Rage >= 100f)
            {
                SelfDestruction();
            }
            if (Container.HitPoints<= 0)
            {
                SelfDestruction();
            }
        }

        private void SideShift()
        {
            transform.position += transform.right * Container.SideSpeed * _massDif * Time.deltaTime;
        }

        private float GetTowersMass()
        {
            float mass = 1f;
            if (_towerControllers.Count==0) return mass;
            foreach (var towerController in _towerControllers)
            {
                mass += towerController.TowerContainer.Mass;
            }
            return mass;
        }

        public void UpdateMassDif()
        {
            float massDif = 0;
            if (_towerControllers.Count==0) return;
            foreach (var towerController in _towerControllers)
            {
                var towerMass = towerController.TowerContainer.Mass;

                massDif += towerController.transform.position.x > transform.position.x ? towerMass * 1 : towerMass * -1;
            }

            _massDif = massDif;
            
            float angle = 0;
            angle = Mathf.Lerp(0,15f, Math.Abs(_massDif/10)) * (_massDif>0? -1: 1);

            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }

            _coroutine = StartCoroutine(MassEffectAppend(angle));
        }

        private void InstantiateTower(int towerID)
        {
            Container.EnemySpawner.Level++;
            if(_isBusy) return;
            Container.NewTowerPlace.gameObject.SetActive(true);
            Container.TrunkController.SetTrigger("Open");
            var tower = Instantiate(Container.Towers[towerID]);
            var towerController = tower.GetComponent<TowerController>();

            towerController.TowerRageCoast = Container.TowerCoast;
            towerController.TowerContainer.EnemySpawner = Container.EnemySpawner;
            towerController.enabled = false;
            SetTowerPlace(tower.transform);
            _isBusy = true;
        }

        public void UpdateTowerController(TowerController towerController, bool remove = false)
        {
            if (_towerControllers.Contains(towerController))
            {
                _towerControllers.Remove(towerController);
            }
            if (!remove)
            {
                _towerControllers.Add(towerController);
            }
        }

        private void SetTowerPlace(Transform transform)
        {
            var placeTransform = Container.NewTowerPlace;
            if (placeTransform != null)
            {
                transform.parent = placeTransform;
                transform.position = placeTransform.position;
            }
        }

        private IEnumerator MassEffectAppend(float angle)
        {
            while (Container.Mesh.transform.rotation != Quaternion.Euler(0,-angle,angle))
            {
                yield return new WaitForEndOfFrame();
                Container.Mesh.transform.rotation = Quaternion.Lerp(Container.Mesh.transform.rotation,
                    Quaternion.Euler(0, -angle*Container.RotateAngle, angle*Container.RollAngle), 0.01f);
            }
        }

        private void SelfDestruction()
        {
            var lights = FindObjectsOfType<Light>();
            
            Container.EnemySpawner.doSpawns = false;
            Container.Speed = 0;
            
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
                
                var alpha = Container.GameOverImage.color.a;
                alpha += 0.1f;
                Container.GameOverImage.color = new Color(1,1,1,alpha);
                time -= 0.1f;
                yield return new WaitForEndOfFrame();
            }

            Container.GameOverButton.SetActive(true);
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}