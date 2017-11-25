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
            newParent.transform.position = avg;

            for (int i = 0; i < selectedObjects.Count; i++)
            {
                Selector selector = selectedObjects[i].GetComponent<Selector>();
                float rndX = Random.Range(
                    0.65f,
                    0.9f);

                float rndY = Random.Range(
                    0.65f,
                    0.9f);

                rndX = Mathf.Round(rndX * 100) / 100;
                rndY = Mathf.Round(rndY * 100) / 100;

                rndX = RandomInvert(rndX);
                rndY = RandomInvert(rndY);

                Debug.Log(rndX);
                Debug.Log(rndY);

                Debug.Log("pos: " + selectedObjects[i].transform.position);

                selectedObjects[i].transform.position = new Vector3(
                    rndX,
                    rndY,
                    selectedObjects[i].transform.position.z);

                selector.DeSelect(true);
                selector.merged = true;
            }

            newParent.AddComponent<Selector>();
            selectedObjects.Clear();
        }
    }

    private static float RandomInvert(float random)
    {
        if(Random.Range(0f, 1f) > 0.5f)
        {
            random *= -1;
        }

        return random;
    }

}
