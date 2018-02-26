using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;

public class MergeManager : MonoBehaviour {
    
    private int x;

    public void Merge(List<GameObject> selectedObjects)
    {
        //If there's less than 2 items in selectedObjects, return
        if (selectedObjects.Count <= 1)
        {
            return;
        }
        x++;

        //Create the parent for the merge
        //GameObject newParent = new GameObject($"Parent{x}");
        GameObject newParent = new GameObject("Parent" + x);

        //For getting the average x and y values
        float xSum = 0;
        float ySum = 0;

        //List of the angles that objects around the center can be placed at
        List<int> angles = new List<int>();
        for (int i = 0; i < 6; i++)
        {
            angles.Add(60 * i);
        }

        for (int i = 0; i < selectedObjects.Count; i++)
        {
            //Loop through every object in the list and disable collisions
            foreach (GameObject obj in selectedObjects)
            {
                Physics.IgnoreCollision(obj.GetComponent<SphereCollider>(), selectedObjects[i].GetComponent<SphereCollider>());
            }

            //Set the parent and stop movement
            selectedObjects[i].transform.SetParent(newParent.transform);
            selectedObjects[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
            
            //For average position
            xSum += selectedObjects[i].transform.position.x;
            ySum += selectedObjects[i].transform.position.y;
        }

        //Calculate the average position and set the parent's position
        float xAvg = xSum / selectedObjects.Count;
        float yAvg = ySum / selectedObjects.Count;
        Vector3 avg = new Vector3(xAvg, yAvg, 0);
        newParent.transform.position = avg;

        //Make one object the center and deselect it
        selectedObjects[0].transform.position = newParent.transform.position;
        selectedObjects[0].GetComponent<Selector>().DeSelect();
        selectedObjects[0].GetComponent<Selector>().merged = true;

        for (int i = 1; i < selectedObjects.Count; i++)
        {
            Selector selector = selectedObjects[i].GetComponent<Selector>();
            
            //Assign one of the not selected angles from the list
            int angleListPos = Random.Range(0, angles.Count);
            int randomAngle = angles[angleListPos];
            angles.Remove(randomAngle);

            //Set the position based on the angle
            selectedObjects[i].transform.position = new Vector3(
                newParent.transform.position.x + (Mathf.Cos((randomAngle * Mathf.PI) / 180)),
                newParent.transform.position.y + (Mathf.Sin((randomAngle * Mathf.PI) / 180)),
                0);

            selector.DeSelect();
            selector.merged = true;
        }

        newParent.AddComponent<Selector>();
        selectedObjects.Clear();
    }

    public void Separate(List<GameObject> selectedObjects, bool explode = false)
    {
        List<Transform> children = new List<Transform>();
        if (selectedObjects.Count < 1)
        {
            return;
        }

        foreach(GameObject parent in selectedObjects)
        {
            if (parent.transform.childCount == 0)
            {
                parent.GetComponent<Selector>().DeSelect();
                return;
            }
            Bounds bounds = new Bounds(parent.transform.GetChild(0).position, Vector3.zero);

            for (int k = 0; k < parent.transform.childCount; k++)
            {
                Transform child = parent.transform.GetChild(k);
                Selector selector = child.GetComponent<Selector>();

                children.Add(child);
                selector.DeSelect();
                selector.merged = false;
                bounds.Encapsulate(child.position);

                if (explode)
                {
                    Collider[] colliders = Physics.OverlapSphere(bounds.center, 10.0f);
                    foreach (Collider hit in colliders)
                    {
                        Rigidbody rb = hit.GetComponent<Rigidbody>();

                        if (rb != null)
                        {
                            rb.AddExplosionForce(500f, bounds.center, 10.0f);
                            rb.drag = 1;
                        }
                    }
                }
            }

            parent.transform.DetachChildren();
            Destroy(parent);
        }

        StartCoroutine(CollisionEnable(children, .25f));
        selectedObjects.Clear();
    }

    private IEnumerator CollisionEnable(List<Transform> objects, float delay = 1.0f)
    {
        yield return new WaitForSeconds(delay);
        foreach (Transform child in objects)
        {
            foreach (Transform sibling in objects)
            {
                Physics.IgnoreCollision(child.gameObject.GetComponent<SphereCollider>(), sibling.gameObject.GetComponent<SphereCollider>(), false);
            }
            print(child);
            child.GetComponent<FloatingController>().slowDown = true;
        }

    }

    private void CheckMerge(List<GameObject> objects)
    {
        DBConnect dbc = new DBConnect();
        IDbConnection conn = dbc.GetConnection();
        int protons = 0;
        int neutrons = 0;

        foreach(GameObject obj in objects)
        {
            if (obj.tag == "Proton")
            {
                protons++;
            }
            else if (obj.tag == "Neutrons")
            {
                neutrons = 0;
            }
        }

    }
}
