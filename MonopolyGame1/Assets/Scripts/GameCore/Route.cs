using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    public GameControllerCenter gameControllerCenter;
    private Transform[] childObjects;
    public List<Transform> childNodeLists = new List<Transform>();
    public List<NodeMember> nodeMemberList = new List<NodeMember>();

    public void SettingRoute()
    {
        FillNode();
    }
    /* private void OnDrawGizmos()
     {
         Gizmos.color = Color.green;
         for (int i = 0; i < childNodeLists.Count; i++)
         {
             Vector3 currentPos = childNodeLists[i].position;
             if (i > 0)
             {
                 Vector3 prePos = childNodeLists[i - 1].position;
                 Gizmos.DrawLine(prePos, currentPos);
             }
         }

     }*/
    private void FillNode()
    {
        childNodeLists.Clear();
        nodeMemberList.Clear();
        childObjects = GetComponentsInChildren<Transform>();
        for (int i = 0; i < childObjects.Length; i++)
        {
            if (childObjects[i] != this.transform)
            {
                if (childObjects[i].GetComponent<NodeMember>() != null)
                {
                    childObjects[i].GetComponent<NodeMember>().SettingNodeMember(this);
                    nodeMemberList.Add(childObjects[i].GetComponent<NodeMember>());
                    childNodeLists.Add(childObjects[i]);
                }
            }
        }

        GiveIndexForNodeMember();
    }
    private void GiveIndexForNodeMember()
    {
        for (int i = 0; i < nodeMemberList.Count; i++)
        {
            nodeMemberList[i].indexNode = i;
        }
    }
}
