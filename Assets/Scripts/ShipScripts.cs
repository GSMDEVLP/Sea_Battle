﻿using System.Collections.Generic;
using UnityEngine;

public class ShipScripts : MonoBehaviour
{
    
    public float xOffset = 0;
    public float zOffset = 0;
    private float nextYRotation = 90f;

    private GameObject clickedTile;
    int hitCount = 0;
    public int shipSize;

    private Material[] allMaterials;
    
    List<Color> allColors = new List<Color>();
    List<GameObject> touchTiles = new List<GameObject>();

    private void Start()
    {
        allMaterials = GetComponent<Renderer>().materials;
        for (int i = 0; i < allMaterials.Length; i++)
            allColors.Add(allMaterials[i].color);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Tile"))
        {
            touchTiles.Add(collision.gameObject);
        }        
    }

    public void ClearTileList()
    {
        touchTiles.Clear();
    }

    public Vector3 GetOffSetVec(Vector3 tilePos)
    {
        return new Vector3(tilePos.x + xOffset, 2, tilePos.z + zOffset);
    }

    public void RotateShip()
    {
        if (clickedTile == null) return;
        touchTiles.Clear();
        transform.localEulerAngles += new Vector3(0, nextYRotation, 0);
        nextYRotation *= -1;

        float temp = xOffset;
        xOffset = zOffset;
        zOffset = temp;
        SetPosition(clickedTile.transform.position);
    }

    public void SetPosition(Vector3 newVec)
    {
        ClearTileList();
        transform.localPosition = new Vector3(newVec.x + xOffset, 2, newVec.z + zOffset);
    }

    public void SetClickedTile(GameObject tile)
    {
        clickedTile = tile;
    }


    public bool OnGameBoard()
    {
        return touchTiles.Count == shipSize;
    }

    public bool HitCheckSank()
    {
        hitCount++;
        return shipSize <= hitCount;
    }


    public void FlashColor(Color tempColor)
    {
        foreach (Material mat in allMaterials)
        {
            mat.color = tempColor;
        }
        Invoke("ResetColor", 0.4f);
    }

    private void ResetColor ()
    {
        int i = 0;
        foreach (Material mat in allMaterials)
        {
            mat.color = allColors[i++];
        }
    }
}
