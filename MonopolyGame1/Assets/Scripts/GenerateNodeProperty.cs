using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateNodeProperty : MonoBehaviour
{
    [HideInInspector] public GameControllerCenter gameControllerCenter;
    private List<NodeMember> nodeMembers;
    private List<int> nodePositive, nodeNegative;
    private List<int> valuePositive, valueNegative;
    private List<NodeProperties> nodeEffects;
    private List<NodeMember> temp_nodeMembers;
    public void SettingStart()
    {
        nodeEffects = new List<NodeProperties>();
        nodeMembers = new List<NodeMember>(gameControllerCenter.route.nodeMemberList);
        nodePositive = new List<int>();
        nodeNegative = new List<int>();
        valuePositive = new List<int>();
        valueNegative = new List<int>();
        temp_nodeMembers = new List<NodeMember>(nodeMembers);

        //StartGenProp();
    }
    public void StartGenProp()
    {
        for (var i = 0; i < 2; i++)
        {
            GenNodeProp(nodePositive, valuePositive);
            GenNodeProp(nodeNegative, valueNegative);
        }
        CreateNodeProp(nodePositive, valuePositive, "blue");
        CreateNodeProp(nodeNegative, valueNegative, "red");
    }
    public void GenNodeProp(List<int> _nodeadd, List<int> _valueadd)
    {
        int temp_index = Random.Range(3, temp_nodeMembers.Count - 3);
        int temp_value = Random.Range(1, 3);
        //Debug.Log("temp_index " + temp_nodeMembers[temp_index].indexNode + " temp_value " + temp_value);
        _valueadd.Add(temp_value);
        _nodeadd.Add(temp_nodeMembers[temp_index].indexNode);
        temp_nodeMembers.Remove(temp_nodeMembers[temp_index]);
    }
    public void CreateNodeProp(List<int> _nodeadd, List<int> _value, string _color)
    {

        int temp = 0;
        for (int i = 0; i < nodeMembers.Count; i++)
        {
            if (temp < _nodeadd.Count)
            {
                if (nodeMembers[i].indexNode == _nodeadd[temp])
                {
                    //Debug.Log(nodeMembers[i].indexNode + " node.indexNode " + _nodeadd[temp]);
                    SettingNodeProp(nodeMembers[i].indexNode, _value[temp], _color);
                    string code = nodeMembers[i].indexNode.ToString() + "|" + _value[temp].ToString() + "|" + _color;
                    //Debug.Log("code " + code);
                    gameControllerCenter.rPCController.SettingNodeProp(code);
                    temp++;
                }
            }

        }
    }
    public void SettingNodeProp(int _index, int _vaule, string _color)
    {
        foreach (NodeMember node in nodeMembers)
        {
            if (node.indexNode == _index)
            {
                node.gameObject.AddComponent<NodeProperties>();
                NodeProperties temp = node.GetComponent<NodeProperties>();
                temp.SettingStart();
                temp.isContinueMove = true;
                switch (_color)
                {
                    case "blue":
                        temp.stepsContinueMove = _vaule;
                        break;
                    case "red":
                        temp.stepsContinueMove = _vaule * -1;
                        break;
                    default: break;
                }

                temp.ChangeColorBlock(_color);
            }
        }

    }

}
