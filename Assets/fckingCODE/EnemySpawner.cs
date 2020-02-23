﻿using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace fckingCODE
{
    public class EnemySpawner : MonoBehaviour
    {
        [NonSerialized]
        public List<GameObject> Enemyes;
        public Transform player;
        public float spawnsSpeed;
        public bool doSpawns;

        private float _timeCounter;
        
        private void Awake()
        {
            Enemyes = new List<GameObject>();
            doSpawns = true;
            _timeCounter = spawnsSpeed;
        }

        private void Update()
        {
            if (doSpawns == false) return;
            
            _timeCounter -= Time.deltaTime;
            if (_timeCounter <= 0.0f)
            {
                SpawnEnemy();
                _timeCounter = spawnsSpeed;
            }
        }

        private void SpawnEnemy()
        {
            var position = GetNewEnemyPosition();
            var newEnemy =  EnemyFactory.Spawn(1, position);
            //newEnemy.transform.position = GetNewEnemyPosition();
            newEnemy.GetComponent<EnemyController>().Init(this, player);
            Enemyes.Add(newEnemy);
        }

        private Vector3 GetNewEnemyPosition()
        {
            return new Vector3(Random.Range(-10f, 10f),0,-10);
        }
    }
}
