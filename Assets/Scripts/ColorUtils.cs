using System;
using UnityEngine;

namespace Game
{
    public class ColorUtils
    {
        public static Color[] Colors = new Color[] {
            new Color(1, 0.1F, 0.1F),
            new Color(0, 1, 0.4F),
            new Color(1, 1.5F, 0.1F),
            new Color(0.1F, 0.6F, 1.0F)
        };

        public static Color GetColor(ColorType type)
        {
            return Colors[(int)type];
        }

        public static ColorType GetRandomColorType()
        {
            return (ColorType) Mathf.RoundToInt(UnityEngine.Random.value * (Enum.GetNames(typeof(ColorType)).Length - 1));
        }
    }
}