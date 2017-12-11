using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    private MergeManager mergeManager;

    private void Awake()
    {
        mergeManager = new MergeManager();
    }

    public void Merge()
    {
        mergeManager.Merge(ObjectManager.selectedObjects);
    }

    public void Separate()
    {
        mergeManager.Separate(ObjectManager.selectedObjects, true);
    }
}
