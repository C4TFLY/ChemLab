using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeManager : MonoBehaviour {
    
    private int x;

    public MergeManager()
    {

    }

    public void Merge(List<GameObject> selectedObjects)
    {
        if (selectedObjects.Count <= 1)
        {
            return;
        }

        GameObject newParent = new GameObject($"Parent{x}");
        x++;
        float xSum = 0;
        float ySum = 0;

        for (int i = 0; i < selectedObjects.Count; i++)
        {
            selectedObjects[i].transform.SetParent(newParent.transform);
            selectedObjects[i].GetComponent<Rigidbody>().velocity = Vector3.zero;

            foreach (GameObject obj in selectedObjects)
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

        selectedObjects[0].transform.position = newParent.transform.position;
        selectedObjects[0].GetComponent<Selector>().DeSelect();
        selectedObjects[0].GetComponent<Selector>().merged = true;

        for (int i = 1; i < selectedObjects.Count; i++)
        {
            Selector selector = selectedObjects[i].GetComponent<Selector>();

            float randomAngle = Random.Range(
                0f,
                360f);

            selectedObjects[i].transform.position = new Vector3(
                newParent.transform.position.x + (Mathf.Cos(randomAngle)),
                newParent.transform.position.y + (Mathf.Sin(randomAngle)),
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
            for (int k = 0; k < parent.transform.childCount; k++)
            {
                Transform child = parent.transform.GetChild(k);
                children.Add(child);
                Selector selector = child.GetComponent<Selector>();
                selector.DeSelect();
                selector.merged = false;
            }

            parent.transform.DetachChildren();
            Destroy(parent);
        }

        if (explode)
        {
            foreach (GameObject parent in selectedObjects)
            {
                Debug.Log("kaboom");

                Bounds bounds = new Bounds(children[0].position, Vector3.zero);
                for (int i = 0; i < children.Count; i++)
                {
                    bounds.Encapsulate(children[i].position);
                }

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
        CollisionEnableDelay(.25f, children);
        selectedObjects.Clear();
    }

    private IEnumerable CollisionEnableDelay(float delay, List<Transform> objects)
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
}
