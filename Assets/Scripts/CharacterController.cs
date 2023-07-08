using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController : MonoBehaviour
{
    [SerializeField] private float _xVelocity = 3.5f;
    [SerializeField] private float _jumpDuration = 1f;
    [SerializeField] private float _jumpVelocity = 52f;

    private Rigidbody2D _rigidbody;
    private bool _isJumping;
    private float _jumpTime;
    private bool _isMoving = true;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
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
