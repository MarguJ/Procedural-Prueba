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
<<<<<<< Updated upstream
<<<<<<< Updated upstream
                Instantiate(childHall, pos, rot);
                exitScript.SetExitNumber(doorNumber);
=======
                GameObject uniqueHall = Instantiate(childHall, pos, rot);
                exitScript = uniqueHall.GetComponentInChildren<ExitScript>();
                ToSetExitNumber();
>>>>>>> Stashed changes
=======
                GameObject uniqueHall = Instantiate(childHall, pos, rot);
                exitScript = uniqueHall.GetComponentInChildren<ExitScript>();
                ToSetExitNumber();
>>>>>>> Stashed changes
            }
        }

        public void SpawnHallwayAndRooms(int exitNumber)
        {
<<<<<<< Updated upstream
            //Debug.Log("Spawning" + exitNumber);
            GameObject childHall2 = GameObject.Find("Entrance");
            childExit = GameObject.Find("Exit"+exitNumber);
            Vector3 pos = childExit.transform.position;
            Quaternion rot = childExit.transform.rotation;
            Instantiate(childHall2, pos, rot);
=======
            foreach (var exit in ExitScript.allExits.Values)
            {
                GameObject childHall2 = GameObject.Find("Entrance");
                Vector3 pos = exit.transform.position;
                Debug.Log(exit.transform.position);
                Quaternion rot = exit.transform.rotation;
                Debug.Log(exit.transform.rotation);
                Instantiate(childHall2, pos, rot);
            }
>>>>>>> Stashed changes
        }

        public void ToSetExitNumber()
        {
            exitScript.SetExitNumber();
        }

        public void ToSetExitNumber()
        {
            exitScript.SetExitNumber();
        }
    }
}
