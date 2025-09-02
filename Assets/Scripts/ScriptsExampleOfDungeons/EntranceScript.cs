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
        public GameObject uniqueHall;
        public ExitScript exitScript;
        public GameObject childHall;
        private int maxOfRoomSpawn = 8;
        private int minOfRoomSpawn = 5;
        private int quantityOfRooms;
        private int roomHallwayIntersection;
        private float roomChance;
        private float intersectionChance;
        
        
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
                uniqueHall = Instantiate(childHall, pos, rot);
                exitScript = uniqueHall.GetComponentInChildren<ExitScript>();
                ToSetExitNumber();
            }
        }

        public void SpawnHallwayAndRooms()
        {
            foreach (var exit in ExitScript.allExits.Values)
            {
                GameObject childHall2 = GameObject.Find("Entrance");
                Vector3 pos = exit.transform.position;
                Quaternion rot = exit.transform.rotation;
                InstantiateFunction(childHall2, pos, rot);
            }
            SpawnHallWaysUntilRooms();
        }
        public void ToSetExitNumber()
        {
            exitScript.SetExitNumber();
        }

        public void SpawnHallWaysUntilRooms()
        {
            int rooms = 1;
            quantityOfRooms = Random.Range(minOfRoomSpawn, maxOfRoomSpawn);
            Debug.Log(quantityOfRooms);
            while (rooms < quantityOfRooms)
            {
                //registrar a todos los hallways ya creados
                ToSetExitNumber();
                //por cada hallway (For)
                //Que haya una posibilidad de que un room o un hallway aparezca si aparece un room sumar +1 a rooms
            }
        }

        public void InstantiateFunction(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            Instantiate(prefab,position,rotation);
        }
    }
}
