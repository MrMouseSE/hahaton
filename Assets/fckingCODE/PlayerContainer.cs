﻿using System.Collections.Generic;
using UnityEngine;

namespace fckingCODE
{
    public class PlayerContainer : MonoBehaviour
    {
        public EnemySpawner EnemySpawner;
        
        [Space]
        public float HitPoints;

        public float Speed;

        public GameObject Mesh;

        public float RotateAngle;
        public float RollAngle;

        public List<GameObject> TowerPlaces;
        public List<GameObject> Towers;
    }
}
