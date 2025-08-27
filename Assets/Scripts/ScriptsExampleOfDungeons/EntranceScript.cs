using Unity.VisualScripting;
using UnityEngine;

namespace ScriptsExampleOfDungeons
{
    public class EntranceScript : MonoBehaviour //para el posta cambiarle el nombre a SpawnScript
    {
        public GameObject hallwayP;
        public GameObject door;
    
        public void SecondStart(int doorNumber)
        {
            door = GameObject.Find("Door" + doorNumber);
            DoorScript fDoorScript = door.GetComponentInChildren<DoorScript>();
            DoorScript doorScript = DoorScript.allDoors[fDoorScript.doorID];
            if (doorScript.isActive)
            {
                Debug.Log(doorScript.doorID);
                GameObject child = hallwayP.transform.Find("Entrance").gameObject;
                Vector3 pos = doorScript.transform.position;
                Quaternion rot = doorScript.transform.rotation;
                Instantiate(hallwayP, pos, rot);
            }
        }
    }
}
