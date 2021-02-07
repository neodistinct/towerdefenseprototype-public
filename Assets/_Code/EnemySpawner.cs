using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{   
    [Header("Enemy prefab")]
    [SerializeField]
    private GameObject enemyPrefab;

    [Header("Enemy target point")]
    [SerializeField]
    private Transform targetPoint;

    [Header("Spawn rate (in seconds)")]
    public float enemySpawnRate = 0.5f;

    private Coroutine _enemySpawningCoroutine;    
    private Queue<GameObject> _enemiesPool;

    private const int MAX_POOL_SIZE = 50;

    // Giving get-only public acces to pool of enemies coming from current spawner
    public List<GameObject> activeEnemies
    {
        get
        {
            return _enemiesPool.Where(gameObject => gameObject.active).ToList<GameObject>();
        }
    }

    private void Awake()
    {
        _enemiesPool = new Queue<GameObject>();
    }

    private void Start()
    {
        StartSpawing(enemySpawnRate);
    }

    private void StartSpawing(float seconds)
    {
        if (enemyPrefab)
        {
            // Saving corouting reference in variable so we can stop it when we need
            _enemySpawningCoroutine = StartCoroutine(SpawnEnemy());
        }
    }

    private void StopSpawning()
    {
        // if coroutine is running - stop it
        if (_enemySpawningCoroutine != null) StopCoroutine(_enemySpawningCoroutine);
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            GameObject enemy = null;

            // If pool is not full add enemy to pool
            if (_enemiesPool.Count < MAX_POOL_SIZE)
            {
                enemy = Instantiate(enemyPrefab, transform.position, transform.rotation);
                _enemiesPool.Enqueue(enemy);

                //Debug.Log("Elements in queue:" + _enemiesPool.Count);

            } else { // Or take free enemy cell from pool
                GameObject[] activeObjects = _enemiesPool.Where(gameObject => !gameObject.active).ToArray();

                if (activeObjects.Length > 0)
                {
                    enemy = activeObjects[0];

                    enemy.transform.position = transform.position;
                    enemy.transform.rotation = transform.rotation;
                    enemy.SetActive(true);
                }
            }

            // If available enemy was found in pool
            if (enemy)
            {
                NavMeshAgent enemyNavMeshAgent = enemy.GetComponent<NavMeshAgent>();

                if (enemyNavMeshAgent && targetPoint)
                {
                    enemyNavMeshAgent.enabled = true;
                    enemyNavMeshAgent.destination = targetPoint.position;
                }
            }


            yield return new WaitForSeconds(enemySpawnRate);
        }
    }
}
