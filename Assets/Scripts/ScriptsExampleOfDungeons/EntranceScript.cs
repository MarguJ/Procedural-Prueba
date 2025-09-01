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
                Vector3 pos = doorGameObject.transform.position;
                Quaternion rot = doorGameObject.transform.rotation;

                GameObject uniqueHall = Instantiate(childHall, pos, rot);
                exitScript = uniqueHall.GetComponentInChildren<ExitScript>();
                ToSetExitNumber();
            }
        }

        public void SpawnHallwayAndRooms(int exitNumber)
        {
            foreach (var exit in ExitScript.allExits.Values)
            {
                GameObject childHall2 = GameObject.Find("Entrance");
                Vector3 pos = exit.transform.position;
                Debug.Log(exit.transform.position);
                Quaternion rot = exit.transform.rotation;
                Debug.Log(exit.transform.rotation);
                Instantiate(childHall2, pos, rot);
            }
        }
        public void ToSetExitNumber()
        {
            exitScript.SetExitNumber();
        }
    }
}
