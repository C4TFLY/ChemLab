using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour {

    public static List<GameObject> selectedObjects = new List<GameObject>();


    private static int x = 0;

    public static void Merge()
    {
        if (selectedObjects.Count > 1)
        {
            GameObject newParent = new GameObject($"Parent{x}");
            x++;
            float xSum = 0;
            float ySum = 0;

            for (int i = 0; i < selectedObjects.Count; i++)
            {
                selectedObjects[i].transform.SetParent(newParent.transform);
                
                foreach(GameObject obj in selectedObjects)
                {
                    Physics.IgnoreCollision(obj.GetComponent<SphereCollider>(), selectedObjects[i].GetComponent<SphereCollider>());
                }



                xSum += selectedObjects[i].transform.position.x;
                ySum += selectedObjects[i].transform.position.y;  
            }

            float xAvg = xSum / selectedObjects.Count;
            float yAvg = ySum / selectedObjects.Count;

            Vector3 avg = new Vector3(xAvg, yAvg, 0);

            for (int i = 0; i < selectedObjects.Count; i++)
            {
                Selector selector = selectedObjects[i].GetComponent<Selector>();
                float rnd = Random.Range(
                    0.65f,
                    0.9f);

                if (Random.Range(0f, 1f) > 0.5f)
                {
                    rnd *= -1;
                }

                selectedObjects[i].transform.position = new Vector3(
                    avg.x + rnd,
                    avg.y + rnd,
                    selectedObjects[i].transform.position.z);

                selector.DeSelect(true);
                selector.merged = true;
            }

            newParent.AddComponent<Selector>();
            selectedObjects.Clear();
        }
    }
}
