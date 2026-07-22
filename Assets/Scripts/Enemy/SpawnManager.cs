using UnityEngine;

public class SpawnManager : MonoBehaviour {
    [SerializeField] private GameObject enemyPrefab;
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
        GameObject enemy = Instantiate(enemyPrefab, spawnPos[lane].position, Quaternion.identity);
        enemy.GetComponent<EnemyController>().Initialize(targets[lane]);
    }
}
