using System.Collections.Generic;
using UnityEngine;

namespace fckingCODE
{
    [CreateAssetMenu(fileName = "TowerUpgradeSettingsContainer", menuName = "TowerUpgradeSettingsContainer", order = 1)]
    public class TowerUpgradeSettingsContainer : ScriptableObject
    {
        public TowerUpgradeSettings SingleTowerUpgradeSettings;
        public TowerUpgradeSettings GatlingTowerUpgradeSettings;
        public TowerUpgradeSettings RocketTowerUpgradeSettings;
    }
}
