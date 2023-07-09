using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
public class PlatformController : MonoBehaviour
{
    private Color _platformActiveColor;
    private Color _platformInactiveColor;
    private SpriteRenderer _renderer;
    private Collider2D _collider;
    private bool _enabled;

    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
        _platformInactiveColor = _renderer.color;
        _platformActiveColor = new Color(_platformInactiveColor.r, _platformInactiveColor.g, _platformInactiveColor.b, 1);
    }

    public void TogglePlatform()
    {
        _enabled = !_enabled;
        _renderer.color = _enabled ? _platformActiveColor : _platformInactiveColor;
        _collider.enabled = _enabled;
    }
}
