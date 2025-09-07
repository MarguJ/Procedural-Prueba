using UnityEngine;

namespace ScriptsExampleOfDungeons
{
    public class StructureScript : MonoBehaviour
    {
        public float spawnTime;

        void Start()
        {
            spawnTime = Time.time;
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