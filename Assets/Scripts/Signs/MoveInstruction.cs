using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public abstract class MoveInstruction : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private bool _isActive = false;
    private Color _activeColor;
    private Color _inactiveColor;

    public void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _inactiveColor = _spriteRenderer.color;
        _activeColor = new Color(_inactiveColor.r, _inactiveColor.g, _inactiveColor.b, 1);
        GameManager.OnWorldChange += GameManager_OnWorldChange;
    }

    private void GameManager_OnWorldChange(WorldModifier worldModifiers)
    {
        _isActive = worldModifiers == WorldModifier.Electricity;
        _spriteRenderer.color = _isActive ? _activeColor : _inactiveColor;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(!_isActive)
        {
            return;
        }

        if (collision != null)
        {
            var controller = collision.gameObject.GetComponent<CharacterController>();
            DoAction(controller);
        }
    }

    protected abstract void DoAction(CharacterController controller);
}
