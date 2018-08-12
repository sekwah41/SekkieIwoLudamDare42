using UnityEngine;

namespace Game
{
    public class Walls
    {
        GameObject up;
        GameObject down;
        GameObject left;
        GameObject right;

        public Walls()
        {

        }

        public void CreateRepresentation(int xRadius, int yRadius)
        {
            up = CreateWall(-xRadius - 1, -yRadius - 1, xRadius*2 + 3, 1);
            down = CreateWall(-xRadius - 1, yRadius + 1, xRadius*2 + 3, 1);
            left = CreateWall(-xRadius - 1, -yRadius - 1, 1, yRadius * 2 + 3);
            right = CreateWall(xRadius + 1, -yRadius - 1, 1, yRadius * 2 + 3);
        }

        public GameObject CreateWall(int x, int z, int width, int height)
        {
            GameObject gameObject = GameObject.Instantiate(GameManager.Instance.wallPrefab);

            gameObject.transform.position = new Vector3(x, 0, z);
            gameObject.transform.localScale = new Vector3(width, 1, height);

            return gameObject;
        }

        public void ResizeWall(GameObject wall, int x, int z, int width, int height)
        {
            wall.transform.position = new Vector3(x, 0, z);
            wall.transform.localScale = new Vector3(width, 1, height);
        }

        public void Resize(int xRadius, int yRadius)
        {
            ResizeWall(up, -xRadius - 1, -yRadius - 1, xRadius * 2 + 3, 1);
            ResizeWall(down, -xRadius - 1, yRadius + 1, xRadius * 2 + 3, 1);
            ResizeWall(left, -xRadius - 1, -yRadius - 1, 1, yRadius * 2 + 3);
            ResizeWall(right, xRadius + 1, -yRadius - 1, 1, yRadius * 2 + 3);
        }
    }
}