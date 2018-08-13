using UnityEngine;
using UnityEngine.AI;

namespace Game
{
    public class EnemyBehaviourExploder : EnemyBehaviour
    {

        float angleDiff = 360f / 5f;

        new public bool HitByBullet(Bullet bullet)
        {
            float yOrientation = bullet.transform.rotation.y;
            for(int i = 0; i < 5; i++)
            {
                shoot(new Vector3(Mathf.Sin(yOrientation * Mathf.PI / 180.0F + i * angleDiff), 0, Mathf.Cos(yOrientation * Mathf.PI / 180.0F + i * angleDiff)));
            }
            return true;
        }

        private void shoot(Vector3 direction)
        {
            GameObject bulletObject = Instantiate(GameManager.Instance.bulletPrefab);
            bulletObject.transform.SetPositionAndRotation(transform.position, Quaternion.identity);
            Bullet bullet = bulletObject.GetComponent<Bullet>();
            bullet.SetVelocity(direction.normalized * GameManager.Instance.bulletSpeed);
        }
    }
}