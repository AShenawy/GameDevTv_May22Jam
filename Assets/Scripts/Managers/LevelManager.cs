using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Spawner enemySpawner;

    List<EnemyController> levelEnemies = new List<EnemyController>();

    private void Awake()
    {
        Spawner.OnEnemySpawned += RegisterEnemy;
        EnemyController.OnEnemyDestroyed += UnregisterEnemy;
    }

    private void Start()
    {
        enemySpawner.SpawnEnemiesInterval(0.5f, 1f);
    }

    void RegisterEnemy(EnemyController newEnemy)
    {
        levelEnemies.Add(newEnemy);
    }

    void UnregisterEnemy(EnemyController enemy)
    {
        levelEnemies.Remove(enemy);

        if (levelEnemies.Count == 0)
        {
            GameManager.Instance.GameOver();
        }
    }

    private void OnDestroy()
    {
        Spawner.OnEnemySpawned -= RegisterEnemy;
        EnemyController.OnEnemyDestroyed -= UnregisterEnemy;
    }
}