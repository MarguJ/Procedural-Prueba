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
        public Transform childEntra;
        public GameObject childExit;
        public ExitScript exitScript;
        public GameObject childHall;
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
                childHall = GameObject.Find("Entrance");
                if (childHall == null)
                {
                    Debug.LogError("No Entrance found");
                }
                Vector3 pos = doorGameObject.transform.position;
                Quaternion rot = doorGameObject.transform.rotation;
                Instantiate(childHall, pos, rot);
                //Desde aca nada es seguro
                Debug.Log(doorNumber);
                childEntra = childHall.transform.Find("Exit"+doorNumber);
                if (childEntra == null)
                {
                    Debug.LogError("No Exit found");
                }
                exitScript = childEntra.GetComponent<ExitScript>();
                if (exitScript == null)
                {
                    Debug.Log("No script found");
                }
                exitScript.SetExitName(doorNumber);
            }
        }

        public void SpawnHallwayAndRooms(int exitNumber)
        {
            Debug.Log("Spawning" + exitNumber);
            GameObject childHall2 = GameObject.Find("Entrance");
            childExit = GameObject.Find("Exit"+exitNumber);
            Vector3 pos = childExit.transform.position;
            Quaternion rot = childExit.transform.rotation;
            Instantiate(childHall2, pos, rot);
        }
    }
}
