using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeMember : MonoBehaviour
{
    public bool isFinish;
    public int indexNode;
    public List<Transform> all_Positions;
    public bool[] isStays = new bool[4];
    private RPCController rPCController;
    public void SettingNodeMember(Route _route)
    {
        rPCController = _route.gameControllerCenter.rPCController;
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
        //stones.Add(_stone);
        _stone.nodeStay = this;

        _stone.subNodeStay = PositionOpen();
        isStays[_stone.subNodeStay] = true;

        rPCController.RegisterNode(ExportCode(indexNode, _stone.subNodeStay));
        rPCController.SendReadyToMaster();
        _stone.MoveStaySubNode(all_Positions[_stone.subNodeStay].position);


    }
    public void Unregister(Stone _stone)
    {
        //stones.Remove(_stone);
        _stone.nodeStay = null;
        isStays[_stone.subNodeStay] = false;

        rPCController.UnregisterNode(ExportCode(indexNode, _stone.subNodeStay));
    }

    private int PositionOpen()
    {
        for (int i = 0; i < isStays.Length; i++)
        {
            if (isStays[i] == false)
            {
                //Debug.Log("PositionOpen point : " + i);
                return i;
            }
            else
            {
                //Debug.Log("PositionOpen point : " + i + " close");
            }
        }
        Debug.LogWarning("member more 4 !!!");
        return 0;
    }

    private string ExportCode(int _node, int _subnode)
    {
        string temp = _node.ToString() + "|" + _subnode.ToString();
        return temp;
    }

}
