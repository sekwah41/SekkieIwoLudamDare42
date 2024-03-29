﻿using System;
using UnityEngine;

namespace Game
{
    public class Player : MonoBehaviour
    {
        public float playerSpeed = 3;
        public float rateOfFire = 20;
        public float shotReload = 0;

        bool hasFired = false;

        CharacterController controller;
        AudioSource audioSource;
        Vector3 direction = new Vector3(0, 0, 0);
        float yOrientation = 0F;

        // Use this for initialization
        void Start()
        {
            controller = GetComponent<CharacterController>();
            audioSource = GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update()
        {
            Movement();
            this.transform.rotation = Quaternion.Euler(0, yOrientation, 0);

            if (hasFired && Input.GetAxis("Trigger") != 1)
            {
                hasFired = false;
            }

            if (Input.GetMouseButtonDown(0) || (!hasFired && Input.GetAxis("Trigger") == 1))
            {
                hasFired = true;
                shotReload = rateOfFire;
                Shoot();
            }
        }

        /**
         * Handles the axis updates, duuuh
         */
        void Movement()
        {
            float z = Input.GetAxisRaw("Vertical");
            float x = Input.GetAxisRaw("Horizontal");

            Vector3 velocity = new Vector3(x, 0, z);

            if (x * x + z * z > 1)
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

        public void AimTowards(Vector3 point)
        {
            Vector3 diff = point - transform.position;
            yOrientation = Mathf.Atan2(diff.x, diff.z) / Mathf.PI * 180.0F;
        }

        public void Shoot()
        {
            if (GameManager.Instance.CurrentScreen != ScreenType.GAME)
                return;

            Vector3 direction = new Vector3(Mathf.Sin(yOrientation * Mathf.PI / 180.0F), 0, Mathf.Cos(yOrientation * Mathf.PI / 180.0F));
            GameObject bulletObject = Instantiate(GameManager.Instance.bulletPrefab);
            bulletObject.transform.SetPositionAndRotation(transform.position, Quaternion.identity);
            bulletObject.GetComponent<Bullet>().SetVelocity(direction.normalized * GameManager.Instance.bulletSpeed);

            audioSource.Play();
        }
    }
}