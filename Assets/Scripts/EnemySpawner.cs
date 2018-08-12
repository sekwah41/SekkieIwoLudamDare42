using UnityEngine;

namespace Game
{
    [System.Serializable]
    public class EnemySpawner
    {
        public GameObject enemyPrefab;
        public float spawnDelay = 5F;

        public void Spawn()
        {
            GameObject enemyObject = Object.Instantiate(enemyPrefab);
            enemyObject.transform.SetPositionAndRotation(new Vector3(Random.value*5, 0, Random.value * 5), Quaternion.identity);
        }
    }
}
