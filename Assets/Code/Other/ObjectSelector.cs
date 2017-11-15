using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSelector : MonoBehaviour
{
    public bool selected = false;
    public Shader outlinedShader;
    public Shader defaultShader;

    private void OnMouseOver()
    {
        if (Input.GetMouseButton(0)
            && !selected)
        {
            Select();
        }
        else if (Input.GetMouseButton(1)
            && selected)
        {
            DeSelect();
        }
    }

    public void Select()
    {
        selected = true;
        AtomManager.selectedObjects.Add(gameObject);
        GetComponent<Renderer>().material.shader = outlinedShader;
    }

    public void DeSelect(bool mergeRemove = false)
    {
        selected = false;
        GetComponent<Renderer>().material.shader = defaultShader;
        if (!mergeRemove)
        {
            AtomManager.selectedObjects.Remove(gameObject);
        }
    }
}
