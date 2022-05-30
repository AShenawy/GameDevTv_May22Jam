using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static System.Action<EnemyController> OnEnemySpawned;

    public Collider[] spawnLocations;
    public EnemyController enemyPrefab;
    public int numEnemies;

    public void SpawnEnemiesImmediate()
    {
        int i = numEnemies;
        while (i > 0)
        {
            CreateEnemy();
            i--;
        }
    }

    

    public void SpawnEnemiesInterval(float min, float max)
    {
        StartCoroutine(SpawnIntervalsCR(min, max));
    }

    IEnumerator SpawnIntervalsCR(float min, float max)
    {
        int i = numEnemies;
        while (i > 0)
        {
            CreateEnemy();
            i--;
            yield return new WaitForSeconds(Random.Range(min, max));
        }
    }

    void CreateEnemy()
    {
        var location = spawnLocations[Random.Range(0, spawnLocations.Length)];
        var locCenter = location.bounds.center;
        var locExtents = location.bounds.extents;
        var randomX = locCenter.x + Random.Range(-locExtents.x, locExtents.x);
        var randomZ = locCenter.z + Random.Range(-locExtents.z, locExtents.z);

        var newEnemy = Instantiate(enemyPrefab, new Vector3(randomX, 1.5f, randomZ), Quaternion.identity, transform);
        
        if (GameManager.Instance.player)
        {
            newEnemy.SetTarget(GameManager.Instance.player.transform);
        }
        
        OnEnemySpawned?.Invoke(newEnemy);
    }
}
