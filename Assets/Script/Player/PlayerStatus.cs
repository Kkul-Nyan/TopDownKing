using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    public Status health;
    public Status mana;
}

[System.Serializable]
public class Status
{
    public float curValue;
    public float maxValue;
    public float startValue;
    public float regenRate;
    public float decayRate;
    public Image uiBar;

    public void Add(float amount){
        curValue = Mathf.Min(curValue + amount, maxValue);
    }

    public void Subtract(float amount){
        curValue = Mathf.Max(curValue - amount, 0);
    }

    public float GetPercent(){
        return curValue / maxValue;
    }
}