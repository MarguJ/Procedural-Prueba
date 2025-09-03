using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace ScriptsExampleOfDungeons
{
    public class  EntranceScript : MonoBehaviour //para el posta cambiarle el nombre a SpawnScript
    {
        public GameObject doorGameObject;
        public GameObject uniqueHall;
        public ExitScript exitScript; 
        public GameObject entrance;
        public GameObject intersection;
        public GameObject room;
        private readonly int _maxOfRoomSpawn = 9;
        private readonly int _minOfRoomSpawn = 5;
        private int _quantityOfRooms;
        private int _roomHallwayIntersection;
        private float _roomChance;
        private float _intersectionChance;
        
        public void SecondStart(int doorNumber)
        {
            doorGameObject = GameObject.Find("Door" + doorNumber);
            DoorScript doorScript = doorGameObject.GetComponentInChildren<DoorScript>();
            if (doorScript.isActive)
            {
                Vector3 pos = doorGameObject.transform.position;
                Quaternion rot = doorGameObject.transform.rotation;
                uniqueHall = Instantiate(entrance, pos, rot);
                exitScript = uniqueHall.GetComponentInChildren<ExitScript>();
                ToSetExitNumber();
            }
        }

        public void SpawnHallwayAndRooms()
        {
            foreach (var exit in ExitScript.allExits.Values)
            {
                Vector3 pos = exit.transform.position;
                Quaternion rot = exit.transform.rotation;
                InstantiateFunction(entrance, pos, rot);
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
            _quantityOfRooms = Random.Range(_minOfRoomSpawn, _maxOfRoomSpawn);
            Debug.Log(_quantityOfRooms);
            while (rooms < _quantityOfRooms)
            {
                //registrar a todos los hallways ya creados
                ToSetExitNumber();
                //por cada exit (ForEach)
                foreach (var exit in ExitScript.allExits.Values)
                {
                    _roomHallwayIntersection = Random.Range(1,8);
                    if (_roomHallwayIntersection == 1 || _roomHallwayIntersection == 2 || _roomHallwayIntersection == 3 || _roomHallwayIntersection == 4) //Hallway
                    {
                        //Debug.Log("Hallway Generated");
                        Vector3 pos = exit.transform.position;
                        Quaternion rot = exit.transform.rotation;
                        InstantiateFunction(entrance, pos, rot);
                    }
                    else if (_roomHallwayIntersection == 5 || _roomHallwayIntersection == 6) //Intersection
                    {
                        //Debug.Log("Intersection Generated");
                        Vector3 pos = exit.transform.position;
                        Quaternion rot = exit.transform.rotation;
                        InstantiateFunction(intersection, pos, rot);
                    }
                    else if (_roomHallwayIntersection == 7) //Room
                    {
                        //Debug.Log("Room Generated");
                        Vector3 pos = exit.transform.position;
                        Quaternion rot = exit.transform.rotation;
                        InstantiateFunction(room, pos, rot);
                        rooms++;
                    }
                    //Que haya una posibilidad de que un room o un hallway o una intersection aparezca, si aparece un room sumar +1 a rooms
                }
            }
        }

        public void InstantiateFunction(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            Instantiate(prefab,position,rotation);
        }
    }
}
