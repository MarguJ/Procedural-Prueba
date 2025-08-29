using UnityEngine;
using System.Collections.Generic;

public class ExitScript : MonoBehaviour
{
    public static Dictionary<string, ExitScript> allExits = new();
    public string nameExit;

    public void SetExitName(int exitName)
    {
        nameExit = gameObject.name;
        nameExit = "Exit" + exitName;
        if (!allExits.ContainsKey(nameExit))
        {
            allExits.Add(nameExit, this);
            Debug.Log(nameExit);
        }
    }
}
