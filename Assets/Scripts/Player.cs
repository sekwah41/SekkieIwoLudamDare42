using UnityEngine;

namespace Game
{
    public class Player : MonoBehaviour
    {

        CharacterController controller;

        Vector3 direction = new Vector3(0,0,0);

        public float playerSpeed = 3;

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
            float x = Input.GetAxisRaw("Horizontal");

            Vector3 velocity = new Vector3(x, 0, z);

            if(x*x + z * z < 1)
            {
                direction = velocity.normalized * playerSpeed;
            }
            else
            {
                direction = velocity * playerSpeed;
            }

            controller.Move(direction * Time.deltaTime);
            this.transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
        }
    }
}