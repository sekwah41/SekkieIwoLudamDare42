using Game.UI;
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
        public GameObject wallPrefab;
        public EnemySpawner[] spawners;
        public float WaveDuration = 10;
        public int SizePerWaveIncrement = 3;
        public float timeAllowedWithoutCombo = 20;

        public ScreenType CurrentScreen { get; set; }
        public TileMap TileMap { get; private set; }
        public Walls Walls { get; private set; }
        public TrackableValue<int> Points { get; private set; }
        public int WaveCount { get; private set; }
        public float WaveTimer { get; private set; }
        public float TimeLeftUntilComboDeath { get; private set; }

        int xRadius = 10;
        int yRadius = 10;

        void Awake()
        {
            Instance = this;

            CurrentScreen = ScreenType.MAIN_MENU;
            TileMap = new TileMap();
            Walls = new Walls();
            Points = new TrackableValue<int>();
            WaveCount = 0;
            WaveTimer = 0;
        }

        public bool IsOutOfBounds(short tileX, short tileZ)
        {
            return tileX < -xRadius || tileX > xRadius || tileZ < -yRadius || tileZ > yRadius;
        }

        void OpenScreen(ScreenType screenType)
        {
            switch (CurrentScreen)
            {
                case ScreenType.MAIN_MENU:
                    break;
                case ScreenType.GAME:
                    StopAllCoroutines();
                    break;
            }

            CurrentScreen = screenType;

            switch (screenType)
            {
                case ScreenType.MAIN_MENU:
                    UIManager.Instance.backgroundUI.ShowBackground();
                    UIManager.Instance.ShowMenuBits();
                    break;
                case ScreenType.GAME:
                    UIManager.Instance.backgroundUI.HideBackground();
                    UIManager.Instance.HideMenuBits();

                    StartGame();

                    foreach (EnemySpawner spawner in spawners)
                    {
                        StartCoroutine(HandleSpawnEnemy(spawner));
                    }
                    break;
            }
        }

        void Start()
        {
            TileMap.CreateRepresentation();
            Walls.CreateRepresentation(xRadius, yRadius);
        }

        void Update()
        {
            switch (CurrentScreen)
            {
                case ScreenType.MAIN_MENU:
                    if (Input.GetButtonDown("Jump"))
                    {
                        OpenScreen(ScreenType.GAME);
                    }
                    break;
                case ScreenType.GAME:
                    WaveTimer -= Time.deltaTime;
                    if (WaveTimer < 0)
                    {
                        StartNewRound();
                    }
                    TimeLeftUntilComboDeath -= Time.deltaTime;
                    UIManager.Instance.comboSlider.value = TimeLeftUntilComboDeath / timeAllowedWithoutCombo;
                    break;
            }
        }

        public void StartGame()
        {
            GenerateStartMap();
            WaveCount = 0;
            Points.Value = 0;
            TimeLeftUntilComboDeath = timeAllowedWithoutCombo;
            player.transform.position = new Vector3(0.5F, 0, 0.5F);

            StartNewRound();
        }

        public void StartNewRound()
        {
            WaveCount++;
            WaveTimer = WaveDuration;

            int oldXRadius = xRadius;
            int oldYRadius = yRadius;
            xRadius = 10 + (WaveCount-1) * SizePerWaveIncrement;
            yRadius = 10 + (WaveCount-1) * SizePerWaveIncrement;
            Walls.Resize(xRadius, yRadius);

            // Left
            for (int x = -xRadius; x < -oldXRadius; x++)
            {
                for (int y = -yRadius; y <= yRadius; y++)
                {
                    TileMap.SetBlock((short) x, (short) y, ColorUtils.GetRandomColorType(), false);
                }
            }

            // Right
            for (int x = oldXRadius+1; x <= xRadius; x++)
            {
                for (int y = -yRadius; y <= yRadius; y++)
                {
                    TileMap.SetBlock((short)x, (short)y, ColorUtils.GetRandomColorType(), false);
                }
            }

            // Up
            for (int x = -oldXRadius; x <= oldXRadius; x++)
            {
                for (int y = -yRadius; y < -oldYRadius; y++)
                {
                    TileMap.SetBlock((short)x, (short)y, ColorUtils.GetRandomColorType(), false);
                }
            }

            // Down
            for (int x = -oldXRadius; x <= oldXRadius; x++)
            {
                for (int y = oldYRadius+1; y <= yRadius; y++)
                {
                    TileMap.SetBlock((short)x, (short)y, ColorUtils.GetRandomColorType(), false);
                }
            }
        }

        void GenerateStartMap()
        {
            for (short x = (short)-xRadius; x <= xRadius; x++)
            {
                TileMap.SetBlock(x, (short)-yRadius, ColorUtils.GetRandomColorType(), false);
                TileMap.SetBlock(x, (short)yRadius, ColorUtils.GetRandomColorType(), false);
            }
            for (short y = (short)(-yRadius + 1); y < yRadius; y++)
            {
                TileMap.SetBlock((short)-xRadius, y, ColorUtils.GetRandomColorType(), false);
                TileMap.SetBlock((short)xRadius, y, ColorUtils.GetRandomColorType(), false);
            }
        }

        public void AwardPoints(int points)
        {
            Points.Value = Points.Value + points;
            TimeLeftUntilComboDeath = timeAllowedWithoutCombo;
        }

        IEnumerator HandleSpawnEnemy(EnemySpawner spawner)
        {
            while (true)
            {
                yield return new WaitForSeconds(spawner.spawnDelay);
                spawner.Spawn();
            }
        }
    }
}