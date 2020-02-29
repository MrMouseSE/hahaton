using UnityEngine;

namespace fckingCODE
{
    [CreateAssetMenu(fileName = "TowerUpgradeSettings", menuName = "TowerUpgradeSettings", order = 1)]
    public class TowerUpgradeSettings : ScriptableObject
    {
        public float ChangeFireRate;
        public float ChangeDamage;
        public float ChangeMass;
    }
}
