using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    public float speed;
    public int Damage = 50;
    Rigidbody rig;
    float limitTime = 1;
    private void Start() {
        rig = GetComponent<Rigidbody>();
        rig.AddForce(transform.forward * speed, ForceMode.Impulse);
    }
    private void Update() {
        selfDestory();
    }

    void selfDestory(){
        if(limitTime > 0 ){
            limitTime -= Time.deltaTime;
        }
        else{
            Destroy(transform.gameObject);
        }
    }
    
    private void OnCollisionEnter(Collision other) {
        var ob = other.transform.gameObject.GetComponent<IDamagable>();
        if(  ob != null)
            ob.TakePhysicalDamage(Damage);

        Destroy(transform.gameObject);
    }
}
