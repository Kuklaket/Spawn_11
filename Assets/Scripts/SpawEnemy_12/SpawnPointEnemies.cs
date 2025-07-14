using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class SpawnPointEnemies : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private int _defaultCapacity = 2;
    [SerializeField] private int _maxPoolSize = 2;
    [SerializeField] private Transform _target;

    private ObjectPool<Enemy> _enemyPool;

    private void Awake()
    {
        _enemyPool = new ObjectPool<Enemy>(
            createFunc: CreateEnemy,
            actionOnGet: (enemy) =>
            {
                enemy.transform.position = transform.position;
                enemy.gameObject.SetActive(true);
                enemy.SetTarget(_target);
            },
            actionOnRelease: (enemy) => enemy.gameObject.SetActive(false),
            actionOnDestroy: (enemy) => Destroy(enemy.gameObject),
            collectionCheck: true,
            defaultCapacity: _defaultCapacity,
            maxSize: _maxPoolSize
        );
    }

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    private Enemy CreateEnemy()
    {
        Enemy enemy = Instantiate(_enemyPrefab, transform.position, Quaternion.identity);

        enemy.ReadyForReleased += ReleaseEnemyInPool;

        return enemy;
    }

    private void ReleaseEnemyInPool(Enemy enemy)
    {
        _enemyPool.Release(enemy);
    }

    private IEnumerator SpawnEnemy()
    {
        float waitingInSeconds = 2f;
        var waitForSeconds = new WaitForSeconds(waitingInSeconds);

        while (true)
        {
            _enemyPool.Get();
            yield return waitForSeconds;
        }
    }
}