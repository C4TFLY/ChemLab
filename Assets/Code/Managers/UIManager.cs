using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    private MergeManager mergeManager;

    private void Awake()
    {
        mergeManager = GetComponent<MergeManager>();
    }

    public void Merge()
    {
        mergeManager.Merge(ObjectManager.selectedObjects);
    }

    public void Separate()
    {
        foreach (GameObject parent in ObjectManager.selectedObjects)
        {
            mergeManager.Separate(parent, true);
        }

    }
}
