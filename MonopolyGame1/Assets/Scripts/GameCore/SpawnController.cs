using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public List<Stone> stones_players;
    public List<DataPlayer> dataPlayers;
    public Route route;
    private void Start()
    {
        for (int i = 0; i < stones_players.Count; i++)
        {
            SetStone(i);
        }

    }
    void CreateDataPlayer()
    {

    }
    void SetStone(int _no)
    {
        stones_players[_no].transform.position = route.childNodeLists[0].position;
        stones_players[_no].detectNode.Detect(true);
    }
}
