using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using TMPro;

public class PlayFabManager : MonoBehaviour
{
    public TMP_InputField loginEmailInput;
    public TMP_InputField loginPasswoadInput;

    public TMP_InputField registerUserNameInput;
    public TMP_InputField registerEmailInput;
    public TMP_InputField registerPasswordInput;

    public Canvas loginCanvas;
    public Canvas signCanvas;
    public Canvas mainMenuCanvas;
    public CanvasGroup failCanvas;
    public TextMeshProUGUI fadeText;

    public string username = "";


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
        var request = new LoginWithEmailAddressRequest {Email = loginEmailInput.text, Password = loginPasswoadInput.text};
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
    }
    //아이디, 비밀번호, 유저네임(닉네임)을 작성하고 회원가입버튼을 클릭시, Playfab에 Rigister요청을 합니다.
    public void RegisterButton(){
        var request = new RegisterPlayFabUserRequest {Email = registerEmailInput.text, Password = registerPasswordInput.text,Username = registerUserNameInput.text, DisplayName = registerUserNameInput.text};
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnRegisterFailure);
    }

    // 로그인요청이 승인된다면 작동합니다. 
    private void OnLoginSuccess(LoginResult result)
    {
        loginCanvas.gameObject.SetActive(false);
        mainMenuCanvas.gameObject.SetActive(true);
        
        // 플레이팹에서 플레이어 프로필 정보를 가져옵니다. 여기서는 닉네임에 사용할 username을 가져옵니다.
        PlayFabClientAPI.GetPlayerProfile( new GetPlayerProfileRequest() {
        PlayFabId = result.PlayFabId,
            ProfileConstraints = new PlayerProfileViewConstraints() {
                ShowDisplayName = true
            }
        },
        //정보를 가져오는데 성공했다면, 싱글톤인 게임매니저에 정보를 전달합니다.
        result => GameManager.instance.userName = result.PlayerProfile.DisplayName,
        error => Debug.LogError(error.GenerateErrorReport()));
    }
   
   // 로그인이 실패시 로그인을 다시 시도하라는 캔버스를 작동시킵니다.
    private void OnLoginFailure(PlayFabError error)
    {
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
        signCanvas.gameObject.SetActive(false);
        loginCanvas.gameObject.SetActive(true);
    }
    //회원가입에 실패시 실패캔버스를 작동시킵니다.
    void OnRegisterFailure(PlayFabError error){
        failCanvas.gameObject.SetActive(true);
        isfade = true;
    }
    
}
