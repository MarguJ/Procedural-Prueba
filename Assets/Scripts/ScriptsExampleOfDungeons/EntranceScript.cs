using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class EntranceScript : MonoBehaviour
{
    public string targetDoorID;
    public GameObject hallwayP;
    public DoorScript rDoorScript;
    
    public void SecondStart()
    {
        rDoorScript = GetComponent<DoorScript>();
        rDoorScript.GetAllIDs();
        
        
        if (!DoorScript.AllDoors.ContainsKey(targetDoorID))
        {
            Debug.LogError("No door found with ID: " + targetDoorID);
            return;
        }

        DoorScript doorScript = DoorScript.AllDoors[targetDoorID];
        if (doorScript.isActive == false)
        {
            Move(doorScript);
        }
    }
    
    void Move(DoorScript targetDoor)
    {
        GameObject child = hallwayP.transform.Find("Entrance").gameObject;
        Vector3 pos = targetDoor.transform.position;
        pos.x -= (child.transform.localPosition.x)*3;
        Instantiate(hallwayP, pos, Quaternion.identity);
    }
}
