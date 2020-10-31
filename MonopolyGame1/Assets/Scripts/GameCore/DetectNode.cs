using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectNode : MonoBehaviour
{
    public StatusStone statusStone;
    public Transform detector;
    public float length;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(detector.position, new Vector3(detector.position.x, detector.position.y - length, detector.position.z));

    }
    public void Detect(bool isRegister)
    {
        RaycastHit hit;
        if (Physics.Raycast(detector.position, detector.TransformDirection(Vector3.down), out hit, length))
        {
            //Debug.Log("hit : " + hit.collider.gameObject.name);

            if (hit.collider.gameObject.GetComponent<NodeMember>() != null)
            {
                NodeMember nodeMember = hit.collider.gameObject.GetComponent<NodeMember>();

                if (isRegister)
                {
                    if (hit.collider.gameObject.GetComponent<NodeProperties>() != null)
                    {
                        NodeProperties nodeProperties = hit.collider.gameObject.GetComponent<NodeProperties>();
                        nodeProperties.CheckPropertiesNode(statusStone);
                    }
                    nodeMember.Register(statusStone.stone);
                }
                else
                {
                    nodeMember.Unregister(statusStone.stone);
                }

            }
        }
    }
}