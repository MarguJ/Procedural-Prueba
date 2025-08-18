using UnityEngine;

[System.Serializable]
public class Entry1Script : MonoBehaviour
{
    public string doorID;
    public bool isActive;
    public float activationChance = 0.5f; // 50% chance by default
    public Entry1Script connectedDoor;
    public Transform connectionPoint;
    public GameObject activeVisual;
    public GameObject inactiveVisual;

    void Start()
    {
        // Generate unique ID if not set
        if (string.IsNullOrEmpty(doorID))
        {
            doorID = System.Guid.NewGuid().ToString();
        }
        
        // Randomize activation
        RandomizeActivation();
        
        // Update visuals
        UpdateVisuals();
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

    public void ConnectToDoor(Entry1Script otherDoor)
    {
        connectedDoor = otherDoor;
        otherDoor.connectedDoor = this;
        
        // Both doors should have same activation state for connection
        bool shouldBeActive = isActive && otherDoor.isActive;
        SetActivation(shouldBeActive);
        otherDoor.SetActivation(shouldBeActive);
    }

    void UpdateVisuals()
    {
        if (activeVisual != null)
            activeVisual.SetActive(isActive);
        
        if (inactiveVisual != null)
            inactiveVisual.SetActive(!isActive);
    }

    // For debugging in inspector
    void OnDrawGizmos()
    {
        Gizmos.color = isActive ? Color.green : Color.red;
        Gizmos.DrawWireCube(transform.position, Vector3.one);
        
        if (connectedDoor != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, connectedDoor.transform.position);
        }
    }
}