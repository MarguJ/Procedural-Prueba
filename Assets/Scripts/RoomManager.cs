using UnityEngine;
using System.Collections.Generic;

public class RoomManager : MonoBehaviour
{
    [Header("Room Configuration")]
    public List<Entry1Script> doorEntrances = new List<Entry1Script>();
    public string roomID;
    
    void Start()
    {
        // Generate room ID
        if (string.IsNullOrEmpty(roomID))
        {
            roomID = "Room_" + System.Guid.NewGuid().ToString().Substring(0, 8);
        }
        
        // Find all door entrances in this room
        FindDoorEntrances();
    }
    
    void FindDoorEntrances()
    {
        Entry1Script[] doors = GetComponentsInChildren<Entry1Script>();
        doorEntrances.AddRange(doors);
    }
    
    public void ConnectToRoom(RoomManager otherRoom)
    {
        // Find available doors in both rooms
        var availableDoors = doorEntrances.FindAll(door => door.isActive && door.connectedDoor == null);
        var otherAvailableDoors = otherRoom.doorEntrances.FindAll(door => door.isActive && door.connectedDoor == null);
        
        if (availableDoors.Count > 0 && otherAvailableDoors.Count > 0)
        {
            // Connect random doors
            var door1 = availableDoors[Random.Range(0, availableDoors.Count)];
            var door2 = otherAvailableDoors[Random.Range(0, otherAvailableDoors.Count)];
            
            door1.ConnectToDoor(door2);
        }
    }
}