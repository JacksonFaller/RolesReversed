using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Teleport : MonoBehaviour
{
    [SerializeField] private Transform _exit;
    private bool _isActive;
    private SpriteRenderer _entranceSpriteRenderer;
    private SpriteRenderer _exitSpriteRenderer;

    void Start()
    {
        GameManager.OnCameraChange += GameManager_OnCameraChange;
        _entranceSpriteRenderer = GetComponent<SpriteRenderer>();
        _exitSpriteRenderer = _exit.GetComponent<SpriteRenderer>();
    }

    private void GameManager_OnCameraChange(CameraTypeModifier cameraModifier)
    {
        _isActive = cameraModifier == CameraTypeModifier.Teleport;

        _exitSpriteRenderer.enabled = _isActive;
        _entranceSpriteRenderer.enabled = _isActive;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (_isActive && collision != null && collision.CompareTag(Configuration.Tags.Character))
        {
            collision.transform.position = _exit.position;
            GameManager.Instance.ResetCameraModifier();
        }
    }
}
