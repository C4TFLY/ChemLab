using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtomManager : MonoBehaviour {

    public static List<GameObject> selectedObjects = new List<GameObject>();


    private static int x = 0;

    public static void Merge()
    {
        GameObject newParent = new GameObject($"Parent{x}");
        for (int i = 0; i < selectedObjects.Count; i++)
        {
            selectedObjects[i].transform.SetParent(newParent.transform);
            x++;
        }
    }
}
