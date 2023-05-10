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

    public static GameManager instance;
    private void Awake() {
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(instance);
            return;
        }
        Destroy(this.gameObject);
    }

    public void SoundChange(){
        AudioListener.volume = soundSize;
    }

    public void MuteSound(){
        AudioListener.pause = isMute;
    }
}
