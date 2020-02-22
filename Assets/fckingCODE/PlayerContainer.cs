using System.Collections.Generic;
using UnityEngine;

namespace fckingCODE
{
    public class PlayerContainer : MonoBehaviour
    {
        public EnemySpawner EnemySpawner;
        
        [Space]
        public float HitPoints;

        public List<GameObject> TowerPlaces;
        public List<GameObject> Towers;
    }
}
