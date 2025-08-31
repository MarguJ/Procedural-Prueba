using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Object = System.Object;
using Random = UnityEngine.Random;

namespace ScriptsExampleOfDungeons
{
    public class  EntranceScript : MonoBehaviour //para el posta cambiarle el nombre a SpawnScript
    {
        public GameObject doorGameObject;
        public GameObject childExit;
        public ExitScript exitScript;
        public GameObject childHall;
        private int maxOfRoomSpawn = 8;
        private int minOfRoomSpawn = 5;
        private int quantityOfRooms;
        private int roomOrHallway;
        
        
        public void SecondStart(int doorNumber)
        {
            doorGameObject = GameObject.Find("Door" + doorNumber);
            DoorScript doorScript = doorGameObject.GetComponentInChildren<DoorScript>();
            if (doorScript.isActive)
            {
                childHall = GameObject.Find("Entrance");
                if (childHall == null)
                {
                    Debug.LogError("No Entrance found");
                }
                exitScript = childHall.GetComponentInChildren<ExitScript>();
                Vector3 pos = doorGameObject.transform.position;
                Quaternion rot = doorGameObject.transform.rotation;
                Instantiate(childHall, pos, rot);
            }
        }

        public void SpawnHallwayAndRooms()
        {
            foreach (var exit in ExitScript.allExits.Values)
            {
                exitScript.SetExitNumber();
                GameObject childHall2 = GameObject.Find("Entrance");
                Vector3 pos = exit.transform.position;
                Quaternion rot = exit.transform.rotation;
                Instantiate(childHall2, pos, rot);
            }
        }
    }
}
