using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHud : MonoBehaviour
{
    public Canvas hudCanvas;
    public TextMeshProUGUI playerNameText;

    private void Start() {
        ChangeName();
    }
    public void ChangeName(){
        playerNameText.text = GameManager.instance.userName;
    }
}
