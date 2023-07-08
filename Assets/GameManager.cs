using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] TMP_Text _textMeshPro;

    public static CameraTypeModifier ActiveCameraModifiers { get; private set; } = CameraTypeModifier.None;
    public static WorldModifier ActiveWorldModifiers { get; private set; } = WorldModifier.None;

    public static event Action<WorldModifier> OnWorldChange;
    public static event Action<CameraTypeModifier> OnCameraChange;

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleWorldModifier(WorldModifier.Electricity);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            ToggleWorldModifier(WorldModifier.Gravity);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ToggleCameraModifier(CameraTypeModifier.Platform);
        }
    }

    void ToggleCameraModifier(CameraTypeModifier cameraModifier)
    {
        ActiveCameraModifiers ^= cameraModifier;
        UpdateStateText();
        OnCameraChange(ActiveCameraModifiers);
    }

    void ToggleWorldModifier(WorldModifier worldModifier)
    {
        ActiveWorldModifiers ^= worldModifier;
        UpdateStateText();
        OnWorldChange(ActiveWorldModifiers);
    }

    public LayerMask objectLayer;

    public GameObject[] GetVisibleObjects()
    {
        var camera = Camera.main;
        Vector2 bottomLeft = camera.ViewportToWorldPoint(new Vector3(0f, 0f, camera.nearClipPlane));
        Vector2 topRight = camera.ViewportToWorldPoint(new Vector3(1f, 1f, camera.nearClipPlane));

        Collider2D[] colliders = Physics2D.OverlapAreaAll(bottomLeft, topRight, objectLayer);

        GameObject[] visibleObjects = new GameObject[colliders.Length];
        for (int i = 0; i < colliders.Length; i++)
        {
            visibleObjects[i] = colliders[i].gameObject;
        }

        Debug.Log(string.Join(", ", visibleObjects.Select(x => x.gameObject.name)));
        return visibleObjects;
    }

    public void UpdateStateText()
    {
        _textMeshPro.text = $"Camera mods: {ActiveCameraModifiers}, World mods: {ActiveWorldModifiers}";
    }

    [Flags]
    public enum CameraTypeModifier
    {
        None = 0,
        Ice = 1,
        Warm = 2,
        Teleport = 4,
        Springs = 8,
        Platform = 16
    }

    [Flags]
    public enum WorldModifier
    {
        None = 0,
        Electricity = 1,
        Gravity = 2,
    }
}
