using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public enum UIStatus{
    Main,
    StartGame,
    CharactorSelect,
    Shop,
    Option
}

public class MainMenuController : MonoBehaviour
{
    [Header("Canvas")]
    public Canvas mainCanvas;
    public Canvas startGameCanvas;
    public Canvas charctorCanvas;
    public Canvas shopCanvas;
    public Canvas optionCanvas;
    public Canvas successCoinCanvas;

    [Header("Details")]
    public TextMeshProUGUI coinText;
    public Image charactorImage;
    public Sprite[] charactorSprites;
    public Sprite[] soundSprites;
    public int charactorSelectNumber;
    public PlayerCharactor game;
    public UIStatus status;
    public TextMeshProUGUI playerNameText;
    public Image soundImage;
    public Slider soundSilder;
    public TextMeshProUGUI soundText;
    public float soundSize = 1;
    public NetworkManager networkManager;



    private void Start() {
        Invoke("CheckBasic",1f);
    }
    public void IsStatus(){
        Reset();
        switch (status){
            case UIStatus.Main : 
                mainCanvas.gameObject.SetActive(true);
            break;
            case UIStatus.StartGame : 
                startGameCanvas.gameObject.SetActive(true);
            break;
            case UIStatus.CharactorSelect :
                charctorCanvas.gameObject.SetActive(true);
                charactorSelectNumber = GameManager.instance.charactorSelectNumber;
            break;
            case UIStatus.Shop :
                shopCanvas.gameObject.SetActive(true);
            break;
            case UIStatus.Option :
                optionCanvas.gameObject.SetActive(true);
            break;
        }
    }

    void Reset(){
        mainCanvas.gameObject.SetActive(false);
        startGameCanvas.gameObject.SetActive(false);
        charctorCanvas.gameObject.SetActive(false);
        shopCanvas.gameObject.SetActive(false);
        optionCanvas.gameObject.SetActive(false);

    }

    public void OnButtonControll(int statusInt){
        int statusNum = statusInt;
        switch(statusNum){
            case 0 :
                status = UIStatus.Main;
            break;
            case 1 :
                status = UIStatus.StartGame;
            break;
            case 2 :
                status = UIStatus.CharactorSelect;
            break;
            case 3 :
                status = UIStatus.Shop;
            break;
            case 4 :
                status = UIStatus.Option;
            break;
        }
        IsStatus();
    }
    
    void CheckBasic(){
        playerNameText.text = GameManager.instance.userName;
        coinText.text = GameManager.instance.userCoin.ToString();
    }
      
    //1번게임 배틀로얄모드게임씬으로 로드합니다.
    public void OnBattleRoyaleMode(){
        networkManager.JoinorCreateRoom();
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
    //캐릭터선택캔버스에서 이미지스프라이트를 변경해줍니다.
    void ChangeImage() => charactorImage.sprite = charactorSprites[charactorSelectNumber];

    //선택한 캐릭터를 게임매니저에 정보를 넘겨주고, 캐릭터선택캔버스을 꺼주고, 로비캔버스를 열어줍니다.
    public void OnSelectCharactorButton(){
        GameManager.instance.charactorSelectNumber = charactorSelectNumber;
        game.ChooseCharactor();
        OnButtonControll(0);
    }

    public void OnSuccessCoinButton(){
        successCoinCanvas.gameObject.SetActive(false);
        CheckBasic();
    }

    public void SoundValueCheck(){
        soundSize = soundSilder.value;
        soundText.text = (soundSize * 100).ToString();
        GameManager.instance.soundSize = soundSize;
        if(soundSize == 0){
            GameManager.instance.isMute = true;
            soundImage.sprite = soundSprites[1];
        }
        else if(soundSize > 0){
            GameManager.instance.isMute = false;
            soundImage.sprite = soundSprites[0];
        }
    }
    
    public void OnMuteButton(){
        if(GameManager.instance.isMute == false){
            GameManager.instance.isMute = true;
            soundImage.sprite = soundSprites[1];
        }
        else if(GameManager.instance.isMute == true){
            GameManager.instance.isMute = false;
            soundImage.sprite = soundSprites[0];
        }
        GameManager.instance.MuteSound();
    }
}
