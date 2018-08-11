using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class TileMap
    {
        public Dictionary<uint, Block> Blocks { get; private set; }

        GameObject TileMapObject { get; set; }

        public TileMap()
        {
            Blocks = new Dictionary<uint, Block>();
        }

        public void SetBlock(Block block)
        {
            uint index = (uint) block.X << 16;
            index |= (ushort) block.Y;
            Blocks.Add(index, block);

            if (TileMapObject != null)
            {
                block.CreateRepresentation(TileMapObject);
            }
        }

        public Block GetBlock(short x, short y)
        {
            uint index = (uint) x << 16;
            index |= (ushort) y;
            return Blocks[index];
        }

        public void CreateRepresentation()
        {
            if (TileMapObject != null)
                Object.DestroyImmediate(TileMapObject);

            TileMapObject = new GameObject("TileMap");
            TileMapObject.transform.SetPositionAndRotation(new Vector3(0, 0, 0), Quaternion.identity);

            foreach (var pair in Blocks)
            {
                pair.Value.CreateRepresentation(TileMapObject);
            }
        }
    }
}