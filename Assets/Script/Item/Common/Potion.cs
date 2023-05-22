using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public enum PotionType{
    HPPotion,
    ManaPotion
}
public class Potion : MonoBehaviour
{
    private PhotonView pv;
    public int potionID;
    public float recoyValue;
    public PotionType type;
    PlayerStatus playerStatus;

    private void Awake() {
        pv = GetComponent<PhotonView>();
    }
    private void OnTriggerEnter(Collider other) {
        playerStatus = other.GetComponent<PlayerStatus>();
        if(playerStatus != null){
            switch(type){
                case PotionType.HPPotion :
                    playerStatus.health.Add(recoyValue);
                    pv.RPC("DestroyPotion", RpcTarget.AllBuffered);
                break;
                case PotionType.ManaPotion :
                    playerStatus.mana.Add(recoyValue);
                    pv.RPC("DestroyPotion", RpcTarget.AllBuffered);
                break;
            }
        }
    }

    [PunRPC]
    void DestroyPotion(){
        Destroy(transform.gameObject);
    }
}
