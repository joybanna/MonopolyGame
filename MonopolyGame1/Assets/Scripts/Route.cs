using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    private Transform[] childObjects;
    public List<Transform> childNodeLists = new List<Transform>();

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        FillNode();
        for (int i = 0; i < childNodeLists.Count; i++)
        {
            Vector3 currentPos = childNodeLists[i].position;
            if (i > 0)
            {
                Vector3 prePos = childNodeLists[i - 1].position;
                Gizmos.DrawLine(prePos, currentPos);
            }
        }

    }

    private void FillNode()
    {
        childNodeLists.Clear();
        childObjects = GetComponentsInChildren<Transform>();
        foreach (Transform child in childObjects)
        {
            if (child != this.transform)
            {
                childNodeLists.Add(child);
            }
        }
    }
}
