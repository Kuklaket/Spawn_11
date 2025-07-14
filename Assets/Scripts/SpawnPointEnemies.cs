using System.Collections;
<<<<<<< HEAD
=======
using System.Collections.Generic;
>>>>>>> 01105aed8c591c442bf9b9bf9a6cae790830661c
using UnityEngine;
using UnityEngine.Pool;

public class SpawnPointEnemies : MonoBehaviour
{
<<<<<<< HEAD
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
=======
    [SerializeField] private EnemyCat _enemyPrefab;
    [SerializeField] private int _defaultCapacity = 3;
    [SerializeField] private int _maxPoolSize = 3;

    private List<Transform> _pointsSpawn = new();
    private ObjectPool<EnemyCat> _enemyPool;

    private void Awake()
    {
        FillList();

        _enemyPool = new ObjectPool<EnemyCat>(
            createFunc: CreateEnemy,
            actionOnGet: (enemy) => {
                enemy.transform.position = SelectRandomPoint(_pointsSpawn).position;
                enemy.gameObject.SetActive(true);
                enemy.SetDirection(GetRandomDirection());
>>>>>>> 01105aed8c591c442bf9b9bf9a6cae790830661c
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

<<<<<<< HEAD
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
=======
    private EnemyCat CreateEnemy()
    {
        Transform point = SelectRandomPoint(_pointsSpawn);
        EnemyCat enemy = Instantiate(_enemyPrefab, point.position, Quaternion.identity);

        enemy.ReadyForReleased += ReleaseEnemyInPool;

        return enemy;
    }

    private void ReleaseEnemyInPool(EnemyCat enemy)
    {
        _enemyPool.Release(enemy);
    }

    private Transform SelectRandomPoint(List<Transform> spawnList)
    {
        int _randomPointNumber = Random.Range(0, spawnList.Count);

        Transform point = spawnList[_randomPointNumber];

        return point;
    }

    private Vector2 GetRandomDirection()
    {
        float fullCircle = 2f * Mathf.PI;
        float randomAngle = Random.Range(0f, fullCircle);

        Vector2 directionAngle = new(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle));

        return directionAngle;
    }

    private void FillList()
    {
       foreach (Transform spawn in transform)
        {
            _pointsSpawn.Add(spawn);
        }
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
>>>>>>> 01105aed8c591c442bf9b9bf9a6cae790830661c
