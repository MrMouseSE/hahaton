﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        public float SideSpeed;

        public List<GameObject> TowerPlaces;
        public List<GameObject> Towers;

        public Image GameOverImage;
        public GameObject GameOverButton;
    }
}
