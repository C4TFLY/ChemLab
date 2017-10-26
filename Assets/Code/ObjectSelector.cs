﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSelector : MonoBehaviour
{
    private bool selected = false;

    private void Start()
    {

    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButton(0)
            && !selected)
        {
            selected = true;
            AtomManager.selectedObjects.Add(gameObject);
            GetComponent<Renderer>().material.color = Color.red;
        }
        else if (Input.GetMouseButton(1)
            && selected)
        {
            selected = false;
            AtomManager.selectedObjects.Remove(gameObject);
            GetComponent<Renderer>().material.color = Color.white;
        }
    }
}