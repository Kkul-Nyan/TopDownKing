using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerCharactor : MonoBehaviourPunCallbacks
{
    public int charactorSelectNumber;
    public GameObject[] bodyObject;
    public GameObject[] headObject;

    PhotonView pv;

    private void Awake() {
        pv = GetComponent<PhotonView>();
    }
    private void Start() {
        if(pv.IsMine){
            Invoke("ChooseCharactor",0.1f);
        }
    }

    
    [PunRPC]
    public void ChooseCharactor(){
        charactorSelectNumber = GameManager.instance.charactorSelectNumber;
        for(int i = 0; i < bodyObject.Length; i++){
            bodyObject[i].gameObject.SetActive(false);
            headObject[i].gameObject.SetActive(false);
        }
        bodyObject[charactorSelectNumber].gameObject.SetActive(true);
        headObject[charactorSelectNumber].gameObject.SetActive(true);


    }
}
