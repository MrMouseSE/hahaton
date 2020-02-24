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
        private static Vector3 _spawnPosition;

        public static GameObject Spawn(int index, Vector3 position)
        {
            _spawnPosition = position;
            switch (index)
            {
                case 1:
                    return SpawnEnemy(_enemySettingsContainer.EnemySettings);
                case 2:
                    return SpawnEnemy(_enemySettingsContainer.ObstacleSettings);
                case 3:
                    return SpawnEnemy(_enemySettingsContainer.RageItemSettings);
            }

            return null;
        }

        private static GameObject SpawnEnemy(List<EnemySettings> enemySettings)
        {
            var level = 1;
            var settings = GetLevelSettings(level,enemySettings);
            
            GameObject enemy = Object.Instantiate(_enemy, _spawnPosition, Quaternion.identity);
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
