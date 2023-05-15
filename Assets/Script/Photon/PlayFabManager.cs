using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;

public class PlayFabManager : MonoBehaviourPunCallbacks
{
    public TMP_InputField loginEmailInput;
    public TMP_InputField loginPasswoadInput;

    public TMP_InputField registerUserNameInput;
    public TMP_InputField registerEmailInput;
    public TMP_InputField registerPasswordInput;

    public TextMeshProUGUI coinText;
    public CanvasGroup failCanvas;
    public Canvas successCoinCanvas;

    public AudioSource mainAudioSource;
    public AudioSource lobbyAudioSource;
    public AudioClip getCoinSound;

    public Button joinButton;

    public LoginMenuController loginMenuController;
    public AudioClip clickSound;




    float timer;
    float fadetime = 3f;
    bool isfade = false;
    private void Update() {
        if( isfade == true){
            FadeOutCanvas();
        }
    }

    //아이디, 비밀번호를 inputfield에 넣고 로그인 버튼 클릭시, Playfab에 로그인 요청을 합니다.
    public void OnLoginButton()
    {
        lobbyAudioSource.Play();
        joinButton.interactable = false;
        var request = new LoginWithEmailAddressRequest {Email = loginEmailInput.text, Password = loginPasswoadInput.text};
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
    }
    //아이디, 비밀번호, 유저네임(닉네임)을 작성하고 회원가입버튼을 클릭시, Playfab에 Rigister요청을 합니다.
    public void RegisterButton(){
        lobbyAudioSource.Play();
        var request = new RegisterPlayFabUserRequest {Email = registerEmailInput.text, Password = registerPasswordInput.text,Username = registerUserNameInput.text, DisplayName = registerUserNameInput.text};
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnRegisterFailure);
    }

    // 로그인요청이 승인된다면 작동합니다. 
    private void OnLoginSuccess(LoginResult result)
    {
        // 플레이팹에서 플레이어 프로필 정보를 가져옵니다. 여기서는 닉네임에 사용할 username을 가져옵니다.
        PlayFabClientAPI.GetPlayerProfile( new GetPlayerProfileRequest() {
            PlayFabId = result.PlayFabId, 
                ProfileConstraints = new PlayerProfileViewConstraints() {
                    ShowDisplayName = true
            }
        },
        //정보를 가져오는데 성공했다면, 싱글톤인 게임매니저에 정보를 전달합니다.
        result => {
            GameManager.instance.userName = result.PlayerProfile.DisplayName.ToString();
            },
        error => Debug.LogError(error.GenerateErrorReport()));

        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), (result) => GameManager.instance.userCoin = result.VirtualCurrency["CO"], (error) => Debug.Log("Failed Check Coin"));
        //플레이팹 기본적인 정보를 받은 다음, 포톤 서버접속 시도
        PhotonNetwork.ConnectUsingSettings();
        joinButton.interactable = true;
    }
    
    //포톤서버 접속 성공시 메인씬이동합니다.
    public override void OnConnectedToMaster() {
        SceneManager.LoadScene("Main");
    }
    //포톤 접속 종료시 로그인씬으로 이동합니다
    public override void OnDisconnected(DisconnectCause cause)
    {
        SceneManager.LoadScene(0);
    }
   
   // 로그인이 실패시 로그인을 다시 시도하라는 캔버스를 작동시킵니다.
    private void OnLoginFailure(PlayFabError error)
    {
        joinButton.interactable = true;
        failCanvas.gameObject.SetActive(true);
        isfade = true;
        
    }
    //로그인 실패시 작동하는 켄버스입니다. 캔버스그룹을 이용하여, 알파값을 조절해서 알파값이 0이되는 순간, 캔버스를 끄고, 관련된 변수를 초기화합니다.
    void FadeOutCanvas(){
        if(timer < fadetime){
            timer += Time.deltaTime;
            failCanvas.alpha = Mathf.Lerp(1f, 0f, timer / fadetime);
        }
        else if ( timer >= fadetime){
            failCanvas.gameObject.SetActive(false);
            isfade = false;
            failCanvas.alpha = 1;
            timer = 0f;
        }
    }
    //회원가입에 성공하면 기존 로그인 캔버스를 가져옵니다.
    void OnRegisterSuccess(RegisterPlayFabUserResult result){
        loginMenuController.status = LoadingStatus.Main;
        loginMenuController.IsStatus();
    }
    //회원가입에 실패시 실패캔버스를 작동시킵니다.
    void OnRegisterFailure(PlayFabError error){
        failCanvas.gameObject.SetActive(true);
        isfade = true;
    }

    public void AddCoin(int amount){
        var request = new AddUserVirtualCurrencyRequest(){ VirtualCurrency = "CO", Amount = amount};
        PlayFabClientAPI.AddUserVirtualCurrency(request, (result) => {
            GameManager.instance.userCoin = result.Balance;
            successCoinCanvas.gameObject.SetActive(true);
            mainAudioSource.clip = getCoinSound;
            mainAudioSource.Play();
        },
        (error) => Debug.Log("Coin Add Failed"));
        
    }

    public void SubtractCoin(int amount)
    {
        var request = new SubtractUserVirtualCurrencyRequest(){VirtualCurrency = "CO", Amount = amount};
        PlayFabClientAPI.SubtractUserVirtualCurrency(request,(result) => GameManager.instance.userCoin = result.Balance, (error) => Debug.Log("Coin Subtract Failed"));
        
    }
}
