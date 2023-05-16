using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;
using Photon.Realtime;

public class PlayerController : MonoBehaviourPunCallbacks
{
    int mapSizeX;
    int mapSizeZ;
    float mapTileScale;
    public float moveSpeed;
    public float rotationSpeed;
    Rigidbody rig;
    Vector2 moveVec;
    public Transform shootPosition;
    public PhotonView pv;
    Vector3 curPos;

    private void Start() {
        if(pv.IsMine){
            rig = GetComponent<Rigidbody>();
            RandomPosition();
            StartCoroutine("RandomPlayerPosition");
            StopCoroutine("RandomPlayerPosition");
        }        
    }

    private void LateUpdate() {
        Move(); 
    }

    IEnumerator RandomPlayerPosition(){
        yield return new WaitForSeconds(3f);
        RandomPosition();
    }
    
    void RandomPosition(){
        mapSizeX = GameManager.instance.mapSizeX;
        mapSizeZ = GameManager.instance.mapSizeZ;
        mapTileScale = GameManager.instance.mapTileScale;

        float positionX = Random.Range(mapTileScale + 3, mapSizeX - mapTileScale - 3);
        float positionZ = Random.Range(mapTileScale + 3, mapSizeZ - mapTileScale - 3);
        Vector3 randomPosition = new Vector3(positionX, 1.5f, positionZ);
        transform.position = randomPosition;
    }

    void Move(){
        if(pv.IsMine){
            Vector3 dir = Vector3.forward * moveVec.y + Vector3.right * moveVec.x;
            dir *= moveSpeed;
            dir.y = rig.velocity.y;
            rig.velocity = dir;

            if (moveVec != Vector2.zero)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotationSpeed);
            }
        }
       // else transform.position = Vector3.Lerp(transform.position, curPos, Time.deltaTime * 10);
    }

    public void OnMoveInput(InputAction.CallbackContext context){
        if(context.phase == InputActionPhase.Performed){
            moveVec = context.ReadValue<Vector2>();
        }
        else if(context.phase == InputActionPhase.Canceled){
            moveVec = Vector2.zero;
        }
    }

    public void OnAttackInput(InputAction.CallbackContext context){
        if(pv.IsMine){
            if(context.phase == InputActionPhase.Performed){
                InvokeRepeating("ShootBullet", 0.1f, 0.1f); 
            } 
            else if(context.phase == InputActionPhase.Canceled){
                CancelInvoke("ShootBullet");
            }
        }
    }

    public void OnbuttonDown(){
        InvokeRepeating("ShootBullet", 0.1f, 0.1f);
    }

    public void OnbuttonUp(){
        CancelInvoke("ShootBullet");
    }
    public void ShootBullet(){
        GameObject bullet = PhotonNetwork.Instantiate("Bullet", shootPosition.position , shootPosition.rotation);
    }
    /*
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){
        if(stream.IsWriting){
            stream.SendNext(transform.position);
        }
        else{
            curPos = (Vector3)stream.ReceiveNext();
        }
    }
   */
}
