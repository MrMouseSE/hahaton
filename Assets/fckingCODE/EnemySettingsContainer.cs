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
}