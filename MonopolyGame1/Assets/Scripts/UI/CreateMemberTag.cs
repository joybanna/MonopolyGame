using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class CreateMemberTag : MonoBehaviourPunCallbacks
{
    public MemberTag memberTag_prefab;
    public Transform content;
    public List<MemberTag> playerslistTag = new List<MemberTag>(4);


    bool CheckMemberTag(Player _player)
    {
        foreach (MemberTag item in playerslistTag)
        {
            if (item.nameplayer_str == _player.NickName)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        return true;
    }
    public void CreatTag(Player _player)
    {
        if (CheckMemberTag(_player))
        {
            MemberTag tagtemp = Instantiate<MemberTag>(memberTag_prefab, content);
            tagtemp.player_network = _player;
            tagtemp.SetMemberTag();
            playerslistTag.Add(tagtemp);
        }
    }
    public void ClearTag()
    {
        if (playerslistTag.Count != 0)
        {
            for (int i = 0; i < playerslistTag.Count; i++)
            {
                Destroy(playerslistTag[i].gameObject);

            }
            playerslistTag.Clear();
        }
    }
    public void RemoveTag(Player _player)
    {
        Debug.Log("RemoveTag : " + _player.NickName);

        foreach (MemberTag item in playerslistTag)
        {
            if (item.player_network.NickName == _player.NickName)
            {
                playerslistTag.Remove(item);
                Destroy(item.gameObject);
                Debug.Log("Removed Tag");
                break;
            }
        }
    }
}
