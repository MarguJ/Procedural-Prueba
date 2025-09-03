using System;
using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class ExitScript : MonoBehaviour
{
    public static Dictionary<string, ExitScript> allExits = new();
    public string exitID;
    public bool isActive;
    public float activationChance;

    public void Awake()
    {
        isActive = Random.Range(0f, 1f) <= activationChance;
    }
    public void SetExitNumber()
    {
        if (isActive)
        {
            if (!allExits.ContainsKey(exitID))
            {
                exitID = System.Guid.NewGuid().ToString();
                allExits.Add(exitID, this);
            }
        }
    }

    private void OnDestroy()
    {
        allExits.Remove(exitID);
    }
}
