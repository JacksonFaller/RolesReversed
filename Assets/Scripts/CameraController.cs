using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    [SerializeField] private float _vSpeed = .2f;
    [SerializeField] private float _hSpeed = .2f;
    [SerializeField] private float _zoomSpeed = 0.2f;

    private Camera _camera;

    void Start()
    {
        _camera = GetComponent<Camera>();
    }

    void Update()
    {
        float x = Input.GetAxis(Configuration.Input.HorizontalAxis);
        float y = Input.GetAxis(Configuration.Input.VerticalAxis);
        var zoom = Input.mouseScrollDelta.y;
        _camera.orthographicSize -= zoom * _zoomSpeed;

        transform.Translate(new Vector3(x * _hSpeed, y * _vSpeed, 0));
    }
}
