using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        TileMap tileMap;
        
        public GameObject blockPrefab;

        void Awake()
        {
            Instance = this;

            tileMap = new TileMap();
        }

        void Start()
        {
            tileMap.SetBlock(new Block(0, 0, Color.Colors[0]));
            tileMap.SetBlock(new Block(1, 0, Color.Colors[1]));
            tileMap.SetBlock(new Block(2, 0, Color.Colors[2]));
            tileMap.SetBlock(new Block(3, 0, Color.Colors[3]));
            tileMap.CreateRepresentation();
        }
    }
}