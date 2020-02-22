using System;
using UnityEngine;

namespace fckingCODE
{
    public class TowerContainer : MonoBehaviour
    {
        public Transform BulletPosition;
        public GameObject Bullet;
        public float FireRate;
        
        [NonSerialized]
        public EnemySpawner EnemySpawner;
    }
}
