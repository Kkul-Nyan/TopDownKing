using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryFloor : MonoBehaviour
{
    Renderer renderer;
    bool isPlayer = false;
    public float maxTime;
    float itime;
    public float destoryTime;
    bool isDestory = false;

    Color startColor;
    public Color destoryColor;

    private void Start() {
        renderer = GetComponent<Renderer>();
        startColor = renderer.material.color;
    }
    private void Update() {
        ColorChangeFloor();
        RemoveFloor();
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Player")){
            isPlayer = true;
        }
    }

    private void OnCollisionExit(Collision other) {
        if(other.gameObject.CompareTag("Player")){
            isPlayer = false;
        }
    }

    void ColorChangeFloor(){
        if(isPlayer){
            if (itime < maxTime)
            {
                itime += Time.deltaTime;
                float t = itime / maxTime;
                renderer.material.color = Color.Lerp(startColor, destoryColor, t);
            }
            else
            {
                renderer.material.color = destoryColor;
                transform.gameObject.AddComponent<Rigidbody>();
                isDestory = true;
            }
        }
    }

    void RemoveFloor(){
        if(isDestory){
            if(destoryTime > 0){
                destoryTime -= Time.deltaTime;
            }
            else{
                Destroy(transform.gameObject);
            }
        }
    }
}
