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
            case GameData.ColorSelection.Black:
                return Color.black;
            case GameData.ColorSelection.Blue:
                return Color.blue;
            case GameData.ColorSelection.Cyan:
                return Color.cyan;
            case GameData.ColorSelection.Gray:
                return Color.gray;
            case GameData.ColorSelection.Green:
                return Color.green;
            case GameData.ColorSelection.Magenta:
                return Color.magenta;
            case GameData.ColorSelection.Red:
                return Color.red;
            case GameData.ColorSelection.White:
                return Color.white;
            case GameData.ColorSelection.Yellow:
                return Color.yellow;
            default:
                return Color.white;
        }
    }

    public enum LightType
    {
        Laser, Spotlight, Torchlight
    }
}
