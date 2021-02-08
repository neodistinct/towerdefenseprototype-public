using UnityEngine;

public class CannonSite : MonoBehaviour
{
    [SerializeField]
    private GameObject cannonPrefab;

    private Renderer _renderer;
    private Color _originalColor;
    public bool _siteFree = true;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _originalColor = _renderer.material.color;

    }

    // Theese collider mouse events below allow to manage things without Raycast, yet not sure about performance however.

    private void OnMouseEnter()
    {
        // If site is not yet occupied - highlight it with blue color
        if (_siteFree) { 
            _renderer.material.color = new Color(0, 0, 1);
        }
    }

    private void OnMouseExit()
    {
        // Revert to original color
        _renderer.material.color = _originalColor;
    }

    private void OnMouseDown()
    {
        if (_siteFree && cannonPrefab && GameManager.playerBase.score >= GameManager.BUILD_PRICE) {

            Instantiate(cannonPrefab, transform.position, Quaternion.identity);
            _siteFree = false;
            GameManager.playerBase.score -= GameManager.BUILD_PRICE;
        }

    }
}