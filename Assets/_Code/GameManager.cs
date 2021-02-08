using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Panel object for game restart")]
    [SerializeField]
    private GameObject restartPanel;

    [Header("Win/Loose text object")]
    [SerializeField]
    private Text infoText;

    private EnemySpawner _enemySpawner;
    private Coroutine _enemyRateIncreaseCoroutine;
    private int increaseEnemyRateTime = 3;

    // Static references to objects for use in common static methods
    private static GameObject _restartPanelReference;
    private static Text _infoTextReference;

    public static Base playerBase;

    // Price in point required to build new cannon
    public const int BUILD_PRICE = 150;

    private void Awake()
    {
        // Initialize our Base object that keep health and score
        GameObject playerBaseObject = GameObject.Find("Base");
        if (playerBaseObject)
            playerBase = playerBaseObject.GetComponent<Base>();

        // Initialize default enemy spawner
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

        if (infoText) _infoTextReference = infoText;

        _enemyRateIncreaseCoroutine = StartCoroutine(IncreaseEnemySpawnRate());
    }

    private IEnumerator  IncreaseEnemySpawnRate()
    {
        while (true)
        {
            yield return new WaitForSeconds(increaseEnemyRateTime);

            // Increasing enemy spawn rate by 10%, but neve go below 0.1
            if (_enemySpawner.enemySpawnRate >= 0.1f)_enemySpawner.enemySpawnRate *= 0.9f;

        }
    }

    public static void ShowRestartPanel(bool win = false)
    {
        _infoTextReference.text = win ? "YOU WIN!" : "YOU LOOSE!";
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
