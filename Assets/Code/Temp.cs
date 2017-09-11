using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp : MonoBehaviour {
    
    private void OnCollisionEnter(Collision collision)
    {
        List<GameObject> go = new List<GameObject>
        {
            collision.gameObject,
            gameObject
        };

        AtomManager.Merge(go.ToArray());
    }

}
