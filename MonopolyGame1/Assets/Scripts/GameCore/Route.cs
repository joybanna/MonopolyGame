using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    private Transform[] childObjects;
    public List<Transform> childNodeLists = new List<Transform>();
    public List<NodeMember> nodeMemberList = new List<NodeMember>();

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        FillNode();
        GiveIndexForNodeMember();
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
        nodeMemberList.Clear();
        childObjects = GetComponentsInChildren<Transform>();
        foreach (Transform child in childObjects)
        {
            if (child != this.transform)
            {
                if (child.GetComponent<NodeMember>() != null)
                {
                    nodeMemberList.Add(child.GetComponent<NodeMember>());
                    childNodeLists.Add(child);
                }
            }
        }
    }
    private void GiveIndexForNodeMember()
    {
        for (int i = 0; i < nodeMemberList.Count; i++)
        {
            nodeMemberList[i].indexNode = i;
        }
    }
}
