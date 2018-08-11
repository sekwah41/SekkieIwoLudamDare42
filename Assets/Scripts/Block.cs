using UnityEngine;

namespace Game
{
    public class Block
    {
        public short X { get; private set; }
        public short Y { get; private set; }
        public Color Color { get; private set; }

        public Block(short x, short y, Color color)
        {
            X = x;
            Y = y;
            Color = color;
        }

        public void CreateRepresentation(GameObject tileMapObject)
        {
            GameObject blockObject = Object.Instantiate(GameManager.Instance.blockPrefab);
            MeshRenderer renderer = blockObject.GetComponentInChildren<MeshRenderer>();
            renderer.material.color = new UnityEngine.Color(Color.R, Color.G, Color.B);
            blockObject.transform.SetPositionAndRotation(new Vector3(X, 0, Y), Quaternion.identity);
            blockObject.transform.SetParent(tileMapObject.transform);
        }
    }
}