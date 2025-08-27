using System.Collections.Generic;
using UnityEngine;
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
            
            if (allDoors.Count >= maxDoors)
            {
                for (int i = 1; i <= maxDoors; i++)
                {
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
