﻿using UnityEngine;

namespace Game
{
    public class CameraRig : MonoBehaviour
    {
        public Player player;

        bool useMouse = true;

        new Camera camera;

        Plane groundPlane = new Plane(new Vector3(0, 1, 0), new Vector3(0, 0, 0));

        // Use this for initialization
        void Start()
        {
            camera = GetComponentInChildren<Camera>();
        }

        void Update()
        {
            float vertX = Input.GetAxisRaw("HorizontalAim");
            float vertY = Input.GetAxisRaw("VerticalAim");
            if (!useMouse && Input.GetAxisRaw("Mouse X") != 0 && Input.GetAxisRaw("Mouse Y") != 0)
            {
                useMouse = true;
                Debug.Log("Swapping to Mouse");
            }
            else if(useMouse && vertY != 0 && vertX != 0)
            {
                useMouse = false;
                Debug.Log("Swapping to Controller");
            }

            if (useMouse)
            {
                Ray ray = camera.ScreenPointToRay(Input.mousePosition);
                float enter;

                if (groundPlane.Raycast(ray, out enter))
                {
                    Vector3 hitPoint = ray.GetPoint(enter);
                    player.AimTowards(hitPoint);
                }
            }
            else if(vertX * vertX + vertY * vertY > 0.03f)
            {
                player.AimTowards(new Vector3(vertX, 0, vertY) + player.transform.position);
            }
        }

        // Update is called once per frame
        void LateUpdate()
        {
            Vector3 wantedLoc = player.transform.position - this.transform.position;

            this.transform.position += wantedLoc;

            Vector2 looking;

            float z = Input.GetAxisRaw("VerticalAim");
            float x = Input.GetAxisRaw("HorizontalAim");

            if (useMouse)
            {
                looking = new Vector2(-Screen.width * 0.5f, -Screen.width * 0.5f);
            }
            float posX = Screen.width * 0.5f;
            float posY = Screen.width * 0.5f;


        }
    }
}