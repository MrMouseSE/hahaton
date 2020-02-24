using UnityEngine;
using Random = UnityEngine.Random;

namespace fckingCODE
{
    public class ChunkController : MonoBehaviour
    {
        public ChunkContainer Container;

        private void Awake()
        {
            GenerateEnemyInPosition();
        }

        public void CheckDistance(Vector3 position)
        {
            if (Vector3.Distance(position, transform.position) > 100)
            {
                SelfDestroy();
            }
        }

        private void SelfDestroy()
        {
            Destroy(this);
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
            
            foreach (var rageObjectSpawnPoint in Container.EnemySpawnPoints)
            {
                if (Container.EnemyWeight.Evaluate(Random.Range(0f,1f))>0.5)
                {
                    Container.Spawner.SpawnEnemy(1, rageObjectSpawnPoint);
                }
            }
        }
    }
}
