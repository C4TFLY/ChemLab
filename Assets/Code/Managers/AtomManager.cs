using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtomManager : MonoBehaviour {
    
    public static void Merge(GameObject[] atoms)
    {
        GameObject newParent = new GameObject("Deuterium");
        for (int i = 0; i < atoms.Length; i++)
        {
            atoms[i].transform.SetParent(newParent.transform);
        }
    }

}
