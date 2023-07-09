using System;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;

public partial class GameManager : MonoBehaviour
{
    [SerializeField] TMP_Text _textMeshPro;
    [SerializeField] float _maxEnergy = 10f;
    [SerializeField] float _freezeEnergyCostPerSecond = 1f;
    [SerializeField] float _teleportEnergyCost = 1f;

    public static GameManager Instance { get; private set; }
    public static CameraTypeModifier ActiveCameraModifiers { get; private set; } = CameraTypeModifier.None;
    public static WorldModifier ActiveWorldModifiers { get; private set; } = WorldModifier.None;

    public static event Action<WorldModifier> OnWorldChange;
    public static event Action<CameraTypeModifier> OnCameraChange;


    private float _curEnergy;

    void Start()
    {
        _curEnergy = _maxEnergy;
        Instance = this;
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
            StartCoroutine(FreezeWater());
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (!ActiveCameraModifiers.HasFlag(CameraTypeModifier.Teleport))
            {
                if (_curEnergy >= _teleportEnergyCost)
                {
                    _curEnergy -= _teleportEnergyCost;
                    ToggleCameraModifier(CameraTypeModifier.Teleport);
                }
            }
            else
            {
                ToggleCameraModifier(CameraTypeModifier.Teleport);
            }
        }
    }

    IEnumerator FreezeWater()
    {
        ToggleCameraModifier(CameraTypeModifier.Ice);
        while (ActiveCameraModifiers.HasFlag(CameraTypeModifier.Ice) && _curEnergy > 0f)
        {
            var waterObjects = GetVisibleObjects(Configuration.LayerMasks.Water)
                .Select(x => x.GetComponent<WaterObject>());

            foreach (var waterObject in waterObjects)
            {
                waterObject.Freeze();
            }

            yield return new WaitForFixedUpdate();
            _curEnergy -= Time.fixedDeltaTime * _freezeEnergyCostPerSecond;
        }

        if (_curEnergy <= 0f)
        {
            ToggleCameraModifier(CameraTypeModifier.Ice);
            _curEnergy = 0f;
        }
    }

    public void ToggleCameraModifier(CameraTypeModifier cameraModifier)
    {
        ActiveCameraModifiers ^= cameraModifier;
        UpdateStateText();
        OnCameraChange(ActiveCameraModifiers);
    }

    public void ToggleWorldModifier(WorldModifier worldModifier)
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
}
