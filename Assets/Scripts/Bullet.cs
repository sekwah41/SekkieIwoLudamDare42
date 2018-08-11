using UnityEngine;

namespace Game
{
    public class Bullet : MonoBehaviour
    {
        Vector3 Velocity { get; set; }
        float Lifetime { get; set; }

        void Start()
        {
            Lifetime = 2;
        }

        void Update()
        {
            Vector3 newPosition = transform.position;
            newPosition += Velocity * Time.deltaTime;

            Lifetime -= Time.deltaTime;
            if (Lifetime < 0)
            {
                Destroy(gameObject);
            }



            transform.position = newPosition;
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

            float xPosition = transform.position.x;
            float zPosition = transform.position.z;
            short tileX = (short) Mathf.FloorToInt(xPosition);
            short tileZ = (short) Mathf.FloorToInt(zPosition);

            TileMap tileMap = GameManager.Instance.TileMap;

            if (tileMap.HasBlock(tileX, tileZ))
            {
                if (xPosition % 1 < 0.5F)
                    tileX--;
                else
                    tileX++;

                if (zPosition % 1 < 0.5F)
                    tileZ--;
                else
                    tileZ++;
            }
            else
            {
                if (!tileMap.HasBlock((short)(tileX - 1), tileZ) &&
                    !tileMap.HasBlock((short)(tileX + 1), tileZ) &&
                    !tileMap.HasBlock(tileX, (short)(tileZ - 1)) &&
                    !tileMap.HasBlock(tileX, (short)(tileZ + 1)))
                {
                    float x = xPosition % 1 - 0.5F;
                    float z = zPosition % 1 - 0.5F;
                    if (Mathf.Abs(x) > Mathf.Abs(z))
                    {
                        if (x > 0)
                            tileX++;
                        else
                            tileX--;
                    }
                    else
                    {
                        if (z > 0)
                            tileZ++;
                        else
                            tileZ--;
                    }
                }
            }

            tileMap.SetBlock(new Block(tileX, tileZ, Color.Colors[0]));
        }
    }
}