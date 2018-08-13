using UnityEngine;
using UnityEngine.AI;

namespace Game
{
    public class EnemyBehaviourPrism : EnemyBehaviour
    {

        float angleDiff = Mathf.PI * 2 / 15F;

        override public bool HitByBullet(Bullet bullet)
        {
            Debug.Log("EXPLOSION");
            float yOrientation = bullet.transform.rotation.y;
            for(int i = -1; i < 2; i++)
            {
                Shoot(new Vector3(Mathf.Sin(yOrientation * Mathf.PI / 180.0F + i * angleDiff), 0, Mathf.Cos(yOrientation * Mathf.PI / 180.0F + i * angleDiff)));
            }
            Destroy(gameObject);
            return true;
        }

        private void Shoot(Vector3 direction)
        {
            GameObject bulletObject = Instantiate(GameManager.Instance.bulletPrefab);
            bulletObject.transform.SetPositionAndRotation(transform.position, Quaternion.identity);
            Bullet bullet = bulletObject.GetComponent<Bullet>();
            bullet.SetColor(ColorType);
            bullet.SetVelocity(direction.normalized * GameManager.Instance.bulletSpeed);
        }
    }
}