using UnityEngine;

namespace ScriptsExampleOfDungeons
{
    public class StructureScript : MonoBehaviour
    {
        public float spawnTime;

        void Awake()
        {
            spawnTime = Time.time;
            Debug.Log(spawnTime);
        }

        void OnTriggerEnter(Collider other)
        {
            StructureScript otherStruct = other.GetComponent<StructureScript>();
            if (otherStruct != null)
            {
                if (spawnTime > otherStruct.spawnTime)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}