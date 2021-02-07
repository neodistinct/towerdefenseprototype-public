using UnityEngine;

public class CannonSite : MonoBehaviour
{
    [SerializeField]
    private GameObject cannonPrefab;

    private Renderer _meshRenderer;
    private Color _originalColor;
    private bool _siteFree = true;

    private void Awake()
    {
        _meshRenderer = GetComponent<Renderer>();
        _originalColor = _meshRenderer.material.color;

    }

    // Theese collider mouse events below allow to manage things without Raycast, yet not sure about performance however.

    private void OnMouseEnter()
    {
        // If site is not yet occupied - highlight it with blue color
        if (_siteFree) { 
            _meshRenderer.material.color = new Color(0, 0, 1);
        }
    }

    private void OnMouseExit()
    {
        _meshRenderer.material.color = _originalColor;
    }

    private void OnMouseDown()
    {
        if (_siteFree && cannonPrefab && GameManager.playerBase.score >= 100) {

            Quaternion direction = Quaternion.LookRotation((new Vector3(8.96f, 0, -6.9043f) - transform.position).normalized, Vector3.up);

            Instantiate(cannonPrefab, transform.position, Quaternion.identity);
            _siteFree = false;
            GameManager.playerBase.score -= GameManager.BUILD_PRICE;
        }

    }
}