namespace Game
{
    public class Color
    {
        public static Color[] Colors = new Color[] {
            new Color(1, 0.1F, 0.1F),
            new Color(0, 1, 0.4F),
            new Color(1, 1.5F, 0.1F),
            new Color(0.1F, 0.6F, 1.0F)
        };

        public float R { get; private set; }
        public float G { get; private set; }
        public float B { get; private set; }

        public Color(float r, float g, float b)
        {
            R = r;
            G = g;
            B = b;
        }
    }
}