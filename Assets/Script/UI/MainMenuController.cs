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
    public Canvas shopCanvas;
    public Canvas successCoinCanvas;
    public Image charactorImage;
    public Sprite[] charactorSprites;
    public int charactorSelectNumber;
    public PlayerCharactor game;


    //메뉴 캔버스 꺼두고, 게임 캔버스를 켭니다. 
    public void OnLogInWithEmail(){
        IDLoginCanvas.gameObject.SetActive(true);
        loginCanvas.gameObject.SetActive(false);
    }
    
    //메뉴 캔버스 꺼두고, 옵션 캔버스를 켭니다. 
    public void OnOptionButton(){
        
    }

    //게임스타트버튼을 눌렸을때, 게임선택캔버스를 켜줍니다.
    public void OnStartButton(){
        startGameCanvas.gameObject.SetActive(true);
        lobbieCanvas.gameObject.SetActive(false);
    }
    
    //캐릭터 선택 캔버스를 켜줍니다.
    public void OnCharactorButton(){
        charctorCanvas.gameObject.SetActive(true);
        lobbieCanvas.gameObject.SetActive(false);
        charactorSelectNumber = GameManager.instance.charactorSelectNumber;
    }
    // 캐릭터 선택을 취소하고 로비캔버스를 켜줍니다.
    public void OnCancerCharactorButton(){
        charctorCanvas.gameObject.SetActive(false);
        lobbieCanvas.gameObject.SetActive(true);
    }

    //1번게임 배틀로얄모드게임씬으로 로드합니다.
    public void OnBattleRoyaleMode(){
        SceneManager.LoadScene(1);
    }
    //게임선택캔버스를 취소하고, 로비캔버스를 켜줍니다.
    public void OnCancerStart(){
        startGameCanvas.gameObject.SetActive(false);
        lobbieCanvas.gameObject.SetActive(true);
    }


    //2번게임 씬 로드합니다.
    public void OnGame2Button(){
        SceneManager.LoadScene(2);
    }

    //캐릭터선택창에서 오른쪽이동버튼입니다.
    public void OnRightChageButton(){
        if(charactorSelectNumber == charactorSprites.Length - 1){
            return;
        }

        charactorSelectNumber += 1;
        ChangeImage();
    }
    //캐릭터선택창에서 왼쪽이동버튼입니다.
    public void OnLeftChangeButton(){
        if(charactorSelectNumber == 0){
            return;
        }

        charactorSelectNumber -= 1;
        ChangeImage();
    }

    //선택한 캐릭터를 게임매니저에 정보를 넘겨주고, 캐릭터선택캔버스을 꺼주고, 로비캔버스를 열어줍니다.
    public void OnSelectCharactorButton(){
        GameManager.instance.charactorSelectNumber = charactorSelectNumber;
        charctorCanvas.gameObject.SetActive(false);
        lobbieCanvas.gameObject.SetActive(true);
        game.ChooseCharactor();

    }
    //캐릭터선택캔버스에서 이미지스프라이트를 변경해줍니다.
    void ChangeImage() => charactorImage.sprite = charactorSprites[charactorSelectNumber];

    public void OnShopButton(){
        shopCanvas.gameObject.SetActive(true);
        lobbieCanvas.gameObject.SetActive(false);
    }

    public void OnCancerShopButton(){
        shopCanvas.gameObject.SetActive(false);
        lobbieCanvas.gameObject.SetActive(true);
    }
    public void OnBuyCheckButton(){
        successCoinCanvas.gameObject.SetActive(false);
    }
}
