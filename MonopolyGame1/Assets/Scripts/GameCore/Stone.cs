using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    [HideInInspector] public GameControllerCenter gameControllerCenter;
    public TypeCharacter typeCharacter;
    public int subNodeStay;
    public NodeMember nodeStay;
    public Route currentRoute;
    private int routePosition;
    private int steps;
    private bool isMoving;
    public DetectNode detectNode;
    public float speedMove = 0.1f;

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
        detectNode.Detect(false);//unregister node
        Vector3 nextPos = new Vector3();

        while (steps > 0)//move foward
        {
            routePosition++;
            routePosition %= currentRoute.childNodeLists.Count;

            nextPos = currentRoute.childNodeLists[routePosition].position;
            transform.LookAt(LokAtPosition(nextPos));
            while (MoveToNextNode(nextPos))
            {
                yield return null;
            }
            gameControllerCenter.soundBox.PalySoundEffect("move");
            yield return new WaitForSeconds(speedMove);
            steps--;

        }
        while (steps < 0)//move backfoward
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

            transform.LookAt(LokAtPosition(nextPos));
            while (MoveToNextNode(nextPos))
            {
                yield return null;
            }
            gameControllerCenter.soundBox.PalySoundEffect("move");
            yield return new WaitForSeconds(speedMove);
            steps++;
        }

        if (routePosition < 0)//Look node
        {
            transform.LookAt(LokAtPosition(currentRoute.childNodeLists[(currentRoute.childNodeLists.Count + routePosition + 1)].position));
            routePosition = currentRoute.childNodeLists.Count + routePosition;
        }
        else
        {
            if ((routePosition + 1) < currentRoute.childNodeLists.Count)
            {
                transform.LookAt(LokAtPosition(currentRoute.childNodeLists[routePosition + 1].position));
            }

        }

        isMoving = false;
        detectNode.Detect(true);//register node
    }

    public void MoveStaySubNode(Vector3 _point)
    {
        transform.position = new Vector3(_point.x, transform.position.y, _point.z);
    }

    private bool MoveToNextNode(Vector3 _goal)
    {
        return _goal != (transform.position = Vector3.MoveTowards(transform.position, _goal, 8f * Time.deltaTime));
    }

    private Vector3 LokAtPosition(Vector3 _pos)
    {
        return new Vector3(_pos.x, this.transform.position.y, _pos.z);
    }
    public void SendNextPlayer()
    {
        currentRoute.gameControllerCenter.rPCController.SendTurnRunToMaster();
    }

}
