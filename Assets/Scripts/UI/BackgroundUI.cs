using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class BackgroundUI : MonoBehaviour
    {
        public Color backgroundColor;
        public float visibleAlpha = 0.7F;

        public bool Visible { get; private set; }

        Image image;

        void Awake()
        {
            image = GetComponent<Image>();
        }

        public void ShowBackground()
        {
            Visible = true;
            Color color = new Color(backgroundColor.r, backgroundColor.g, backgroundColor.b, backgroundColor.a);
            image.color = color;
        }

        public void HideBackground()
        {
            Visible = false;
            image.color = new Color(1, 1, 1, 0);
        }
    }
}