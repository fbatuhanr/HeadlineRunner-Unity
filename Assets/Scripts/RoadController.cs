using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadController : MonoBehaviour
{
    
}



    //     [SerializeField] [TextArea(3, 15)] private string words;
    //     private string[] allWords;
    //     
    //     [SerializeField] private GameObject straightRoad, missingRoad, bothMissingRoad;
    //     [SerializeField] private GameObject doors;

    //
    // public void SpawnRoad()
    // {
    //         float positionZoffset = 18.06017f;
    //         float rotationY;
    //
    //         GameObject selectedRoad;
    //         int randomRoad = Random.Range(0, 3);
    //         switch (randomRoad)
    //         {
    //             case 0:
    //                 selectedRoad = straightRoad;
    //                 rotationY = 0;
    //                 break;
    //             case 1:
    //                 selectedRoad = missingRoad;
    //                 rotationY = Random.Range(0, 2) == 0 ? 180f : 0;
    //                 break;
    //             case 2:
    //                 selectedRoad = bothMissingRoad;
    //                 rotationY = Random.Range(0, 2) == 0 ? 180f : 0;
    //                 break;
    //             
    //             default: 
    //                 selectedRoad = straightRoad;
    //                 rotationY = 0;
    //                 break;
    //         }
    //
    //         GameObject spawnedRoad = Instantiate(
    //             selectedRoad, 
    //             new Vector3(
    //                 selectedRoad.transform.position.x, 
    //                 0, 
    //                 transform.GetChild(transform.childCount-1).position.z + positionZoffset), 
    //             Quaternion.Euler(0, rotationY, 0));
    //         
    //         spawnedRoad.transform.parent = transform;
    //
    //         
    //         // This Spawned Road will contain Door?
    //         if (Random.Range(0, 3) == 0)
    //         {
    //             SpawnDoor(randomRoad);
    //         }
    // }
    //
    // public void SpawnDoor(int whichRoadSpawned)
    // {
    //     /*
    //      * whichRoadSpawned=0 ise StraightRoad spawnlanır.
    //      * Door position z: [-8.5f ile +8.5f] arası olabilir
    //      */
    //     
    //     /*
    //      * whichRoadSpawned=1 ise MissingRoad spawnlanır.
    //      * Door position z: [-8.5f ile -3.33f] ve [+3.33f ve +8.5f] arası olabilir.
    //      */
    //     
    //     /*
    //      * whichRoadSpawned=2 ise BothMissingRoad spawnlanır
    //      * rotation y=0 ise, door position z: [-8.5f ile -1.5f] ve [+4.5f ile +8.5f] arası olabilir.
    //      * rotation y=180 ise, door rotation y=180 olmalı, door position z: [+8.5f ile +4.5f] ve [-1.5f ile -8.5f] olabilir
    //      */
    //
    //     Vector3 doorPosition;
    //     float doorPositionZ;
    //     switch (whichRoadSpawned)
    //     {
    //         case 0:
    //             doorPositionZ = Random.Range(-8.4f, 8.5f);
    //             doorPosition = new Vector3(0, 0, doorPositionZ);
    //             break;
    //         
    //         case 1:
    //             doorPositionZ = Random.Range(0, 2) == 0 ? Random.Range(-8.4f, -3.33f) : Random.Range(+3.33f, +8.5f);
    //             doorPosition = new Vector3(0, 0, doorPositionZ);
    //             break;
    //         
    //         case 2:
    //             doorPositionZ = Random.Range(0, 2) == 0 ? Random.Range(-8.4f, -1.5f) : Random.Range(+4.5f, +8.5f);
    //             doorPosition = new Vector3(0, 0, doorPositionZ);
    //             break;
    //         
    //         default:
    //             doorPositionZ = Random.Range(-8.4f, 8.5f);
    //             doorPosition = new Vector3(0, 0, doorPositionZ);
    //             break;
    //     }
    //     
    //     GameObject spawnedDoor = Instantiate(
    //         doors, 
    //         new Vector3(0,0,0),
    //         Quaternion.Euler(0, 0, 0));
    //
    //     spawnedDoor.transform.parent = transform.GetChild(transform.childCount - 1);
    //     
    //     spawnedDoor.transform.localPosition = doorPosition;
    //     spawnedDoor.transform.localRotation = spawnedDoor.transform.parent.rotation;
    //
    //
    //     string leftDoorText = allWords[Random.Range(0, allWords.Length)];
    //     string rightDoorText = allWords[Random.Range(0, allWords.Length)];
    //     spawnedDoor.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMesh>().text = leftDoorText;
    //     spawnedDoor.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMesh>().text = rightDoorText;
    // }
    //