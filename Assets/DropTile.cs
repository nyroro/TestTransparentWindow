using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DropTile : MonoBehaviour
{
    public Tile tile;

    private AudioSource audioSource;

    protected void Start()
    {
        audioSource = GetComponentInParent<AudioSource>();
        Refresh();
    }

    public void PlaySound()
    {
        audioSource.Play();
    }

    public void Refresh()
    {
        var renderer = GetComponentInChildren<SpriteRenderer>();
        renderer.sprite = tile.sprite;
        var tilePos = GetComponentInParent<Tilemap>().WorldToCell(transform.position);
        renderer.sortingOrder = -tilePos.x - tilePos.y;
    }
}
