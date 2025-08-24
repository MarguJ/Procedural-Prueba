using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class DoorScript : MonoBehaviour
{
    public Dictionary<string, DoorScript> allDoors = new();

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

        if (!allDoors.ContainsKey(doorID))
            allDoors.Add(doorID, this);

        RandomizeActivation();

        UpdateVisuals();

        entrance.SecondStart();
    }

    void OnDestroy()
    {
        if (allDoors.ContainsKey(doorID))
            allDoors.Remove(doorID);
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
}