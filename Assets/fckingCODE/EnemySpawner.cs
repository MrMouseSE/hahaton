using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace fckingCODE
{
    public class EnemySpawner : MonoBehaviour
    {
        public List<GameObject> Chunks;
        
        public Transform player;
        public float spawnsSpeed;
        public bool doSpawns;

        [NonSerialized]
        public List<GameObject> Enemyes;
        
        private float _timeCounter;
        private List<GameObject> _currentChunksList = new List<GameObject>();
        
        private void Awake()
        {
            Enemyes = new List<GameObject>();
            doSpawns = true;
            _timeCounter = spawnsSpeed;
            
            SpawnNewChunk(Vector3.zero);
        }

        public void SpawnNewChunk(Vector3 position)
        {
            int index = Random.Range(0, 2); 
            var newChunk = Object.Instantiate(Chunks[index], transform, true);
            newChunk.transform.position = position;

            var container = newChunk.GetComponent<ChunkContainer>();
            container.Spawner = this;
            
            _currentChunksList.Add(newChunk);
        }

        public void SpawnEnemy(int enemyIndex, Transform enemyPosition)
        {
            var newEnemy =  EnemyFactory.Spawn(enemyIndex, enemyPosition.position);
            newEnemy.GetComponent<EnemyController>().Init(this, player);
            newEnemy.transform.parent = enemyPosition;
            
            if (enemyIndex!=3)
            {
                Enemyes.Add(newEnemy);    
            }
        }
    }
}