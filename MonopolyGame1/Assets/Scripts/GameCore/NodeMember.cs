using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeMember : MonoBehaviour
{
    public List<Transform> all_Positions;
    public List<Stone> stones = new List<Stone>(4);
    private bool[] isStays = new bool[4];

    private void Start()
    {
        all_Positions = new List<Transform>();

        Transform[] temp = new Transform[5];
        temp = GetComponentsInChildren<Transform>();
        foreach (Transform obj in temp)
        {
            if (obj.name == "point")
            {
                all_Positions.Add(obj.GetComponent<Transform>());
            }
        }

        for (int i = 0; i < isStays.Length; i++)
        {
            isStays[i] = false;
        }
        //Debug.Log("NodeMember ready");
    }

    public void Register(Stone _stone)
    {
        stones.Add(_stone);
        _stone.nodeStay = this;

        _stone.subNodeStay = PositionOpen();
        isStays[_stone.subNodeStay] = true;
        _stone.MoveStaySubNode(all_Positions[_stone.subNodeStay].position);
    }
    public void Unregister(Stone _stone)
    {
        stones.Remove(_stone);
        _stone.nodeStay = null;

        isStays[_stone.subNodeStay] = false;
    }

    private int PositionOpen()
    {
        for (int i = 0; i < isStays.Length; i++)
        {
            if (isStays[i] == false)
            {
                return i;
            }
        }
        Debug.LogWarning("member more 4 !!!");
        return 0;
    }
}
