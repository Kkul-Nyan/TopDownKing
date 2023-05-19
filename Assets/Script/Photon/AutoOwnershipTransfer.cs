using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class AutoOwnershipTransfer : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject playerObject;
    private PhotonView photonView;

    
    void Start()
    {
        playerObject = GetComponent<GameObject>();
        photonView = playerObject.GetComponent<PhotonView>();
        if (photonView.Owner == PhotonNetwork.LocalPlayer)
        {
            photonView.TransferOwnership(PhotonNetwork.MasterClient);
        }
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        if (photonView.Owner == PhotonNetwork.LocalPlayer)
        {
            photonView.TransferOwnership(newMasterClient);
        }
    }
}