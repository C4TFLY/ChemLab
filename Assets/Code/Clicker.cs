using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clicker : MonoBehaviour {

    public static List<GameObject> selectedObjects;

	// Use this for initialization
	void Start () {
        selectedObjects = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
        print(selectedObjects.Count);
	}
}
