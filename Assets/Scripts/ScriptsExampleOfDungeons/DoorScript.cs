using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;
using Random = UnityEngine.Random;

namespace ScriptsExampleOfDungeons
{
    public class DoorScript : MonoBehaviour
    {
        public static Dictionary<string, DoorScript> allDoors = new();
        public string doorID;
        private const int maxDoors = 8;
        public bool isActive;
        public float activationChance;
        public GameObject activeVisual;
        public GameObject inactiveVisual;
        public EntranceScript entrance;
        private List<GameObject> doors = new List<GameObject>();

        void Awake()
        {
            entrance = FindAnyObjectByType<EntranceScript>();

            if (string.IsNullOrEmpty(doorID))
                doorID = System.Guid.NewGuid().ToString();

            if (!allDoors.ContainsKey(doorID))
            {
                allDoors.Add(doorID, this);
            }

            RandomizeActivation();

            UpdateVisuals();

            
            GameObject door = gameObject;
            doors.Add(door);
            
            if (allDoors.Count >= maxDoors)
            {
                for (int i = 0; i < maxDoors; i++)
                {
                    Debug.Log(i);
                    foreach (var dor in doors)
                    {
                        dor.name = ("Door" + i);
                    } //esto no va a acá
                    entrance.SecondStart(i);
                }
            }
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
}
