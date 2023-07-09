using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField]
    private float _bounceVelocity;

    [SerializeField]
    private float _minVelocity;

    [SerializeField]
    private float _maxVelocity;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag(Configuration.Tags.Character)) return;

        collision.attachedRigidbody.velocity = Vector2.up * 
            Mathf.Clamp(collision.attachedRigidbody.velocity.magnitude + _bounceVelocity, _minVelocity, _maxVelocity);

    }
}
