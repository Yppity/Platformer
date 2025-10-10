using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Coin _coinPrefab;
    [SerializeField] private CoinSpawnPoint[] _spawnPoints;
    [SerializeField] private float _spawnDelay = 5f;

    private int _activeCoinCount = 0;
    private ObjectPool<Coin> _coinPool;
    private List<Transform> _currentSpawnPoints = new List<Transform>();
    private Coroutine _spawnCoroutine = null;

    public event Action CoinCollected;

    private void Awake()
    {
        _coinPool = new ObjectPool<Coin>(
        createFunc: () => Instantiate(_coinPrefab),
        actionOnGet: (coin) => InitializeCoin(coin),
        actionOnRelease: (coin) => ReleaseCoin(coin));

        TryStartSpawnRoutine();
    }

    private void InitializeCoin(Coin coin)
    {
        coin.gameObject.SetActive(true);
        coin.transform.position = GetRandomSpawnPoint();
        coin.PlayerCollisionCoin += HandleCoinCollected;

        _activeCoinCount++;
    }

    private Vector3 GetRandomSpawnPoint()
    {
        int index = UnityEngine.Random.Range(0, _currentSpawnPoints.Count);
        Vector3 spawnPointPosition = _currentSpawnPoints[index].position;
        _currentSpawnPoints.RemoveAt(index);

        return spawnPointPosition;
    }

    private IEnumerator SpawnRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(_spawnDelay);

        while (_currentSpawnPoints.Count > 0)
        {
            yield return wait;

            _coinPool.Get();
        }

        _spawnCoroutine = null;

        yield break;
    }

    private void ReleaseCoin(Coin coin)
    {
        coin.gameObject.SetActive(false);
        coin.PlayerCollisionCoin -= HandleCoinCollected;

        _activeCoinCount--;
    }

    private void HandleCoinCollected(Coin coin)
    {
        _coinPool.Release(coin);
        CoinCollected?.Invoke();
        TryStartSpawnRoutine();
    }

    private void FillSpawnPoints()
    {
        for (int i = 0; i < _spawnPoints.Length; i++)
            _currentSpawnPoints.Add(_spawnPoints[i].transform);
    }

    private void TryStartSpawnRoutine()
    {
        if (_spawnCoroutine == null && _activeCoinCount <= 0)
        {
            FillSpawnPoints();
            _spawnCoroutine = StartCoroutine(SpawnRoutine());
        }
    }
}
