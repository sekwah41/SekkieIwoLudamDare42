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

        public void SetBlock(short x, short y, ColorType colorType, bool checkClusters = true)
        {
            Block block = new Block(this, x, y, colorType);
            Blocks[GetIndexForCoords(x, y)] = block;

            if (checkClusters)
            {
                int sameNeighboursCount = block.CountSameNeighbours();
                Debug.Log("Same Neighbours: " + sameNeighboursCount);
            }

            if (TileMapObject != null)
            {
                block.CreateRepresentation(TileMapObject);
            }
        }

        public Block GetBlock(short x, short z)
        {
            uint index = GetIndexForCoords(x, z);
            return Blocks.ContainsKey(index) ? Blocks[index] : null;
        }

        public bool HasBlock(short x, short z)
        {
            return Blocks.ContainsKey(GetIndexForCoords(x, z));
        }

        public void RemoveBlock(short x, short z)
        {
            Blocks.Remove(GetIndexForCoords(x, z));
        }

        public void CreateRepresentation()
        {
            if (TileMapObject != null)
                UnityEngine.Object.DestroyImmediate(TileMapObject);

            TileMapObject = new GameObject("TileMap");
            TileMapObject.transform.SetPositionAndRotation(new Vector3(0, 0, 0), Quaternion.identity);

            foreach (var pair in Blocks)
            {
                pair.Value.CreateRepresentation(TileMapObject);
            }
        }

        private uint GetIndexForCoords(short x, short z)
        {
            uint index = (uint)x << 16;
            index |= (ushort)z;
            return index;
        }
    }
}