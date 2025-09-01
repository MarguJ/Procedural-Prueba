using UnityEngine;
using System.Collections.Generic;

public class ExitScript : MonoBehaviour
{
    public static Dictionary<string, ExitScript> allExits = new();
    
    public void SetExitNumber(int exitNumber)
    {
<<<<<<< Updated upstream
<<<<<<< Updated upstream
        gameObject.tag = "Untagged";
        Debug.Log(exitNumber);
        exitNumber += 1;
        gameObject.name = "Exit" + exitNumber;
        if (!allExits.ContainsKey(gameObject.name))
        {
            allExits.Add(gameObject.name, this);
=======
        exitID = System.Guid.NewGuid().ToString();
        if (!allExits.ContainsKey(exitID))
        {
            allExits.Add(exitID, this);
>>>>>>> Stashed changes
=======
        exitID = System.Guid.NewGuid().ToString();
        if (!allExits.ContainsKey(exitID))
        {
            allExits.Add(exitID, this);
>>>>>>> Stashed changes
        }
    }
}
