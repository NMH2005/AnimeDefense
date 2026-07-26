using UnityEngine;

public class SpawnManager : MonoBehaviour {
    [SerializeField] private EnemyData[] enemies;
    [SerializeField] private Transform[] spawnPos;
    [SerializeField] private float spawnInterval = 1f;
    [SerializeField] private Transform[] targets;

    private float timer;
    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    private void SpawnEnemy()
    {
        int lane = Random.Range(0, spawnPos.Length);
        int randomEnemy = Random.Range(0, enemies.Length);

        GameObject enemy = Instantiate(enemies[randomEnemy].prefab, spawnPos[lane].position, Quaternion.Euler(0, 90, 0));
        enemy.GetComponent<EnemyController>().Initialize(targets[lane], enemies[randomEnemy].speed, enemies[randomEnemy].maxHealth, enemies[randomEnemy].value);
    }
}
