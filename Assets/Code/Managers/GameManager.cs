using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static float deltaTime
    {
        get;
        private set;
    }

	// Use this for initialization
	void Start () {
		
	}

    private void FixedUpdate()
    {
        deltaTime = Time.deltaTime;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
