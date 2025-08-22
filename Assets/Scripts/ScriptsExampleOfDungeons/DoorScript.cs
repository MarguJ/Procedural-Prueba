using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class DoorScript : MonoBehaviour
{
    public static readonly Dictionary<string, DoorScript> AllDoors = new Dictionary<string, DoorScript>();
    
    public static string[] allTargets;
    public string doorID;
    public bool isActive;
    public float activationChance;
    public GameObject activeVisual;
    public GameObject inactiveVisual;
    public EntranceScript entrance;

    void Awake()
    {
        entrance = FindAnyObjectByType<EntranceScript>();
        
        if (string.IsNullOrEmpty(doorID))
            doorID = System.Guid.NewGuid().ToString();
        
        if (!AllDoors.ContainsKey(doorID))
            AllDoors.Add(doorID, this);
        
        RandomizeActivation();
        
        UpdateVisuals();

        entrance.SecondStart();
    }

    void OnDestroy()
    {
        if (AllDoors.ContainsKey(doorID))
            AllDoors.Remove(doorID);
    }

    public void RandomizeActivation()
    {
        isActive = Random.Range(0f, 1f) <= activationChance;
    }

    public void SetActivation(bool active)
    {
        isActive = active;
        UpdateVisuals();
    }

    void UpdateVisuals()
    {
        if (activeVisual != null)
            activeVisual.SetActive(isActive);

        if (inactiveVisual != null)
            inactiveVisual.SetActive(!isActive);
    }

    public void GetAllIDs()
    {
        var quantTargets = AllDoors.Count;
        for (int i = 0; i < quantTargets; i++)
        { 
            allTargets.AddRange(doorID);
            Debug.Log(allTargets[i]);
        }
    }
}