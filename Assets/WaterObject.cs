using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class WaterObject : MonoBehaviour
{
    [SerializeField] private Color _waterColor;
    [SerializeField] private Color _iceColor;

    private SpriteRenderer _spriteRenderer;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.color = _waterColor;
    }

    void Update()
    {
        
    }

    public void Freeze()
    {
        gameObject.layer = Configuration.Layers.Ice;
        _spriteRenderer.color = _iceColor;
    }

    public void Melt()
    {
        gameObject.layer = Configuration.Layers.Water;
        _spriteRenderer.color = _waterColor;
    }
}
