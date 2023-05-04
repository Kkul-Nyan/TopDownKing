using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    int mapSizeX;
    int mapSizeZ;
    float mapTileScale;
    public float moveSpeed;
    public float rotationSpeed;
    Rigidbody rig;
    Vector2 moveVec;
    public Transform shootPosition;


    public GameObject[] bullets;

    private void Start() {
        rig = GetComponent<Rigidbody>();
        Invoke("RandomPosition",0.1f);
        
        
    }

    private void LateUpdate() {
        Move();
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
        Vector3 dir = Vector3.forward * moveVec.y + Vector3.right * moveVec.x;
        dir *= moveSpeed;
        dir.y = rig.velocity.y;
        rig.velocity = dir;

        if (moveVec != Vector2.zero)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotationSpeed);
            }
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
        if(context.phase == InputActionPhase.Performed){
            InvokeRepeating("ShootBullet", 0.1f, 0.1f); 
        } 
        else if(context.phase == InputActionPhase.Canceled){
            CancelInvoke("ShootBullet");
        }
    }

    public void OnbuttonDown(){
        InvokeRepeating("ShootBullet", 0.1f, 0.1f);
    }

    public void OnbuttonUp(){
        CancelInvoke("ShootBullet");
    }
    public void ShootBullet(){
        GameObject bullet = Instantiate(bullets[0], shootPosition.position , shootPosition.rotation);
    }

   
}
