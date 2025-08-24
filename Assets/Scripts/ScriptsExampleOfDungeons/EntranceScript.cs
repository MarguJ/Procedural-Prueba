using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class EntranceScript : MonoBehaviour
{
    public GameObject hallwayP;
    public DoorScript rDoorScript;
    
    public void SecondStart()
    {
        rDoorScript = FindAnyObjectByType<DoorScript>(); //Busca cualquiera (No ideal)
        DoorScript doorScript = rDoorScript.allDoors[rDoorScript.doorID];
        if (doorScript.isActive)
        {
            Debug.Log(doorScript.doorID);
            GameObject child = hallwayP.transform.Find("Entrance").gameObject;
            Vector3 pos = doorScript.transform.position;
            pos.x -= (child.transform.localPosition.x)*3;
            Instantiate(hallwayP, pos, Quaternion.identity);
        }
    }
}
