using UnityEngine;
using System.Collections.Generic;

public class ExitScript : MonoBehaviour
{
    public static Dictionary<string, ExitScript> allExits = new();
    public string exitID;
    
    public void SetExitNumber()
    {
        if (string.IsNullOrEmpty(exitID))
            exitID = System.Guid.NewGuid().ToString();
        //Debug.Log(exitNumber);
        if (!allExits.ContainsKey(exitID))
        {
            allExits.Add(gameObject.name, this);
            Debug.Log(allExits[gameObject.name].exitID);
        }
    }
}
