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
    private bool _canMove = true;
    private bool _platformModActive = false;
    private PlatformController _platformController;
  

    void Start()
    {
        _camera = GetComponent<Camera>();
        GameManager.OnCameraChange += GameManager_OnCameraChange;
        _platformController = GetComponentInChildren<PlatformController>(true);
    }

    private void GameManager_OnCameraChange(CameraTypeModifier cameraModifier)
    {
        _platformModActive = cameraModifier.HasFlag(CameraTypeModifier.Platform);
        _platformController.gameObject.SetActive(_platformModActive);
    }

    void Update()
    {
        if (_platformModActive && Input.GetKeyDown(KeyCode.Space))
        {
            _canMove = !_canMove;
            _platformController.TogglePlatform();
        }

        if (!_canMove) return;

        float x = Input.GetAxis(Configuration.Input.HorizontalAxis);
        float y = Input.GetAxis(Configuration.Input.VerticalAxis);
        var zoom = Input.mouseScrollDelta.y;
        _camera.orthographicSize -= zoom * _zoomSpeed;

        transform.Translate(new Vector3(x * _hSpeed, y * _vSpeed, 0));
    }
}
