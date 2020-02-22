using System;
using System.Collections.Generic;
using UnityEngine;

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
        private string _enemyPath = "Enemy";
        private GameObject _enemyСopy;

        
        private void Awake()
        {
            Enemyes = new List<GameObject>();
            doSpawns = true;
            _timeCounter = spawnsSpeed;
            _enemyСopy = Resources.Load<GameObject>(_enemyPath);
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
            var newEnemy =  Instantiate(_enemyСopy);
            newEnemy.transform.position = new Vector3(10,10,10);
            newEnemy.GetComponent<EnemyController>().Init(this, player);
            Enemyes.Add(newEnemy);
        }
    }
}
