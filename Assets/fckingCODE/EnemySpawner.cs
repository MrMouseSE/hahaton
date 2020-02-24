using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace fckingCODE
{
    public class EnemySpawner : MonoBehaviour
    {
        public List<GameObject> Chunks;
        
        [FormerlySerializedAs("player")] public Transform Player;
        [FormerlySerializedAs("spawnsSpeed")] public float SpawnsSpeed;
        public bool doSpawns;

        [NonSerialized]
        public List<GameObject> Enemyes;
        
        private float _timeCounter;
        private List<GameObject> _currentChunksList = new List<GameObject>();
        
        private void Awake()
        {
            Enemyes = new List<GameObject>();
            doSpawns = true;
            _timeCounter = SpawnsSpeed;
            foreach (var chunk in Chunks)
            {
                chunk.GetComponent<ChunkContainer>().Spawner = this;
            }
            SpawnNewChunk(0);
        }

        private void Update()
        {
            if (Player.transform.position.z > 5)
            {
                foreach (Transform child in transform)
                {
                    var pos = child.position;
                    pos.z -= 100;
                    child.position = pos;
                }
                
                foreach (var chu in _currentChunksList)
                {
                    chu.GetComponent<ChunkController>().CheckDistance(Player.position);
                }
                
                SpawnNewChunk(Random.Range(0, Chunks.Count));
            }
        }

        public void SpawnNewChunk(int index)
        {
            var newChunk = Instantiate(Chunks[index], transform, true);
            newChunk.transform.parent = transform;

            var container = newChunk.GetComponent<ChunkContainer>();
            container.Spawner = this;
            
            _currentChunksList.Add(newChunk);
        }

        public void SpawnEnemy(int enemyIndex, Transform enemyPosition)
        {
            var newEnemy =  EnemyFactory.Spawn(enemyIndex, enemyPosition.position);
            newEnemy.GetComponent<EnemyController>().Init(this, Player);
            newEnemy.transform.parent = enemyPosition;
            
            if (enemyIndex!=3)
            {
                Enemyes.Add(newEnemy);    
            }
        }
    }
}