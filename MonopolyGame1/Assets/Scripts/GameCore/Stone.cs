using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    public int subNodeStay;
    public NodeMember nodeStay;
    public Route currentRoute;
    private int routePosition;
    private int steps;
    private bool isMoving;
    public DetectNode detectNode;
    public void MoveSteps(int _steps)
    {
        steps = _steps;
        StartCoroutine(Move());
    }
    private IEnumerator Move()
    {
        if (isMoving)
        {
            yield break;
        }


        isMoving = true;
        detectNode.Detect(false);
        Vector3 nextPos = new Vector3();

        while (steps > 0)
        {
            routePosition++;
            routePosition %= currentRoute.childNodeLists.Count;

            nextPos = currentRoute.childNodeLists[routePosition].position;
            transform.LookAt(nextPos);
            while (MoveToNextNode(nextPos))
            {
                yield return null;
            }

            yield return new WaitForSeconds(0.1f);
            steps--;

        }
        while (steps < 0)
        {
            routePosition--;
            routePosition %= currentRoute.childNodeLists.Count;
            if (routePosition < 0)
            {
                nextPos = currentRoute.childNodeLists[currentRoute.childNodeLists.Count + routePosition].position;
            }
            else
            {
                nextPos = currentRoute.childNodeLists[routePosition].position;
            }

            transform.LookAt(nextPos);
            while (MoveToNextNode(nextPos))
            {
                yield return null;
            }

            yield return new WaitForSeconds(0.1f);
            steps++;
        }

        if (routePosition < 0)
        {
            transform.LookAt(currentRoute.childNodeLists[(currentRoute.childNodeLists.Count + routePosition + 1)].position);
            routePosition = currentRoute.childNodeLists.Count + routePosition;
        }
        else
        {
            if ((routePosition + 1) < currentRoute.childNodeLists.Count)
            {
                transform.LookAt(currentRoute.childNodeLists[routePosition + 1].position);
            }

        }

        isMoving = false;
        detectNode.Detect(true);

    }
    public void MoveStaySubNode(Vector3 _point)
    {
        transform.position = new Vector3(_point.x, transform.position.y, _point.z);
    }

    private bool MoveToNextNode(Vector3 _goal)
    {
        return _goal != (transform.position = Vector3.MoveTowards(transform.position, _goal, 8f * Time.deltaTime));
    }

}
