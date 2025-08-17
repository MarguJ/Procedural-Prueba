using UnityEngine;

public class EntranceScript : MonoBehaviour
{
    public DoorScript doorScript;
    public GameObject doorToConnect;
    public GameObject entrance;
    
    public void SecondStart()
    {
        doorScript = FindAnyObjectByType<DoorScript>();
        bool check = doorScript.isActive;
        if (check == false)
        {
            doorToConnect = GameObject.Find("Door1");
        }
        Move();
    }

    void Move()
    {
            entrance.transform.position = doorToConnect.transform.position;
    }
    
}
