using System;
using UnityEngine;

namespace fckingCODE
{
    public class TowerContainer : MonoBehaviour
    {
        public Transform BulletPosition;
        public GameObject Bullet;
        public float FireRate;
        public float Damage;
        public float Mass;

        public int Level;
        
        [NonSerialized]
        public EnemySpawner EnemySpawner;
    }
}
