using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public Canvas loginCanvas;
    public Canvas IDLoginCanvas;
    public Canvas signCanvas;
    public Canvas startGameCanvas;
    public Canvas lobbieCanvas;
    public Canvas charctorCanvas;

    public Image charactorImage;
    public Sprite[] charactorSprites;
    int charactorSelectNumber;


    //메뉴 캔버스 꺼두고, 게임 캔버스를 켭니다. 
    public void OnLogInWithEmail(){
        IDLoginCanvas.gameObject.SetActive(true);
        loginCanvas.gameObject.SetActive(false);
    }
    
    //메뉴 캔버스 꺼두고, 옵션 캔버스를 켭니다. 
    public void OnOptionButton(){
        
    }

    public void OnLogInWithGoogle(){
        
    }
    public void OnCancerIDLogin(){
        IDLoginCanvas.gameObject.SetActive(false);
        loginCanvas.gameObject.SetActive(true);
    }
    public void OnRegisterButton(){
        signCanvas.gameObject.SetActive(true);
        loginCanvas.gameObject.SetActive(false);
    }
    public void OnRegisterCancerButton(){
        signCanvas.gameObject.SetActive(false);
        loginCanvas.gameObject.SetActive(true);
    }

    public void OnStartButton(){
        startGameCanvas.gameObject.SetActive(true);
        lobbieCanvas.gameObject.SetActive(false);
    }
    
    public void OnCharactorButton(){

    }

    public void OnCancerCharactorButton(){

    }

    //1번게임 씬 로드합니다.
    public void OnBattleRoyaleMode(){
        SceneManager.LoadScene(1);
    }
    public void OnCancerStart(){
        startGameCanvas.gameObject.SetActive(false);
        lobbieCanvas.gameObject.SetActive(true);
    }


    //2번게임 씬 로드합니다.
    public void OnGame2Button(){
        SceneManager.LoadScene(2);
    }

    public void OnRightChageButton(){
        if(charactorSelectNumber == charactorSprites.Length){
            return;
        }

        charactorSelectNumber += 1;
        ChangeImage();
    }

    public void OnLeftChangeButton(){
        if(charactorSelectNumber == 0){
            return;
        }

        charactorSelectNumber -= 1;
        ChangeImage();
    }

    public void SelectCharactorButton(){

    }

    void ChangeImage() => charactorImage.sprite = charactorSprites[charactorSelectNumber];


}
