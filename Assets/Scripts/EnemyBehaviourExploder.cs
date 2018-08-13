using UnityEngine;
using UnityEngine.AI;

namespace Game
{
    public class EnemyBehaviourExploder : EnemyBehaviour
    {
        new public bool HitByBullet(Bullet bullet)
        {
            return true;
        }
    }
}