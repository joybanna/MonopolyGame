using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SyncTransformStone : MonoBehaviourPunCallbacks

{
    private PhotonView photonView_player;
    private PhotonTransformView photonTransformView;
    private Vector3 temp_trans;
    public void SetSyncTransformStone(PhotonView _photonView_player)
    {
        photonView_player = _photonView_player;
        photonTransformView = GetComponent<PhotonTransformView>();
    }
    private void FixedUpdate()
    {
        if (photonView_player != null)
        {
            if (!photonView_player.IsMine)
            {
                this.transform.position = Vector3.Lerp(transform.position, temp_trans, Time.deltaTime * 2);
            }
        }

    }
    private void OnPhotonSerializeView(PhotonStream _stream, PhotonMessageInfo _info)
    {
        if (_stream.IsWriting)
        {
            _stream.SendNext(transform.position);
        }
        else
        {
            temp_trans = (Vector3)_stream.ReceiveNext();
        }
    }

}

