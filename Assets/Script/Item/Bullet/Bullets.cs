using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Bullets : MonoBehaviourPunCallbacks
{
    public PhotonView pv;
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
            pv.RPC("DestroyRPC", RpcTarget.AllBuffered);
        }
    }
    
    private void OnCollisionEnter(Collision other) {
        var ob = other.transform.gameObject.GetComponent<IDamagable>();
        if(  ob != null)
            ob.TakePhysicalDamage(Damage);

        pv.RPC("DestroyRPC", RpcTarget.AllBuffered);
    }

    [PunRPC]
    void DestroyRPC() => Destroy(transform.gameObject);
}
