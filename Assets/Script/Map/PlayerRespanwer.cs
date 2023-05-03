using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespanwer : MonoBehaviour
{
    public GameObject player;
    private void Start() {
        RandomPositin();
      
    }
    void RandomPositin(){
        float positionX = Random.Range(3, 147);
        float positionZ = Random.Range(3, 87);
        Vector3 randomPosition = new Vector3(positionX, 1.5f, positionZ);

        Instantiate(player, randomPosition, Quaternion.identity);

    }
}
