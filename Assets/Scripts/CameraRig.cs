using UnityEngine;

namespace Game
{
    public class CameraRig : MonoBehaviour
    {

        public Player player;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void LateUpdate()
        {
            Vector3 position = player.transform.position;
            this.transform.position = position;
        }
    }
}