using UnityEngine;

namespace Game
{
    public class CameraRig : MonoBehaviour
    {
        public Player player;

        new Camera camera;
        Plane groundPlane = new Plane(new Vector3(0, 1, 0), new Vector3(0, 0, 0));

        // Use this for initialization
        void Start()
        {
            camera = GetComponentInChildren<Camera>();
        }

        void Update()
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            float enter;

            if (groundPlane.Raycast(ray, out enter))
            {
                Vector3 hitPoint = ray.GetPoint(enter);
                player.AimTowards(hitPoint);
            }
        }

        // Update is called once per frame
        void LateUpdate()
        {
            Vector3 position = player.transform.position;
            this.transform.position = position;
        }
    }
}