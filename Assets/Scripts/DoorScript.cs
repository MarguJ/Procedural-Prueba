using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public string doorID;
    public bool isActive;
    public float activationChance;
    public GameObject activeVisual;
    public GameObject inactiveVisual;
    public EntranceScript entrance;

    void Start()
    {
        entrance = FindAnyObjectByType<EntranceScript>();
        // Generate unique ID if not set
        if (string.IsNullOrEmpty(doorID))
        {
            doorID = System.Guid.NewGuid().ToString();
        }
        
        // Randomize activation
        RandomizeActivation();
        
        // Update visuals
        UpdateVisuals();

        entrance.SecondStart();
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