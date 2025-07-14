using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class SpawnPointEnemies_11 : MonoBehaviour
{
    [SerializeField] private EnemyCat_11 _enemyPrefab;
    [SerializeField] private int _defaultCapacity = 3;
    [SerializeField] private int _maxPoolSize = 3;

    private List<Transform> _pointsSpawn = new();
    private ObjectPool<EnemyCat_11> _enemyPool;

    private void Awake()
    {
        FillList();

        _enemyPool = new ObjectPool<EnemyCat_11>(
            createFunc: CreateEnemy,
            actionOnGet: (enemy) => {
                enemy.transform.position = SelectRandomPoint(_pointsSpawn).position;
                enemy.gameObject.SetActive(true);
                enemy.SetDirection(GetRandomDirection());
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

    private EnemyCat_11 CreateEnemy()
    {
        Transform point = SelectRandomPoint(_pointsSpawn);
        EnemyCat_11 enemy = Instantiate(_enemyPrefab, point.position, Quaternion.identity);

        enemy.ReadyForReleased += ReleaseEnemyInPool;

        return enemy;
    }

    private void ReleaseEnemyInPool(EnemyCat_11 enemy)
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
