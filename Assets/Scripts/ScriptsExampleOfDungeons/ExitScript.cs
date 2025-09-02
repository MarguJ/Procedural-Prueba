using UnityEngine;
using System.Collections.Generic;

public class ExitScript : MonoBehaviour
{
    public static Dictionary<string, ExitScript> allExits = new();
    public string exitID;
    
    public void SetExitNumber()
    {
        if (!allExits.ContainsKey(exitID))
        {
            exitID = System.Guid.NewGuid().ToString();
            allExits.Add(exitID, this);
        }
    }
}
