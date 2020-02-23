using System;
using UnityEngine;

namespace fckingCODE
{
    public enum TowerType
    {
        Single = 0,
        Gatling = 1,
        Rocket = 2
    }
    
    public class TowerContainer : MonoBehaviour
    {
        public TowerType TowerType;
        public Transform BulletPosition;
        public GameObject Bullet;
        public float FireRate;
        public float Damage;
        public float Mass;

        [Space]
        public int Level;
        
        public GameObject Level1Mesh;
        public GameObject Level2Mesh;
        public GameObject Level3Mesh;

        [Space] public Transform Hardpoint;
        
        [NonSerialized]
        public EnemySpawner EnemySpawner;
    }
}
