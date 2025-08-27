using UnityEngine;

namespace ScriptsExampleOfDungeons
{
    public class EntranceScript : MonoBehaviour //para el posta cambiarle el nombre a SpawnScript
    {
        public GameObject doorGameObject;

        public void SecondStart(int doorNumber)
        {
            doorGameObject = GameObject.Find("Door" + doorNumber);
            DoorScript doorScript = doorGameObject.GetComponentInChildren<DoorScript>();
            if (doorScript.isActive)
            {
                // Debug.Log(doorScript.doorID);
                GameObject childGO = GameObject.Find("Entrance");
                Vector3 pos = doorGameObject.transform.position;
                Quaternion rot = doorGameObject.transform.rotation;
                Instantiate(childGO, pos, rot);
            }
        }
    }
}
