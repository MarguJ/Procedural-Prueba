using UnityEngine;
using System.Collections.Generic;

public class ExitScript : MonoBehaviour
{
    public static Dictionary<string, ExitScript> allExits = new();
    public static string exitID;
    
    public void SetExitNumber()
    {
        exitID = System.Guid.NewGuid().ToString();
        if (!allExits.ContainsKey(exitID))
        {
            allExits.Add(exitID, this);
            
        }
    }
}
