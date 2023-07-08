using System;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] TMP_Text _textMeshPro;
    [SerializeField] float _maxEnergy = 10f;
    [SerializeField] float _freezeEnergyCost = 1f;

    public static CameraTypeModifier ActiveCameraModifiers { get; private set; } = CameraTypeModifier.None;
    public static WorldModifier ActiveWorldModifiers { get; private set; } = WorldModifier.None;

    public static event Action<WorldModifier> OnWorldChange;
    public static event Action<CameraTypeModifier> OnCameraChange;

    private float _curEnergy;

    void Start()
    {
        _curEnergy = _maxEnergy;
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


        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ToggleCameraModifier(CameraTypeModifier.Ice);
            StartCoroutine(FreezeWater());
        }
    }

    IEnumerator FreezeWater()
    {
        while (ActiveCameraModifiers.HasFlag(CameraTypeModifier.Ice) && _curEnergy != 0)
        {
            _curEnergy -= Time.deltaTime * _freezeEnergyCost;

            var waterObjects = GetVisibleObjects(Configuration.LayerMasks.Water)
             .Select(x => x.GetComponent<WaterObject>());

            foreach (var waterObject in waterObjects)
            {
                waterObject.Freeze();
            }

            yield return new WaitForFixedUpdate();
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

    public Collider2D[] GetVisibleObjects(LayerMask layerMask)
    {
        var camera = Camera.main;
        Vector2 bottomLeft = camera.ViewportToWorldPoint(new Vector3(0f, 0f, camera.nearClipPlane));
        Vector2 topRight = camera.ViewportToWorldPoint(new Vector3(1f, 1f, camera.nearClipPlane));

        Collider2D[] colliders = Physics2D.OverlapAreaAll(bottomLeft, topRight, layerMask);

        if (colliders.Length > 0)
            Debug.Log(string.Join(", ", colliders.Select(x => x.gameObject.name)));
        return colliders;
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
