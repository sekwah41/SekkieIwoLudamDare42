using Game.Utils;
using System;
using System.Collections;
using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public Player player;

        public GameObject blockPrefab;
        public GameObject bulletPrefab;
        public EnemySpawner[] spawners;

        public TileMap TileMap { get; private set; }
        public TrackableValue<int> Points { get; private set; }

        void Awake()
        {
            Instance = this;

            TileMap = new TileMap();
            Points = new TrackableValue<int>();

            foreach (EnemySpawner spawner in spawners)
            {
                StartCoroutine(HandleSpawnEnemy(spawner));
            }
        }

        void Start()
        {
            GenerateStartMap();
            TileMap.CreateRepresentation();
        }

        void GenerateStartMap()
        {
            short xRadius = 10;
            short yRadius = 10;
            for (short x = (short)-xRadius; x <= xRadius; x++)
            {
                TileMap.SetBlock(x, (short)-yRadius, ColorUtils.GetRandomColorType(), false);
                TileMap.SetBlock(x, yRadius, ColorUtils.GetRandomColorType(), false);
            }
            for (short y = (short)(-yRadius + 1); y < yRadius; y++)
            {
                TileMap.SetBlock((short)-xRadius, y, ColorUtils.GetRandomColorType(), false);
                TileMap.SetBlock(xRadius, y, ColorUtils.GetRandomColorType(), false);
            }
        }

        public void AwardPoints(int points)
        {
            Points.Value = Points.Value + points;
        }

        IEnumerator HandleSpawnEnemy(EnemySpawner spawner)
        {
            while (true)
            {
                spawner.Spawn();
                yield return new WaitForSeconds(spawner.spawnDelay);
            }
        }
    }
}