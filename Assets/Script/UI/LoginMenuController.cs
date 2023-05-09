using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public enum LoadingStatus{
    Main,
    SinginEmail,
    LoginEmail,
}
public class LoginMenuController : MonoBehaviour
{

    public Canvas loginCanvas;
    public Canvas emailLoginCanvas;
    public Canvas singinEmailCanvas;
    
    public LoadingStatus status;
   
    private void Start() {
        status = LoadingStatus.Main;
        IsStatus();
    }

    private void Update() {
        //IsStatus();
    }
    public void IsStatus(){
        Reset();
        switch (status){
            case LoadingStatus.Main : 
                loginCanvas.gameObject.SetActive(true);
            break;
            case LoadingStatus.LoginEmail : 
                emailLoginCanvas.gameObject.SetActive(true);
            break;
            case LoadingStatus.SinginEmail :
                singinEmailCanvas.gameObject.SetActive(true); 
            break;
            
        }
    }

    void Reset(){
        loginCanvas.gameObject.SetActive(false);
        emailLoginCanvas.gameObject.SetActive(false);
        singinEmailCanvas.gameObject.SetActive(false);
    }

    public void OnButtonControll(int statusInt){
        int statusNum = statusInt;
        switch(statusNum){
            case 0 :
                status = LoadingStatus.Main;
                
            break;
            case 1 :
                status = LoadingStatus.LoginEmail;
            break;
            case 2 :
                status = LoadingStatus.SinginEmail;
            break;
        }
        IsStatus();
    }
}
