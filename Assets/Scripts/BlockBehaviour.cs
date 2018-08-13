using UnityEngine;

namespace Game
{
    public class BlockBehaviour : MonoBehaviour
    {
        public int ClusterSize
        {
            get { return clusterSize; }

            set
            {
                clusterSize = value;
                UpdateColor();
            }
        }

        private int clusterSize = 0;
        private Color color;
        public ColorType ColorType { get; private set; }

        new MeshRenderer renderer;

        void Awake()
        {
            renderer = GetComponentInChildren<MeshRenderer>();
        }

        public void SetColor(ColorType colorType)
        {
            ColorType = colorType;
            color = ColorUtils.GetColor(ColorType);

            MeshRenderer renderer = GetComponentInChildren<MeshRenderer>();
            renderer.material.color = ColorUtils.GetColor(ColorType);

            UpdateColor();
        }

        public void UpdateColor()
        {
            if (ClusterSize < 3)
            {
                renderer.material.SetColor("_EmissionColor", Color.black);
            }
        }

        void Update()
        {
            if (ClusterSize >= 3)
            {
                renderer.material.SetColor("_EmissionColor", color * 0.5F * (Mathf.Sin(Time.time * 8.0F)*0.5F + 0.5F));
            }
        }
    }
}