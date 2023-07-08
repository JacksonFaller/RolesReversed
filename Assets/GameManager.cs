using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] TMP_Text _textMeshPro;
    public static CameraTypeModifiers ActiveCameraModifiers { get; private set; } = CameraTypeModifiers.None;
    public static WorldModifiers ActiveWorldModifiers { get; private set; } = WorldModifiers.None;

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            ActiveWorldModifiers ^= WorldModifiers.Electricity;
            UpdateStateText();
        }
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
    public enum CameraTypeModifiers
    {
        None = 0,
        Ice = 1,
        Warm = 2,
        Teleport = 4,
        Springs = 8,
        Platform = 16
    }

    [Flags]
    public enum WorldModifiers
    {
        None = 0,
        Electricity = 1,
        Gravity = 2,
    }
}
