using UnityEngine;

namespace Game
{
    public class Bullet : MonoBehaviour
    {
        public ColorType? ColorType { get; set; }

        new MeshRenderer renderer;
        TrailRenderer trailRenderer;

        Vector3 Velocity { get; set; }
        Vector3 LastPosition { get; set; }
        float Lifetime { get; set; }
        int EnemiesPassed { get; set; }

        const int MaxEnemyPasses = 3;

        void Awake()
        {
            ColorType = null;
            Lifetime = 2;
            EnemiesPassed = 0;

            renderer = GetComponent<MeshRenderer>();
            trailRenderer = GetComponent<TrailRenderer>();
        }

        void Start()
        {
            LastPosition = transform.position;
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

            short tileX = (short) Mathf.FloorToInt(newPosition.x);
            short tileZ = (short) Mathf.FloorToInt(newPosition.z);

            TileMap tileMap = GameManager.Instance.TileMap;

            if (tileMap.HasBlock(tileX, tileZ) || GameManager.Instance.IsOutOfBounds(tileX, tileZ))
            {
                if (ColorType != null)
                {
                    TryPlaceAfterHitting(tileX, tileZ);
                }
                else
                {
                    TryTriggerChainReaction(tileX, tileZ);
                }
                Destroy(gameObject);
            }

            Ray ray = new Ray(LastPosition, newPosition - LastPosition);

            RaycastHit[] hits = Physics.RaycastAll(ray, (newPosition - LastPosition).magnitude);

            foreach (RaycastHit hit in hits) {
                EnemyBehaviour enemyBehaviour = hit.transform.gameObject.GetComponent<EnemyBehaviour>();
                if (enemyBehaviour != null)
                {
                    if (EnemiesPassed < MaxEnemyPasses)
                    {
                        if (ColorType == null)
                        {
                            Destroy(enemyBehaviour.gameObject);
                            SetColor(enemyBehaviour.ColorType);
                            EnemiesPassed++;
                        }
                        else if (enemyBehaviour.ColorType == ColorType)
                        {
                            GameManager.Instance.AwardPoints(1);
                            Destroy(enemyBehaviour.gameObject);
                            EnemiesPassed++;
                        }
                    }
                }
            }
            
            transform.position = newPosition;
            LastPosition = transform.position;
        }

        public void SetVelocity(Vector3 velocity)
        {
            Velocity = velocity;
            float angle = Mathf.Atan2(Velocity.x, Velocity.z);
            transform.rotation = Quaternion.Euler(0, angle / Mathf.PI * 180.0f, 0);
        }

        public void SetColor(ColorType colorType)
        {
            ColorType = colorType;
            Color color = ColorUtils.GetColor(ColorType ?? Game.ColorType.RED);
            renderer.material.color = color;
            trailRenderer.startColor = color;
            trailRenderer.endColor = color;
        }

        void TryPlaceAfterHitting(short hitX, short hitZ)
        {
            TileMap tileMap = GameManager.Instance.TileMap;

            bool canPlaceX = !tileMap.HasBlock((short)(Velocity.x < 0 ? hitX + 1 : hitX - 1), hitZ);
            bool canPlaceZ = !tileMap.HasBlock(hitX, (short)(Velocity.z < 0 ? hitZ + 1 : hitZ - 1));

            if (!canPlaceX && !canPlaceZ)
            {
                // Place diagonal
                if (Velocity.x < 0)
                    hitX++;
                else
                    hitX--;

                if (Velocity.z < 0)
                    hitZ++;
                else
                    hitZ--;
            }
            else
            {
                if ((Mathf.Abs(Velocity.x) > Mathf.Abs(Velocity.z) && canPlaceX) || !canPlaceZ)
                {
                    if (Velocity.x < 0)
                        hitX++;
                    else
                        hitX--;
                }
                else
                {
                    if (Velocity.z < 0)
                        hitZ++;
                    else
                        hitZ--;
                }
            }

            if (ColorType != null && !GameManager.Instance.IsOutOfBounds(hitX, hitZ))
            {
                GameManager.Instance.PlayCrystaliseSound();
                tileMap.SetBlock(hitX, hitZ, ColorType ?? Game.ColorType.RED);
            }
        }

        void TryTriggerChainReaction(short hitX, short hitZ)
        {
            TileMap tileMap = GameManager.Instance.TileMap;
            Block block = tileMap.GetBlock(hitX, hitZ);

            if (block != null)
            {
                block.StartChainReaction();
            }
        }
    }
}