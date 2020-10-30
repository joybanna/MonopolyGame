using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeMember : MonoBehaviour
{
    public List<Transform> positions;
    public List<Stone> stones = new List<Stone>(4);

    public void Register(Stone _stone)
    {
        stones.Add(_stone);
        _stone.nodeStay = this;
    }
    public void Unregister(Stone _stone)
    {
        stones.Remove(_stone);
        _stone.nodeStay = null;
    }
}
