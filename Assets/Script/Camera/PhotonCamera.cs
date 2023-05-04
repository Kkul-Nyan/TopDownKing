using UnityEngine;
using Photon.Pun;

public class PhotonCamera : MonoBehaviourPunCallbacks
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;

    private void LateUpdate()
    {
        transform.position = target.position + offset;
        transform.eulerAngles = new Vector3(60, 0, 0);
        
    }
}
