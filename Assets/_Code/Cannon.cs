using System.Collections;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [Header("Shooting radius")]
    [SerializeField]
    private float shootingRadius = 5.0f;

    [Header("Shooting rate (in seconds)")]
    [SerializeField]
    private float shootingRate = 1.0f;

    [Header("Rotation speed")]
    [SerializeField]
    private float rotationSpeed = 8.0f;

    [Header("Bullet prefab")]
    [SerializeField]
    private GameObject bulletPrefab;

    private EnemySpawner _enemySpawner;
    private Coroutine _shootingCoroutine;

    private void Awake()
    {
        GameObject spawnerObject = GameObject.FindGameObjectWithTag("EnemySpawner");
        if (spawnerObject)
        {
            _enemySpawner = spawnerObject.GetComponent<EnemySpawner>();
        }
    }

    private void Update()
    {
        if (_enemySpawner)
        {

            if (_enemySpawner.activeEnemies.Count > 0)
            {
                Transform closestEnemy = _enemySpawner.activeEnemies[0].transform;
                float shortestDistance = Vector3.Distance(transform.position, closestEnemy.position);

                foreach (GameObject enemy in _enemySpawner.activeEnemies)
                {
                    float distance = Vector3.Distance(transform.position, enemy.transform.position);

                    if (distance < shortestDistance)
                    {
                        shortestDistance = distance;
                        closestEnemy = enemy.transform;
                    }
                }

                // Rotate towards and shoot if enemy is in radius
                if(shortestDistance <= shootingRadius) { 
                    Library.RotateTowards(closestEnemy, transform, rotationSpeed);

                    if(_shootingCoroutine == null)
                        _shootingCoroutine = StartCoroutine(Shoot(shootingRate));
                } 
                else if (_shootingCoroutine != null) // Stop shooting otherwise
                {
                    StopCoroutine(_shootingCoroutine);
                    _shootingCoroutine = null;
                }

            }
        }
    }

    IEnumerator Shoot(float shootingRate)
    {
        // Don't shoot immediately. Give a time to 'aim'.
        yield return new WaitForSeconds(0.5f);

        while (true)
        {
            Instantiate(bulletPrefab, transform.position + new Vector3(0, 1.4f ,0), transform.rotation);
            yield return new WaitForSeconds(shootingRate);
        }
    }


}
