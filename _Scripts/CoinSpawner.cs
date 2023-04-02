using System.Collections;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private int _coinPoolSize;
    [SerializeField] private float _spawnRate;

    private ObjectPool _coinPool;
    private void OnEnable()
    {
        CoinMovement.ReturnCoinToPool += ReturnToPool;
        PlayerMovement.OnPlayerDies += StopSpawning;
    }
    private void OnDisable()
    {
        CoinMovement.ReturnCoinToPool -= ReturnToPool;
        PlayerMovement.OnPlayerDies -= StopSpawning;
    }

    private void StopSpawning() => StopAllCoroutines();

    private void Start()
    {
        _coinPool = new ObjectPool(_coinPrefab, _coinPoolSize);
        StartCoroutine(CreateCoins());
    }

    private IEnumerator CreateCoins()
    {
        while (true)
        {
            var newObject = _coinPool.GetFromPool();
            newObject.transform.position = _spawnPoints[Random.Range(0, _spawnPoints.Length)].transform.position;
            newObject.gameObject.SetActive(true);
            yield return new WaitForSeconds(_spawnRate);
        }
    }
    private void ReturnToPool(GameObject newObject) => _coinPool.ReturnToPool(newObject);
}
