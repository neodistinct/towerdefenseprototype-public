using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Base : MonoBehaviour
{
    [Header("Hit points")]
    [SerializeField]
    private int hitPoints = 100;

    [Header("Base health text item")]
    [SerializeField]
    private Text baseHealthText;

    [Header("Base health label")]
    [SerializeField]
    private string baseHealthLabel = "BASE HEALTH: ";

    [Header("Game score text object")]
    [SerializeField]
    private Text scoreText;

    [Header("Game score text object")]
    [SerializeField]
    private  string scoreLabel = "SCORE: ";

    [Header("'Place a cannon' text object")]
    [SerializeField]
    private GameObject placeCannonText;

    private int _score = 200;

    public int score { 
        get { return _score; }
        set
        {
            _score = value;
            UpdateScoreText();

            bool showScoreText = _score >= GameManager.BUILD_PRICE;
            placeCannonText.SetActive(showScoreText);
        }
    }

    private void Start()
    {
        UpdateHealthText();
        UpdateScoreText();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Decresing
            if (hitPoints > 0)
                hitPoints -= 5;
            else hitPoints = 0;

            // Updating base hit point on HUD
            UpdateHealthText();

            other.GetComponent<NavMeshAgent>().enabled = false;
            other.gameObject.SetActive(false);

            // If we lost - show restar panel
            if (hitPoints == 0)
            {
                GameManager.ShowRestartPanel();
            }
        }
    }

    private void UpdateHealthText() {
        if (baseHealthText) baseHealthText.text = baseHealthLabel + hitPoints;
    }

    private void UpdateScoreText()
    {
        if (scoreText) scoreText.text = scoreLabel + _score;
    }
}
