using System.Collections.Generic;
using UnityEngine;

namespace fckingCODE
{
        [CreateAssetMenu(fileName = "EnemySettings", menuName = "EnemySettings", order = 1)]
        public class EnemySettings : ScriptableObject
        {
                public int EnemyLevel;
                public List<Mesh> EnemyMeshes;
                public Vector2 EnemyHitPoints;
                public Vector2 EnemyDamage;
                public Vector2 EnemySpeed;
                public Vector2 EnemyRage;
        }
}
