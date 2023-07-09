using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringsController : MonoBehaviour
{
    [SerializeField] GameObject _container;

    void Start()
    {
        GameManager.OnCameraChange += GameManager_OnCameraChange;
    }

    private void GameManager_OnCameraChange(CameraTypeModifier cameraModifier)
    {
        _container.SetActive(cameraModifier == CameraTypeModifier.Springs);
    }
}
