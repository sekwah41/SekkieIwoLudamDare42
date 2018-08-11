using UnityEngine;

namespace Game
{
    public class Player : MonoBehaviour
    {

        CharacterController controller;

        // Use this for initialization
        void Start()
        {
            controller = GetComponent<CharacterController>();
        }

        // Update is called once per frame
        void Update()
        {
            Movement();
        }

        /**
         * Handles the axis updates, duuuh
         */
        void Movement()
        {
            float z = Input.GetAxisRaw("Vertical");
            float x = Input.GetAxisRaw("Hotizontal");

            Vector3 velocity = new Vector3(x, 0, z);


        }
    }
}