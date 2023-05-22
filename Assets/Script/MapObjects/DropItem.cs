using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public enum DropType {
    dropBullet,
    dropConsumableItem
}
public class DropItem : MonoBehaviourPunCallbacks
{
    public DropType dropType;
    public ItemData[] items;


    public void Drop(){
        switch (dropType) {
            case DropType.dropBullet :
                DropBullet();
            break;
            case DropType.dropConsumableItem :
                DropConsumableItem();
            break;
        }
    }

    void DropBullet(){
        int dropItemNum = Random.Range(0, items.Length);
        if(items[dropItemNum].type != ItemType.Bullet){
            DropBullet();
            return;
        }
        Bullets bullet = items[dropItemNum].dropPrefab.GetComponent<Bullets>();
        Vector3 bulletPosition =  transform.position + new Vector3(0, 1, 0);
        PhotonNetwork.Instantiate("itemBullet2", bulletPosition, Quaternion.identity);
        Debug.Log("itemBullet"+ bullet.bulletID +"Instantiate");
    }

    void DropConsumableItem(){
        int dropItemNum = Random.Range(0, items.Length);
        if(items[dropItemNum].type != ItemType.Consumable){
            DropConsumableItem();
            return;
        }
        Potion potion = items[dropItemNum].dropPrefab.GetComponent<Potion>();
        Vector3 rotation =  new Vector3(-90,0,0);
        PhotonNetwork.Instantiate("Potion" + potion.potionID, transform.position, Quaternion.Euler(rotation));
    }
}