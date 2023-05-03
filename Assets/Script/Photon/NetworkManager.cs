using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    private void Awake() {

        //포톤서버와 얼마나 자주 데이터 송수신을 할지를 결정합니다.
        PhotonNetwork.SendRate = 60;
        PhotonNetwork.SerializationRate = 30;
    }

    //방참여를 시도합니다.
    public void Connect(){
        PhotonNetwork.ConnectUsingSettings();
    }

    //유저 닉네임을 로그인시 게임매니저에서 가지고있던 username값으로 해줍니다. 그후 방에 접속하거나 새로운 방을 만듭니다.
    public override void OnConnectedToMaster() {
        PhotonNetwork.LocalPlayer.NickName = GameManager.instance.userName;
         
        RoomOptions roomOption = new RoomOptions();
        roomOption.MaxPlayers = 2; //최대 인원수 설정
        roomOption.IsOpen = true; //방이 열려있는지 닫혀있는지 설정
        roomOption.IsVisible = true; //비공개 방 여부
        
        PhotonNetwork.JoinOrCreateRoom(GameManager.instance.userName, roomOption, null);
    }

    //방 접속시 게임1번 씬으로 이동합니다.
    public override void OnJoinedRoom()
    {
        SceneManager.LoadScene(1);
        Debug.Log("현재 방 이름 : " + PhotonNetwork.CurrentRoom.Name);
        Debug.Log("현재 방 인원수 : " + PhotonNetwork.CurrentRoom.PlayerCount);
        Debug.Log("현재 방 최대인원수 : " + PhotonNetwork.CurrentRoom.MaxPlayers);
        Debug.Log("현재 방 열려있는지 : " + PhotonNetwork.CurrentRoom.IsOpen);
        Debug.Log("현재 방 비공개 여부 : " + PhotonNetwork.CurrentRoom.IsVisible);
        
    }

    // 방접속 종료시 메인화면씬으로 전환합니다.
    public override void OnDisconnected(DisconnectCause cause)
    {
        SceneManager.LoadScene(0);
    }
}
