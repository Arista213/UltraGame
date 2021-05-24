using System;
using System.Collections;
using System.Collections.Generic;
using General;
using Turrets;
using UnityEditor;
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
        if (BuildManager.GetTurretToBuild() == null) return;
        if (turret != null)
        {
            Debug.Log("нет");
            return;
        }

        turret = BuildManager.BuildTurret(transform.position, transform.rotation);
    }

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    private void OnMouseEnter()
    {
        if (BuildManager.GetTurretToBuild() == null) return;
        if (Physics2D.OverlapCircle(transform.position, 0.04f, Map.SolidLayer))
            rend.material.color = cannotColor;
        else
            rend.material.color = holorColor;
        rend.sortingOrder = 1;
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
        rend.sortingOrder = -1;
    }
}