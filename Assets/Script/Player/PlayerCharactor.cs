using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class PlayerCharactor : MonoBehaviour
{
    public int charactorSelectNumber;
    public GameObject[] bodyObject;
    public GameObject[] headObject;

    private void Start() {
        ChooseCharactor();
    }

    [Button]
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
