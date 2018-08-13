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
            Debug.Log(yOrientation);
            for (int i = 0; i < 1; i++)
            {
                Shoot(new Vector3(Mathf.Cos(yOrientation + i * angleDiff), 0, Mathf.Sin(yOrientation + i * angleDiff)));
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