using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;
using Photon.Realtime;
using Sirenix.OdinInspector;

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
    Animator anim;

    public Bullets bullet;
    PlayerStatus playerStatus;
    bool isShootWeapon = false;
    
    

    private void Start() {
        anim = GetComponentInChildren<Animator>();
        playerStatus =  GetComponent<PlayerStatus>();
        if(pv.IsMine){
            rig = GetComponent<Rigidbody>();
            RandomPosition();
            
        }        
    }

    private void LateUpdate() {
        Move(); 
    }

    [Button]
    void RandomPosition(){
        if(pv.IsMine){
            mapSizeX = GameManager.instance.mapSizeX;
            mapSizeZ = GameManager.instance.mapSizeZ;
            mapTileScale = GameManager.instance.mapTileScale;

            float positionX = Random.Range(mapTileScale + 3, mapSizeX - mapTileScale - 3);
            float positionZ = Random.Range(mapTileScale + 3, mapSizeZ - mapTileScale - 3);
            Vector3 randomPosition = new Vector3(positionX, 1.5f, positionZ);
            transform.position = randomPosition;
        }
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
    }

    public void OnMoveInput(InputAction.CallbackContext context){
        if(context.phase == InputActionPhase.Performed){
            anim.SetBool("isMove", true);
            moveVec = context.ReadValue<Vector2>();
        }
        else if(context.phase == InputActionPhase.Canceled){
            anim.SetBool("isMove", false);
            moveVec = Vector2.zero;
        }
    }

    public void OnAttackInput(InputAction.CallbackContext context){
        if(pv.IsMine){
            if(context.phase == InputActionPhase.Performed){
                anim.SetBool("isShoot",true);
                InvokeRepeating("ShootBullet", 0.1f, 0.1f); 
            } 
            else if(context.phase == InputActionPhase.Canceled){
                anim.SetBool("isShoot",false);
                CancelInvoke("ShootBullet");
            }
        }
    }

    public void ShootBullet(){
        if(playerStatus.mana.curValue >= bullet.manaDecoy){
            PhotonNetwork.Instantiate("Bullet" + bullet.bulletID, shootPosition.position , shootPosition.rotation);
            playerStatus.mana.Subtract(bullet.manaDecoy);
        }
        
        Debug.Log("Bullet" + bullet.bulletID);
    }
}
