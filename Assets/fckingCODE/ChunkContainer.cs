using System.Collections.Generic;
using UnityEngine;

namespace fckingCODE
{
    public class ChunkContainer : MonoBehaviour
    {
        public EnemySpawner Spawner;
        
        [Space]
        public AnimationCurve RageObjectWeight;
        public List<Transform> RageObjectSpawnPoints;
        public AnimationCurve ObstaclesWeight;
        public List<Transform> ObstaclesSpawnPoints;
        public AnimationCurve EnemyWeight;
        public List<Transform> EnemySpawnPoints;
    }
}
