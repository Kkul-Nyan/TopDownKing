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

    float timer;
    float fadetime = 3f;
    bool isfade = false;
    private void Update() {
        if( isfade == true){
            FadeOutCanvas();
        }
    }

    public void OnLoginButton()
    {
        var request = new LoginWithEmailAddressRequest {Email = loginEmailInput.text, Password = loginPasswoadInput.text};
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
    }

    public void RegisterButton(){
        var request = new RegisterPlayFabUserRequest {Email = registerEmailInput.text, Password = registerPasswordInput.text, Username = registerUserNameInput.text};
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnRegisterFailure);
    }

    private void OnLoginSuccess(LoginResult result)
    {
        loginCanvas.gameObject.SetActive(false);
        mainMenuCanvas.gameObject.SetActive(true);
    }

    private void OnLoginFailure(PlayFabError error)
    {
        failCanvas.gameObject.SetActive(true);
        isfade = true;
        
    }
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

    void OnRegisterSuccess(RegisterPlayFabUserResult result){
        signCanvas.gameObject.SetActive(false);
        loginCanvas.gameObject.SetActive(true);
    }
    void OnRegisterFailure(PlayFabError error){
        failCanvas.gameObject.SetActive(true);
        isfade = true;
    }
    
}
