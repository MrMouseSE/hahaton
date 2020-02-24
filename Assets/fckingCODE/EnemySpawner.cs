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

        private int _level;

        public int Level
        {
            get => _level;
            set => _level = value;
        }

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
                SpawnNewChunk(Random.Range(0, Chunks.Count));

                GameObject removeThis = null;
                foreach (var chu in _currentChunksList)
                {
                    if (chu.GetComponent<ChunkController>().CheckDistance(Player.position))
                    {
                        removeThis = chu;
                    }
                }

                if (removeThis!=null)
                {
                    _currentChunksList.Remove(removeThis);
                    Destroy(removeThis);
                }
                
            }
        }

        public void SpawnNewChunk(int index)
        {
            var newChunk = Instantiate(Chunks[index], transform, true);
            newChunk.transform.parent = transform;
            
            newChunk.transform.RotateAround(Vector3.zero, Vector3.up, 180*Random.Range(0,2));

            var container = newChunk.GetComponent<ChunkContainer>();
            container.Spawner = this;
            
            _currentChunksList.Add(newChunk);
        }

        public void SpawnEnemy(int enemyIndex, Transform enemyPosition)
        {
            var newEnemy =  EnemyFactory.Spawn(enemyIndex, enemyPosition.position,_level);
            newEnemy.GetComponent<EnemyController>().Init(this, Player);
            newEnemy.transform.parent = enemyPosition;
            
            if (enemyIndex!=3)
            {
                Enemyes.Add(newEnemy);    
            }
        }
    }
}