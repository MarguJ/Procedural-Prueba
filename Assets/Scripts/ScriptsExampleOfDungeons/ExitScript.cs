using UnityEngine;
using System.Collections.Generic;

public class ExitScript : MonoBehaviour
{
    public static Dictionary<string, ExitScript> allExits = new();
    
    public void SetExitName()
    {
        for (int i = 0; i < 6; i++)
        {
            gameObject.name = "Exit" + i;
            if (!allExits.ContainsKey(name))
            {
                allExits.Add(name, this);
            }
        }
    }
}
