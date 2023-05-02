using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public Canvas mainMenuCanvas;
    public Canvas gameMenuCanvas;
    public Canvas loginCanvas;
    public Canvas signCanvas;

    //메뉴 캔버스 꺼두고, 게임 캔버스를 켭니다. 
    public void OnStartButton(){
        mainMenuCanvas.gameObject.SetActive(false);
        gameMenuCanvas.gameObject.SetActive(true);
    }
    
    //메뉴 캔버스 꺼두고, 옵션 캔버스를 켭니다. 
    public void OnOptionButton(){
        
    }

    public void OnRegisterButton(){
        signCanvas.gameObject.SetActive(true);
        loginCanvas.gameObject.SetActive(false);
    }
    public void OnRegisterCancerButton(){
        signCanvas.gameObject.SetActive(false);
        loginCanvas.gameObject.SetActive(true);
    }
    //1번게임 씬 로드합니다.
    public void OnGame1Button(){
        SceneManager.LoadScene(1);
    }

    //2번게임 씬 로드합니다.
    public void OnGame2Button(){
        SceneManager.LoadScene(2);
    }

    //게임 캔버스 꺼두고, 메인 캔버스를 켭니다. 
    public void OnCancerButton(){
        mainMenuCanvas.gameObject.SetActive(true);
        gameMenuCanvas.gameObject.SetActive(false);
    }

    //프로그램을 종료합니다
    public void OnExitButton(){
        Application.Quit();
    }
}
