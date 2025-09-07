using Unity.VisualScripting;
using UnityEngine;

namespace ScriptsExampleOfDungeons
{
    public class StructureScript : MonoBehaviour
    {
        public EntranceScript entrance;

        void Awake()
        {
            entrance = FindAnyObjectByType<EntranceScript>();
        }

        void Start()
        {
            gameObject.tag = "DungeonPiece";
        }

        private void OnTriggerStay(Collider other)
        {
            Debug.Log("Hay Trigger");
            if (entrance.rooms == entrance.quantityOfRooms)
            {
                if (other.CompareTag("DungeonPiece"))
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}