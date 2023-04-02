using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _obstaclePrefabs;
    [SerializeField] private GameObject[] _spawnPoints;
    [SerializeField] private float _spawnTimeRate;
    [SerializeField] private float _spawnDifferenceBetweenLevels;
    private ObjectPool _obstaclePool;
    private void OnEnable()
    {
        ObstacleMovement.ReturnToPool += ReturnObstacleToPool;
        LevelManager.OnLevelIncrease += IncreaseSpawnRate;
        PlayerMovement.OnPlayerDies += StopSpawning;
    }
    private void OnDisable()
    {
        ObstacleMovement.ReturnToPool -= ReturnObstacleToPool;
        LevelManager.OnLevelIncrease -= IncreaseSpawnRate;
        PlayerMovement.OnPlayerDies -= StopSpawning;
    }


    private void Start()
    {
        _obstaclePool = new ObjectPool(_obstaclePrefabs, 10);
        StartCoroutine(Spawner());
    }
    private void IncreaseSpawnRate() => _spawnTimeRate -= _spawnDifferenceBetweenLevels;

    private Vector3 RandomPoint() => new Vector3(_spawnPoints[Random.Range(0, _spawnPoints.Length)].transform.position.x, 0, this.transform.position.z);

    private IEnumerator Spawner()
    {
        while (true)
        {
            var newObstacle = _obstaclePool.GetFromPool();
            newObstacle.transform.position = RandomPoint();
            newObstacle.gameObject.SetActive(true);
            yield return new WaitForSeconds(_spawnTimeRate);
        }
    }
    private void ReturnObstacleToPool(GameObject obstacle) => _obstaclePool.ReturnToPool(obstacle);
    private void StopSpawning() => StopAllCoroutines();
}
