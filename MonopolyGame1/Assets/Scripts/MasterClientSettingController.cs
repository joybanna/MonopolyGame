using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class MasterClientSettingController : MonoBehaviourPunCallbacks
{
    public SpawnController spawnController;
    public List<DataPlayer> dataPlayers;

    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            spawnController.stones_players = new List<Stone>();
            dataPlayers = new List<DataPlayer>();
            SettingBegin();
        }
    }
    void SettingBegin()
    {
        CreatePlayerStone();
        spawnController.StartSpawnController();
    }
    void CreatePlayerStone()
    {
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            SpawnStone(player);
        }
    }
    void SpawnStone(Player _player)
    {
        GameObject playerStone = PhotonNetwork.Instantiate("stoneplayer_prefab", Vector3.zero, Quaternion.Euler(Vector3.zero));
        Stone stone_script = playerStone.GetComponent<Stone>();
        DataPlayer dataPlayer = new DataPlayer(_player.NickName, (TypeCharacter)_player.CustomProperties["TypeCharacter"], 0);
        ModelStoneController modelStoneController = playerStone.GetComponent<ModelStoneController>();
        modelStoneController.StartModelStoneController(dataPlayer.playerType);
        dataPlayers.Add(dataPlayer);
        spawnController.stones_players.Add(stone_script);
    }
}
