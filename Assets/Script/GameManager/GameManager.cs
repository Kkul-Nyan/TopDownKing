using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int mapSizeX;
    public int mapSizeZ;
    public int mapTileScale;
    public int charactorSelectNumber = 0;
    public int userCoin;
    public float soundSize;
    
    public string userName;
    public bool isMute = false;

    //게임매니저는 유저정보등을 가지고 있기 떄문에 만들어준 싱글톤입니다. 
    public static GameManager instance;
    private void Awake() {
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(instance);
            return;
        }
        Destroy(this.gameObject);
    }

    // 사운드 크기를 조정합니다.
    public void SoundChange(){
        AudioListener.volume = soundSize;
    }

    //사운드를 중지합니다.
    public void MuteSound(){
        AudioListener.pause = isMute;
    }
}
