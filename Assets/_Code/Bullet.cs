using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet speed")]
    [SerializeField]
    private float speed = 20.0f;

    private void Start()
    {
        StartCoroutine(SelfDestroy());
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
            Destroy(gameObject);
            GameManager.playerBase.score += 5;
        }
    }

    private IEnumerator SelfDestroy()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
