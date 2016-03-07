﻿using UnityEngine;
using System.Collections;

//Script for putting tiles in the tilemap. To use with TileMap script.

public class TileBrush : MonoBehaviour {

    public Vector2 brushsize = Vector2.zero;
    public int tileID = 0;
    public SpriteRenderer sRenderer;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, brushsize);
    }

    public void UpdateBrush(Sprite sprite)
    {
        sRenderer.sprite = sprite;
    }
}
