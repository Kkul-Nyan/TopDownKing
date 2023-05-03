using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{

    public GameObject mapTile;
    public GameObject[] objects;
    public int objectScale;
    public int mapSizeX = 10;
    public int mapSizeZ = 10;
    public int positionY;
    public int maxObjects;
    public int minObjects;
    
    public bool isOverlap = false;

    void Start()
    {
        GenerateMap();
        GenerateEdge();
        ObjectGenerator();
    }

    void GenerateMap()
    {
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int z = 0; z < mapSizeZ; z++)
            {
                Vector3Int tilePosition = new Vector3Int(x * objectScale, positionY, z * objectScale);
                Instantiate(mapTile, tilePosition, Quaternion.identity);
            }
        }
    }

    void GenerateEdge(){
        for(int x = 0; x < mapSizeX; x++){
            Vector3 edgePosition = new Vector3(x * objectScale, Mathf.Abs(positionY * objectScale) - 1f, (mapSizeZ - 1) * objectScale);
            Instantiate(mapTile, edgePosition, Quaternion.identity);

            edgePosition = new Vector3(x * objectScale, Mathf.Abs(positionY * objectScale)-1, 0);
            Instantiate(mapTile, edgePosition, Quaternion.identity);
        }
        
        for(int z = 1; z < mapSizeZ-1; z ++){
            Vector3 edgePosition = new Vector3((mapSizeX - 1) * objectScale, Mathf.Abs(positionY * objectScale) - 1f, z * objectScale);
           
            Instantiate(mapTile, edgePosition, Quaternion.identity);

            edgePosition = new Vector3(0, Mathf.Abs(positionY * objectScale) - 1f, z * objectScale);
            Instantiate(mapTile, edgePosition, Quaternion.identity);
        }
    }

    void ObjectGenerator(){
        int totalObject = Random.Range(minObjects, maxObjects + 1);
        Vector3[] bannedPosition = new Vector3[totalObject];
        for(int i = 0; i < totalObject; i++){
            
            int selectRandomObject = Random.Range(0, objects.Length);
            int randomPositinX = Random.Range(objectScale, (mapSizeX - 1) * objectScale); 
            int randomPositinZ = Random.Range(objectScale, (mapSizeZ - 1) * objectScale); 
            Vector3 randomPosition = new Vector3(randomPositinX, 1, randomPositinZ);

            foreach(Vector3 pos in bannedPosition){
                if(pos == randomPosition){
                    isOverlap = true;
                }
            }

            if(!isOverlap){
                Instantiate(objects[selectRandomObject], randomPosition, Quaternion.identity);
            }
        
            bannedPosition[i] = randomPosition;
            isOverlap = false;
        }

    }

}
