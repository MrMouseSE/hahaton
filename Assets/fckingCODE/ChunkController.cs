using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace fckingCODE
{
    public class ChunkController : MonoBehaviour
    {
        public ChunkContainer Container;

        private float _spawnCooldown;

        private Coroutine _cdCoroutine;

        private void Awake()
        {
            GenerateEnemyInPosition();
        }

        public bool CheckDistance(Vector3 position)
        {
            if (Vector3.Distance(position, transform.position) > 100)
            {
                return true;
            }

            return false;
        }

        private void Update()
        {
            
            if (Vector3.Distance(transform.position, Container.Spawner.Player.transform.position) < 33)
            {
                Debug.Log(Vector3.Distance(transform.position, Container.Spawner.Player.transform.position));
                if (_spawnCooldown>0) return;
                foreach (var rageObjectSpawnPoint in Container.EnemySpawnPoints)
                {
                    if (Container.EnemyWeight.Evaluate(Random.Range(0f,1f))>0.5)
                    {
                        Container.Spawner.SpawnEnemy(1, rageObjectSpawnPoint);
                    }
                }
                
                if (_cdCoroutine != null)
                {
                    StopCoroutine(_cdCoroutine);
                }
                _spawnCooldown = Container.SpawnCooldown;
                _cdCoroutine = StartCoroutine(CooldownCounter(Container.SpawnCooldown));
            }
        }
        
        private IEnumerator CooldownCounter(float time)
        {
            yield return new WaitForSeconds(time);
            _spawnCooldown = 0;
        }


        private void GenerateEnemyInPosition()
        {
            foreach (var rageObjectSpawnPoint in Container.RageObjectSpawnPoints)
            {
                if (Container.RageObjectWeight.Evaluate(Random.Range(0f,1f))>0.5)
                {
                    Container.Spawner.SpawnEnemy(3, rageObjectSpawnPoint);
                }
            }
            
            foreach (var rageObjectSpawnPoint in Container.ObstaclesSpawnPoints)
            {
                if (Container.ObstaclesWeight.Evaluate(Random.Range(0f,1f))>0.5)
                {
                    Container.Spawner.SpawnEnemy(2, rageObjectSpawnPoint);
                }
            }
        }
    }
}
