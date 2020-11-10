using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public List<Stone> stones_players;

    public Route route;

    public void StartSpawnController()
    {
        for (int i = 0; i < stones_players.Count; i++)
        {
            SetStone(i);
        }
    }
    void SetStone(int _no)
    {
        stones_players[_no].transform.position = route.childNodeLists[0].position;
        stones_players[_no].detectNode.Detect(true);
    }
}
