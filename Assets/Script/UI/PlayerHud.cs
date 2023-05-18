using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class PlayerHud : MonoBehaviourPunCallbacks
{
    public Transform player;
    public Canvas hudCanvas;
    public TextMeshProUGUI playerNameText;
    public Vector3 offset;
    public Vector3 roOffset;
    PlayerStatus status;
    public PhotonView pv;
    string playerName;

    private void Start() {
        ChangeName();
    }

    private void Update() {
        transform.position = player.position + offset;
        transform.eulerAngles = roOffset;

    }

    public void ChangeName(){
        if(pv.IsMine){
            playerNameText.text = PhotonNetwork.NickName;
            playerNameText.color = Color.white;
        }
        else{
            playerNameText.text = photonView.Owner.NickName;
            playerNameText.color = Color.red;
        }
    }
    
}
