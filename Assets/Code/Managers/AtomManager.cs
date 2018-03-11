using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtomManager : MonoBehaviour {

    public Atom atom;

    // Use this for initialization
    void Start () {
        print(atom.valid);
        print(atom.Name);
        print(atom.Isotope);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
