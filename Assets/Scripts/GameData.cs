using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    // All colors available
    public enum ColorSelection
    {
        Black, Gray, White, Red, Yellow, Green, Cyan, Blue, Magenta
    }

    // Function allowing to get the color based on ColorSelection
    public static Color GetColor(ColorSelection color)
    {
        switch (color)
        {
            case ColorSelection.Black:
                return Color.black;
            case ColorSelection.Blue:
                return Color.blue;
            case ColorSelection.Cyan:
                return Color.cyan;
            case ColorSelection.Gray:
                return Color.gray;
            case ColorSelection.Green:
                return Color.green;
            case ColorSelection.Magenta:
                return Color.magenta;
            case ColorSelection.Red:
                return Color.red;
            case ColorSelection.White:
                return Color.white;
            case ColorSelection.Yellow:
                return Color.yellow;
            default:
                return Color.white;
        }
    }

    public enum LightType
    {
        Laser, Spotlight, Torchlight
    }

    public static float GetLightRange(LightType lt)
    {
        switch (lt)
        {
            case LightType.Laser:
                return 100f;
            case LightType.Spotlight:
                return 12f;
            case LightType.Torchlight:
                return 6f;
            default:
                return 9f;
        }
    }

    public static float GetLightIntensity(LightType lt)
    {
        switch (lt)
        {
            case LightType.Laser:
                return 1f;
            case LightType.Spotlight:
                return 15f;
            case LightType.Torchlight:
                return 9f;
            default:
                return 10f;
        }
    }

    public static float GetLightInnerAngle(LightType lt)
    {
        switch (lt)
        {
            case LightType.Laser:
                return 1f;
            case LightType.Spotlight:
                return 3f;
            case LightType.Torchlight:
                return 1f;
            default:
                return 10f;
        }
    }

    public static float GetLightAngle(LightType lt)
    {
        switch (lt)
        {
            case LightType.Laser:
                return 1f;
            case LightType.Spotlight:
                return 16f;
            case LightType.Torchlight:
                return 13f;
            default:
                return 10f;
        }
    }
}
