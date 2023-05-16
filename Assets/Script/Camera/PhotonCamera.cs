using UnityEngine;
using Photon.Pun;

public class PhotonCamera : MonoBehaviourPunCallbacks
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] PhotonView pv;

    private void Start() {
        pv = GameObject.FindWithTag("Player").GetComponent<PhotonView>();
        if(pv.IsMine){
            target = GameObject.FindWithTag("Player").GetComponent<Transform>();
        }
    }
    private void LateUpdate()
    {
        transform.position = target.position + offset;
        transform.eulerAngles = new Vector3(60, 0, 0);
        
    }
}
