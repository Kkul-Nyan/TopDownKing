using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PlayerStatus : MonoBehaviourPunCallbacks, IDamagable, IPunObservable
{
    public Status health;
    public Status mana;
    public PhotonView pv;

    private void Update() {
        statusUpdate();
    }

    
    public void statusUpdate(){
        CheckisPlayerDead();

        mana.Add(mana.regenRate * Time.deltaTime);
        CheckStatus();
    }
    [PunRPC]
    void CheckStatus(){
        health.uiImage.fillAmount = health.GetPercent();
        mana.uiImage.fillAmount = mana.GetPercent();
    }

    public void CheckisPlayerDead(){
        if(health.curValue == 0f){
            Die();
        }
    }
    public void TakePhysicalDamage(int amount){
        health.Subtract(amount);
        pv.RPC("CheckStatus", RpcTarget.AllBuffered);
    }

    void Die(){
        Debug.Log( GameManager.instance.userName + "Die");
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){

    }

}

[System.Serializable]
public class Status
{
    public float curValue;
    public float maxValue;
    public float startValue;
    public float regenRate;
    public float decayRate;
    public Image uiImage;

    
    public void Add(float amount){
        curValue = Mathf.Min(curValue + amount, maxValue);
    }
    
    public void Subtract(float amount){
        curValue = Mathf.Max(curValue - amount, 0);
    }

    public float GetPercent(){
        return curValue / maxValue;
    }
    
}

public interface IDamagable 
{
    void TakePhysicalDamage(int damageAmount);
}