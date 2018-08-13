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
        public AudioClip crystaliseAudio;
        public AudioClip clusterAudio;

        public GameObject blockPrefab;
        public GameObject bulletPrefab;
        public GameObject wallPrefab;
        public EnemySpawner[] spawners;
        public float enemySpawnDelay = 2;
        public float WaveDuration = 10;
        public int SizePerWaveIncrement = 3;
        public float timeAllowedWithoutCombo = 20;

        public ScreenType CurrentScreen { get; set; }
        public TileMap TileMap { get; private set; }
        public Walls Walls { get; private set; }
        public TrackableValue<int> Points { get; private set; }
        public int HighScore { get; private set; }
        public int WaveCount { get; private set; }
        public float WaveTimer { get; private set; }
        public float TimeLeftUntilComboDeath { get; private set; }

        AudioSource audioSource;

        int xRadius = 16;
        int yRadius = 16;

        void Awake()
        {
            Instance = this;

            audioSource = GetComponent<AudioSource>();
            TileMap = new TileMap();
            Walls = new Walls();
            Points = new TrackableValue<int>();
            WaveCount = 0;
            WaveTimer = 0;

            HighScore = PlayerPrefs.GetInt("HighScore", 0);
        }

        void Start()
        {
            OpenScreen(ScreenType.MAIN_MENU);
            TileMap.CreateRepresentation();
            Walls.CreateRepresentation(xRadius, yRadius);
        }

        public bool IsOutOfBounds(short tileX, short tileZ)
        {
            return tileX < -xRadius || tileX > xRadius || tileZ < -yRadius || tileZ > yRadius;
        }

        void OpenScreen(ScreenType screenType)
        {
            Debug.Log("Opening " + screenType.ToString());

            switch (CurrentScreen)
            {
                case ScreenType.MAIN_MENU:
                    UIManager.Instance.backgroundUI.HideBackground();
                    UIManager.Instance.HideMenuBits();
                    break;
                case ScreenType.GAME:
                    
                    break;
                case ScreenType.GAME_OVER:
                    UIManager.Instance.backgroundUI.HideBackground();
                    UIManager.Instance.HideGameOverBits();
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
                    StartGame();
                    break;
                case ScreenType.GAME_OVER:
                    UIManager.Instance.backgroundUI.ShowBackground();
                    UIManager.Instance.ShowGameOverBits();
                    break;
            }
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Debug.Log("Quit");
                Application.Quit();
            }

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
                    if (TimeLeftUntilComboDeath <= 0)
                    {
                        GameOver();
                    }
                    break;
                case ScreenType.GAME_OVER:
                    if (Input.GetButtonDown("Jump"))
                    {
                        OpenScreen(ScreenType.MAIN_MENU);
                    }
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

            StartCoroutine(HandleSpawnEnemy());

            StartNewRound();

            Walls.Resize(xRadius, yRadius);
        }

        public void StartNewRound()
        {
            WaveCount++;
            WaveTimer = WaveDuration;

            int oldXRadius = xRadius;
            int oldYRadius = yRadius;
            xRadius = 16 + (WaveCount-1) * SizePerWaveIncrement;
            yRadius = 16 + (WaveCount-1) * SizePerWaveIncrement;
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

        void GameOver()
        {
            if (CurrentScreen == ScreenType.GAME)
            {
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                foreach (GameObject enemy in enemies)
                {
                    Destroy(enemy);
                }

                GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
                foreach (GameObject bullet in bullets)
                {
                    Destroy(bullet);
                }

                ClearMap();

                StopAllCoroutines();

                if (Points.Value > HighScore)
                {
                    HighScore = Points.Value;
                    PlayerPrefs.SetInt("HighScore", HighScore);
                }
                OpenScreen(ScreenType.GAME_OVER);
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

        void ClearMap()
        {
            TileMap.Clear();
        }

        public void AwardPoints(int points)
        {
            Points.Value = Points.Value + points;
            TimeLeftUntilComboDeath = timeAllowedWithoutCombo;
        }

        public void DrainEnergy(float amount)
        {
            TimeLeftUntilComboDeath -= amount;
            if (TimeLeftUntilComboDeath <= 0)
            {
                GameOver();
            }
        }

        public void PlayCrystaliseSound()
        {
            audioSource.PlayOneShot(crystaliseAudio);
        }

        public void PlayClusterSound()
        {
            audioSource.PlayOneShot(clusterAudio);
        }

        IEnumerator HandleSpawnEnemy()
        {
            while (true)
            {
                yield return new WaitForSeconds(enemySpawnDelay + UnityEngine.Random.value * 0.5F);

                float random = UnityEngine.Random.value;
                EnemySpawner chosenSpawner = spawners[0];
                foreach (var spawner in spawners) {
                    if (random <= spawner.spawnThreshold)
                    {
                        chosenSpawner = spawner;
                        break;
                    }

                }
                chosenSpawner.Spawn();
            }
        }
    }
}