using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Bullets : MonoBehaviourPunCallbacks
{
    public int bulletID;
    public PhotonView pv;
    public float speed;
    public int Damage = 50;
    public int manaDecoy;
    Rigidbody rig;
    public float limitTime = 3;
    private void Start() {
        rig = GetComponent<Rigidbody>();
        rig.AddForce(transform.forward * speed, ForceMode.Impulse);
        Invoke("selfDestroy", limitTime);
    }

    void selfDestory(){
        pv.RPC("DestroyRPC", RpcTarget.AllBuffered);  
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
