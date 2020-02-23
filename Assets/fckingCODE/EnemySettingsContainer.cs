using System;
using System.Collections.Generic;
using UnityEngine;

namespace fckingCODE
{
    
    [CreateAssetMenu(fileName = "EnemySettingsContainer", menuName = "EnemySettingsContainer", order = 1)]
    public class EnemySettingsContainer : ScriptableObject
    {
        [SerializeField]
        public List<EnemySettings> EnemySettings;
        public List<EnemySettings> ObstacleSettings;
        public List<EnemySettings> RageItemSettings;
    }

    
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