using UnityEngine;

namespace Game
{
    public class Bullet : MonoBehaviour
    {
        Vector3 Velocity { get; set; }

        void Update()
        {
            Vector3 position = this.transform.position;
            position += Velocity * Time.deltaTime;
            transform.position = position;
        }

        public void SetVelocity(Vector3 velocity)
        {
            Velocity = velocity;
            float angle = Mathf.Atan2(Velocity.x, Velocity.z);
            transform.rotation = Quaternion.Euler(0, angle / Mathf.PI * 180.0f, 0);
        }

        void OnCollisionEnter(Collision collision)
        {
            Destroy(gameObject);
        }
    }
}