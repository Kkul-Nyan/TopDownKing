using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour, IDamagable
{
    public Status health;
    public Status mana;

    private void Update() {
        statusUpdate();
    }

    void statusUpdate(){
        CheckisPlayerDead();

        mana.Add(mana.regenRate * Time.deltaTime);

        health.uiBar.fillAmount = health.GetPercent();
        mana.uiBar.fillAmount = mana.GetPercent();
    }

    public void CheckisPlayerDead(){
        if(health.curValue == 0f){
            Die();
        }
    }
    public void TakePhysicalDamage(int amount){
        health.Subtract(amount);
        //onTakeDamage?.Invoke();
    }

    void Die(){
        Debug.Log( GameManager.instance.userName + "Die");
    }

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

public interface IDamagable 
{
    void TakePhysicalDamage(int damageAmount);
}