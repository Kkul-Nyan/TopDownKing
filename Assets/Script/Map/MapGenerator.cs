using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class MapGenerator : MonoBehaviourPunCallbacks
{
    public int objectScale;
    public int mapSizeX = 10;
    public int mapSizeZ = 10;
    public int positionY;
    public int maxObjects;
    public int minObjects;
    
    public bool isOverlap = false;
    public PhotonView pv;
    
    void Start()
    {
        GameManager.instance.mapTileScale = objectScale;
        GameManager.instance.mapSizeX = mapSizeX * objectScale;
        GameManager.instance.mapSizeZ = mapSizeZ * objectScale;
        if(PhotonNetwork.IsMasterClient){
            GenerateMap();
            GenerateEdge();
            ObjectGenerator();
        }
        CreatePlayer();
    }

    //맵타일을 제작하는 부분입니다.
    void GenerateMap()
    {
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int z = 0; z < mapSizeZ; z++)
            {
                Vector3 tilePosition = new Vector3(x * objectScale, positionY, z * objectScale);
                PhotonNetwork.Instantiate("Maptile", tilePosition, Quaternion.identity);
            }
        }
    }

    //맵타일 주변 테두리 오브젝트를 만들어줍니다.
    void GenerateEdge(){
        for(int x = 0; x < mapSizeX; x++){
            Vector3 edgePosition = new Vector3(x * objectScale, Mathf.Abs(positionY * objectScale) - 1f, (mapSizeZ - 1) * objectScale);
            PhotonNetwork.Instantiate("Maptile", edgePosition, Quaternion.identity);

            edgePosition = new Vector3(x * objectScale, Mathf.Abs(positionY * objectScale)-1, 0);
            PhotonNetwork.Instantiate("Maptile", edgePosition, Quaternion.identity);
        }
        
        for(int z = 1; z < mapSizeZ-1; z ++){
            Vector3 edgePosition = new Vector3((mapSizeX - 1) * objectScale, Mathf.Abs(positionY * objectScale) - 1f, z * objectScale);
           
            PhotonNetwork.Instantiate("Maptile", edgePosition, Quaternion.identity);

            edgePosition = new Vector3(0, Mathf.Abs(positionY * objectScale) - 1f, z * objectScale);
            PhotonNetwork.Instantiate("Maptile", edgePosition, Quaternion.identity);
        }
    }

    //맵내부 오브젝트를 제작하는 부분입니다.
    void ObjectGenerator(){
        int totalObject = Random.Range(minObjects, maxObjects + 1);
        Vector3[] bannedPosition = new Vector3[totalObject];
        for(int i = 0; i < totalObject; i++){
            
            int selectRandomObject = Random.Range(0, 6);
            int randomPositinX = Random.Range(objectScale, (mapSizeX - 2) * objectScale); 
            int randomPositinZ = Random.Range(objectScale, (mapSizeZ - 2) * objectScale);
            Vector3 randomPosition = new Vector3(0,0,0);

            randomPosition = new Vector3(randomPositinX, Mathf.Abs(positionY * objectScale) - 1f, randomPositinZ);

            foreach(Vector3 pos in bannedPosition){
                if(pos == randomPosition){
                    ObjectGenerator();
                    return;
                }
            }
            
            if(selectRandomObject == 0){
                PhotonNetwork.Instantiate("Stone1", randomPosition, Quaternion.identity);
            }
            else if(selectRandomObject == 1){
                PhotonNetwork.Instantiate("Stone", randomPosition, Quaternion.identity);
            }
            else if(selectRandomObject > 2){
                PhotonNetwork.Instantiate("Tree", randomPosition, Quaternion.identity);
            }

            bannedPosition[i] = randomPosition;
            isOverlap = false;
        }
    }
    void CreatePlayer(){
        PhotonNetwork.Instantiate("Player", transform.position, Quaternion.identity);
    }

}
