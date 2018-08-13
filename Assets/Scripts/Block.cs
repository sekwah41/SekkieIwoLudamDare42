using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Block
    {
        public TileMap TileMap { get; private set; }
        public short X { get; private set; }
        public short Z { get; private set; }
        public ColorType Color { get; private set; }
        public int ClusterSize { get { return clusterSize; }
            set {
                if (Representation != null)
                    Representation.ClusterSize = value;
                clusterSize = value;
            }
        }
        public BlockBehaviour Representation { get; private set; }

        private int clusterSize = 0;

        public Block(TileMap tileMap, short x, short z, ColorType color)
        {
            TileMap = tileMap;
            X = x;
            Z = z;
            Color = color;
        }

        public void CreateRepresentation(GameObject tileMapObject)
        {
            GameObject gameObject = Object.Instantiate(GameManager.Instance.blockPrefab);
            Representation = gameObject.GetComponent<BlockBehaviour>();
            Representation.transform.SetPositionAndRotation(new Vector3(X, 0, Z), Quaternion.identity);
            Representation.transform.SetParent(tileMapObject.transform);
            Representation.ClusterSize = ClusterSize;
            Representation.SetColor(Color);
        }

        public void BreakWithReward()
        {
            Break();
            GameManager.Instance.AwardPoints(2);
        }

        public void Break()
        {
            TileMap.RemoveBlock(X, Z);
            GameObject.Destroy(Representation.gameObject);
        }

        public void StartChainReaction()
        {
            List<Block> cluster = GetCluster();
            if (cluster.Count >= 3)
            {
                GameManager.Instance.PlayClusterSound();

                foreach (Block block in cluster)
                {
                    block.BreakWithReward();
                }
                GameManager.Instance.AwardPoints((cluster.Count - 3) * 3);
            }
        }

        public List<Block> GetCluster()
        {
            List<Block> list = new List<Block>();
            GetCluster(list);
            return list;
        }

        public int CountSameNeighbours()
        {
            List<Block> list = GetCluster();
            
            foreach (Block block in list)
            {
                block.ClusterSize = list.Count;
            }

            return list.Count;
        }

        private void GetCluster(List<Block> list)
        {
            Block up = TileMap.GetBlock(X, (short)(Z - 1));
            Block right = TileMap.GetBlock((short)(X + 1), Z);
            Block down = TileMap.GetBlock(X, (short)(Z + 1));
            Block left = TileMap.GetBlock((short)(X - 1), Z);
            List<Block> neighbours = new List<Block>();
            if (up != null) neighbours.Add(up);
            if (right != null) neighbours.Add(right);
            if (down != null) neighbours.Add(down);
            if (left != null) neighbours.Add(left);

            list.Add(this);
            foreach (Block neighbour in neighbours)
            {
                if (neighbour.Color == Color && !list.Contains(neighbour))
                {
                    neighbour.GetCluster(list);
                }
            }
        }
    }
}