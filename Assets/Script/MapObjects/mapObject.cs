using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class mapObject : MonoBehaviour, IDamagable
{
    public PhotonView pv;
    public int maxHealth;
    public int curhealth;
    Renderer renderer;
    Color startColor;
    public Color destroyColor;

 
    private void Start() {
        renderer = GetComponent<Renderer>();
        startColor = renderer.material.color; 
    }

    private void Update() {
        
    }

    public void TakePhysicalDamage(int amount){
        curhealth -= amount;
        pv.RPC("HitColor", RpcTarget.All);
        CheckDestroy();
    }

    [PunRPC]
    void Die() => Destroy(transform.gameObject);

    [PunRPC]
    void HitColor(){
        float  t = (float)(maxHealth - curhealth) / (float)maxHealth;
        renderer.material.color = Color.Lerp(startColor, destroyColor, t);
    }

    void CheckDestroy(){
        if( curhealth <= 0){
            pv.RPC("Die", RpcTarget.All);
        }
    }

}
