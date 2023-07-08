using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] TMP_Text _textMeshPro;
    private CameraTypeModifiers _cameraTypeModifiers = CameraTypeModifiers.None;

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            var tablichki = GetVisibleObjects().Select(x => x);
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

    public enum CameraTypeModifiers
    {
        None,
        Ice,
        Warm,
        Teleport,
        Springs,
        Platform
    }

    public enum WorldModifiers
    {
        Electricity,
        Gravity
    }
}
