using UnityEngine;
using UnityEngine.AI;

namespace Game
{
    public class EnemyBehaviour : MonoBehaviour
    {
        public ColorType ColorType { get; private set; }

        protected Player target;

        protected NavMeshPath path;
        protected float recalculate = 0.0f;

        protected NavMeshAgent agent;

        protected void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            target = GameManager.Instance.player;
            path = new NavMeshPath();
            recalculate = 0.0F;
        }

        protected void Update()
        {
            // agent.SetDestination(target.transform.position);

            recalculate += Time.deltaTime;
            if (recalculate > 0.5F)
            {
                recalculate -= 0.5F;
                NavMesh.CalculatePath(transform.position, target.transform.position, NavMesh.AllAreas, path);
                if(path.status != NavMeshPathStatus.PathInvalid)
                {
                    agent.SetPath(path);
                }
            }

            for (int i = 0; i < path.corners.Length - 1; i++)
                Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);
        }
        
        public bool HitByBullet(Bullet bullet)
        {
            return false;
        }

        protected void OnCollisionEnter(Collision collision)
        {
            Player player = collision.collider.GetComponent<Player>();
            if (player != null)
            {
                GameManager.Instance.DrainEnergy(1);
            }
        }

        public void SetColor(ColorType colorType)
        {
            ColorType = colorType;
            MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();

            foreach (MeshRenderer renderer in renderers)
            {
                renderer.material.color = ColorUtils.GetColor(ColorType);
            }
        }
    }
}