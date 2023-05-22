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

   
    public void Connect(){
        PhotonNetwork.ConnectUsingSettings();
    }

    public void JoinRandomRoom()
    {
        PhotonNetwork.NickName = GameManager.instance.userName;
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Join random failed, creating new room.");
        RoomOptions options = new RoomOptions {MaxPlayers = (byte) 4};
        PhotonNetwork.CreateRoom(null, options, null);
    }
    /*
    public void JoinorCreateRoom(){
        PhotonNetwork.LocalPlayer.NickName = GameManager.instance.userName;
         
        RoomOptions roomOption = new RoomOptions();
        roomOption.MaxPlayers = 2; //최대 인원수 설정
        roomOption.IsOpen = true; //방이 열려있는지 닫혀있는지 설정
        roomOption.IsVisible = false; //비공개 방 여부
        
        PhotonNetwork.JoinOrCreateRoom(PhotonNetwork.LocalPlayer.NickName,roomOption, null);
        Debug.Log("랜덤룸 생성 및 접속시동중");
    }
    */
    //방 접속시 게임1번 씬으로 이동합니다.
    public override void OnJoinedRoom()
    {
        //룸접속완료
        Debug.Log("현재 방 이름 : " + PhotonNetwork.CurrentRoom.Name);
        Debug.Log("현재 방 인원수 : " + PhotonNetwork.CurrentRoom.PlayerCount);
        Debug.Log("현재 방 최대인원수 : " + PhotonNetwork.CurrentRoom.MaxPlayers);
        Debug.Log("현재 방 열려있는지 : " + PhotonNetwork.CurrentRoom.IsOpen);
        Debug.Log("현재 방 비공개 여부 : " + PhotonNetwork.CurrentRoom.IsVisible);
        
        PhotonNetwork.LoadLevel(2);
    }

    // 방접속 종료시 메인화면씬으로 전환합니다.
    public override void OnDisconnected(DisconnectCause cause)
    {
        SceneManager.LoadScene(1);
    }

    public void Disconnected(){
        PhotonNetwork.Disconnect();
        Connect();
        //SceneManager.LoadScene(0);
    }

    

 



}
