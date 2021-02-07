using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Panel object for game restart")]
    [SerializeField]
    private GameObject restartPanel;

    private EnemySpawner _enemySpawner;
    private Coroutine _enemyRateIncreaseCoroutine;
    private int increaseEnemyRateTime = 3;

    private static GameObject _restartPanelReference;

    public static Base playerBase;
    public static int BUILD_PRICE = 150;

    private void Awake()
    {
        // Initialize our Base object that keep health and score
        GameObject playerBaseObject = GameObject.Find("Base");
        if (playerBaseObject)
            playerBase = playerBaseObject.GetComponent<Base>();

        GameObject spawnerObject = GameObject.FindGameObjectWithTag("EnemySpawner");
        if (spawnerObject)
        {
            _enemySpawner = spawnerObject.GetComponent<EnemySpawner>();
        }
    }

    private void Start()
    {
        if (restartPanel)
        {
            restartPanel.SetActive(false);
            _restartPanelReference = restartPanel;
        }
        _enemyRateIncreaseCoroutine = StartCoroutine(IncreaseEnemySpawnRate());
    }

    private IEnumerator  IncreaseEnemySpawnRate()
    {
        while (true)
        {
            yield return new WaitForSeconds(increaseEnemyRateTime);

            // Increasing enemy spawn rate by 10%
            _enemySpawner.enemySpawnRate *= 0.9f;

        }
    }

    public static void ShowRestartPanel()
    {
        Time.timeScale = 0;
        if (_restartPanelReference != null) _restartPanelReference.SetActive(true);
    }

    public static void Restart()
    {
        // As we have only single scene in project - we can reload it like this
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

}
