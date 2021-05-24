using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Tile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var a = GameObject.FindGameObjectWithTag("Player");
        var b = new Vector3Int(3, 3, 0);
        var c = GetComponent<Tilemap>();
        print("A");
        c.SetColor(new Vector3Int(0,0,0), Color.black);
        c.DeleteCells(new Vector3Int(1, 1, 0), Vector3Int.left);
    }
}