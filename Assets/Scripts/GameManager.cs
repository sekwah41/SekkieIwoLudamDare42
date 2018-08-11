using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public GameObject blockPrefab;
        public GameObject bulletPrefab;

        public TileMap TileMap { get; set; }

        void Awake()
        {
            Instance = this;

            TileMap = new TileMap();
        }

        void Start()
        {
            TileMap.SetBlock(new Block(0, 0, Color.Colors[0]));
            TileMap.SetBlock(new Block(1, 0, Color.Colors[1]));
            TileMap.SetBlock(new Block(2, 0, Color.Colors[2]));
            TileMap.SetBlock(new Block(3, 0, Color.Colors[3]));
            TileMap.CreateRepresentation();
        }
    }
}