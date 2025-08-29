using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ScriptsExampleOfDungeons
{
    public class  EntranceScript : MonoBehaviour //para el posta cambiarle el nombre a SpawnScript
    {
        public GameObject doorGameObject;
        public GameObject hallWayExit;
        private int maxOfRoomSpawn = 8;
        private int minOfRoomSpawn = 5;
        private int _quantityOfRooms;
        private int _roomOrHallway;
        
        public void SecondStart(int doorNumber)
        {
            doorGameObject = GameObject.Find("Door" + doorNumber);
            DoorScript doorScript = doorGameObject.GetComponentInChildren<DoorScript>();
            if (doorScript.isActive)
            {
                GameObject childHall = GameObject.Find("Entrance");
                Vector3 pos = doorGameObject.transform.position;
                Quaternion rot = doorGameObject.transform.rotation;
                Instantiate(childHall, pos, rot);
            }
        }

        public void SpawnHallwayAndRooms(int exitNumber)
        {
            hallWayExit = GameObject.Find("Exit"+exitNumber);
            GameObject childHall2 = GameObject.Find("Entrance");
            Vector3 pos = hallWayExit.transform.position;
            Quaternion rot = hallWayExit.transform.rotation;
            Instantiate(childHall2, pos, rot);
        }
    }
}
