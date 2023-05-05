using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapObject : MonoBehaviour, IDamagable
{
    public int maxHealth;
    public int curhealth;
    Renderer renderer;
    Color startColor;
    public Color destroyColor;

    private void Start() {
        renderer = GetComponent<Renderer>();
        startColor = renderer.material.color; 
    }

    private void Update() {
        CheckDestroy();
        HitColor(); 
    }

    public void TakePhysicalDamage(int amount){
        curhealth -= amount;
    }

    void Die(){
        Destroy(transform.gameObject);
    }

    void HitColor(){
        float  t = (float)(maxHealth - curhealth) / (float)maxHealth;
        renderer.material.color = Color.Lerp(startColor, destroyColor, t);
    }

    void CheckDestroy(){
        if( curhealth <= 0){
            Destroy(transform.gameObject);
        }
    }

}
