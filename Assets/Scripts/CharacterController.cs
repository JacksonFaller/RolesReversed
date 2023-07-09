using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController : MonoBehaviour
{
    [SerializeField] private float _xVelocity = 3.5f;
    [SerializeField] private float _jumpDuration = 1f;
    [SerializeField] private float _jumpVelocity = 52f;
    [SerializeField] private float _gravityModifier = .5f;

    private Rigidbody2D _rigidbody;
    private bool _isJumping;
    private float _jumpTime;
    private bool _isMoving = true;
    private float _gravity;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _gravity = _rigidbody.gravityScale;
        GameManager.OnWorldChange += GameManager_OnWorldChange;
    }

    private void GameManager_OnWorldChange(WorldModifier worldModifier)
    {
        bool gravityModActive = worldModifier == WorldModifier.Gravity;
        _rigidbody.gravityScale = gravityModActive ? _gravityModifier : _gravity;
    }

    void Update()
    {
        if (!_isMoving)
        {
            _rigidbody.velocity = Vector2.zero;
            return;
        }

        float yVelocity = _rigidbody.velocity.y;

        if (_isJumping)
        {
            yVelocity = _jumpVelocity;
            _jumpTime -= Time.deltaTime;

            if (_jumpTime <= 0f)
            {
                _isJumping = false;
            }
        }

        _rigidbody.velocity = new Vector2(_xVelocity, yVelocity);
        //Debug.Log(_rigidbody.velocity);
    }


    public void Jump()
    {
        Debug.Log("Jumping");
        _isJumping = true;
        _jumpTime = _jumpDuration;
    }

    public void Stop()
    {
        _isMoving = false;
    }
}
