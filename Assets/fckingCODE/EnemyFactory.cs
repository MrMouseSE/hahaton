using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace fckingCODE
{
    public class EnemyFactory
    {
        private static EnemySettingsContainer _enemySettingsContainer =
            AssetDatabase.LoadAssetAtPath<EnemySettingsContainer>("Assets/Resources/EnemySettingsContainer.asset");
        
        private static GameObject _enemy = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Resources/Enemy.prefab");
        private static GameObject _rageItem = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Resources/RageItem.prefab");
        private static GameObject _obstacle = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Resources/Obstacle.prefab");
        private static Vector3 _spawnPosition;

        public static GameObject Spawn(int index, Vector3 position, int level)
        {
            _spawnPosition = position;
            switch (index)
            {
                case 1:
                    return SpawnEnemy(_enemySettingsContainer.EnemySettings,_enemy,level);
                case 2:
                    return SpawnEnemy(_enemySettingsContainer.ObstacleSettings,_obstacle,level);
                case 3:
                    return SpawnEnemy(_enemySettingsContainer.RageItemSettings,_rageItem,level);
            }

            return null;
        }

        private static GameObject SpawnEnemy(List<EnemySettings> enemySettings, GameObject inst,int level)
        {
            var settings = GetLevelSettings(1,enemySettings);
            
            GameObject enemy = Object.Instantiate(inst, _spawnPosition, Quaternion.identity);
            enemy.SetActive(true);
            
            var container = enemy.GetComponent<EnemyContainer>();
            container.Health = GetRandomValue(settings.EnemyHitPoints);
            container.Damage = GetRandomValue(settings.EnemyDamage);
            container.Speed = GetRandomValue(settings.EnemySpeed);
            container.Rage = GetRandomValue(settings.EnemyRage);;

            
            var enemyIndex = GetRandomValue(new Vector2(0, settings.EnemyObjects.Count));
                
            GameObject enemyObject = Object.Instantiate(settings.EnemyObjects[enemyIndex], container.RootPosition, true);
            enemyObject.transform.position = container.RootPosition.position;

            return enemy;
        }

        private static int GetRandomValue(Vector2 valueRange)
        {
            return Random.Range((int)valueRange.x, (int)valueRange.y);
        }

        private static EnemySettings GetLevelSettings(int level, List<EnemySettings> itemSettings)
        {
            foreach (var setting in itemSettings)
            {
                if (setting.EnemyLevel == level)
                {
                    return setting;
                }
            }

            return null;
        }
    }
}
