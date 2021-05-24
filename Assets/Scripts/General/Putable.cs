using System;
using System.Collections;
using System.Collections.Generic;
using Turrets;
using UnityEngine;

public class Putable : MonoBehaviour
{
    public Color holorColor;
    public Color cannotColor;
    private Color startColor;
    private Renderer rend;

    private Tree tree;
    public Vector3 positionOffset;

    private GameObject turret;

    private void OnMouseDown()
    {
        if (turret!=null)
        {
            Debug.Log("нет");
            return;
        }

        GameObject turretToBuild = TurretPlacing.instance.GetTurrentToBuild();
        turret = (GameObject) Instantiate(turretToBuild, transform.position + positionOffset, transform.rotation);
    }

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    private void OnMouseEnter()
    {
        if (false)  
            rend.material.color = cannotColor; 
        else rend.material.color = holorColor;
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
