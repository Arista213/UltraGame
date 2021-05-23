using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Putable : MonoBehaviour
{
    public Color holorColor;
    private Color startColor;
    private Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    private void OnMouseEnter()
    {
        rend.material.color = holorColor;
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
