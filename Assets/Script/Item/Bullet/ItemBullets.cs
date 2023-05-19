using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBullets : MonoBehaviour
{
    public ItemData bulletData; 

    private void OnTriggerEnter(Collider other) {
        if(!other.gameObject.CompareTag("Player")){
            return;
        }
        if(other.gameObject.CompareTag("Player")){
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            player.bullet = bulletData.dropPrefab.GetComponent<Bullets>();
            
            Destroy(transform.gameObject);
        }
    }
}
