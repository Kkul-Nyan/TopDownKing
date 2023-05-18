using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHud : MonoBehaviour
{
    public Transform player;
    public Canvas hudCanvas;
    public TextMeshProUGUI playerNameText;
    public Vector3 offset;
    public Vector3 roOffset;
    PlayerStatus status;

    private void Start() {
        ChangeName();
    }

    private void Update() {
        transform.position = player.position + offset;
        transform.eulerAngles = roOffset;

    }
    public void ChangeName(){
        playerNameText.text = GameManager.instance.userName;
    }
}
