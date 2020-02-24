using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace fckingCODE
{
    public class ChunkController : MonoBehaviour
    {
        public ChunkContainer Container;

        private float _spawnCooldown;

        private void Awake()
        {
            _spawnCooldown = Container.SpawnCooldown;
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

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.layer != 11) return;
            StartCoroutine(CooldownCounter(Container.SpawnCooldown));
            if (_spawnCooldown>0) return;
            _spawnCooldown = Container.SpawnCooldown;
            foreach (var rageObjectSpawnPoint in Container.EnemySpawnPoints)
            {
                if (Container.EnemyWeight.Evaluate(Random.Range(0f,1f))>0.5)
                {
                    Container.Spawner.SpawnEnemy(1, rageObjectSpawnPoint);
                }
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
