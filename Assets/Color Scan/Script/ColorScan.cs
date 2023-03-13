using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorScan : MonoBehaviour
{
    [Header("Render Texture")]
    [Tooltip("RenderTexture used as the output of the camera.")]
    public RenderTexture rtex;
    private Texture2D _tex2D; // Texture2D derived from the RenderTexture

    [Header("Color for activation")]
    [Tooltip("The object to be activated if there is a color match.")]
    public GameObject objectToActivate;
    [Tooltip("The color that is needed for the activation. Shades of color are taken into account.")]
    public enum ColorSelection
    {
        Black, Gray, White, Red, Yellow, Green, Cyan, Blue, Magenta
    }
    public ColorSelection targetColor;

    private Color _targetColor; // Targeted color
    private Color newCol; // The new color detected

    [Header("Debugging")]
    [Tooltip("Check it if you wan't Debug logs and visual debugging.")]
    public bool debugON;
    [Tooltip("Material used for visual debug, can be left null.")]
    public Material visualMat; // Material for visual debug

    [Header("Model3D Manager")]
    public GameObject lightOnOff;
    public GameObject screen; // Quad where the renderTexture mat is showed
    public GameObject colorClue;

    // Start is called before the first frame update
    void Start()
    {
        // Set the chosen color
        SetTargetColor();

        if(lightOnOff == null)
            lightOnOff = transform.Find("Object Model/Scanner/LightOnOff").gameObject;
        if (screen == null)
            screen = transform.Find("Object Model/Screen/Screen").gameObject;
        if (colorClue == null)
            colorClue = transform.Find("Object Model/Scanner/ColorClue").gameObject;

        lightOnOff.GetComponent<Renderer>().material.color = Color.red;
        colorClue.GetComponent<Renderer>().material.color = _targetColor;
    }

    // Update is called once per frame
    void Update()
    {
        // Get the average color
        _tex2D = toTexture2D(rtex);
        newCol = AverageColorFromTexture(_tex2D);



        if (ColorMatch())
        {
            lightOnOff.GetComponent<Renderer>().material.color = Color.green;
            screen.GetComponent<Renderer>().material.color = Color.black;

            if (objectToActivate != null)
            {
                // Activate the GameObject
            }
        }

        if (debugON)
        {
            DebuggingON();
        }

    }

    /// <summary>
    /// Set the chosen color as a Color
    /// </summary>
    public void SetTargetColor()
    {
        switch (targetColor)
        {
            case ColorSelection.Black:
                _targetColor = Color.black;
                break;
            case ColorSelection.Blue:
                _targetColor = Color.blue;
                break;
            case ColorSelection.Cyan:
                _targetColor = Color.cyan;
                break;
            case ColorSelection.Gray:
                _targetColor = Color.gray;
                break;
            case ColorSelection.Green:
                _targetColor = Color.green;
                break;
            case ColorSelection.Magenta:
                _targetColor = Color.magenta;
                break;
            case ColorSelection.Red:
                _targetColor = Color.red;
                break;
            case ColorSelection.White:
                _targetColor = Color.white;
                break;
            case ColorSelection.Yellow:
                _targetColor = Color.yellow;
                break;
            default:
                _targetColor = Color.white;
                break;
        }
    }

    /// <summary>
    /// Look if there is any shades of the chosen color in front of the scanner
    /// </summary>
    /// <returns> False : Not a color match / True : Color match</returns>
    private bool ColorMatch()
    {
        // RGB of seen color
        float r = newCol.r;
        float g = newCol.g;
        float b = newCol.b;

        // RGB of chosen color
        float target_r = _targetColor.r;
        float target_g = _targetColor.g;
        float target_b = _targetColor.b;

        // Look if outside of chosen color shades
        if (r > Mathf.Clamp(target_r + 0.2f, target_r, 1) || r < Mathf.Clamp(target_r - 0.2f, 0, target_r)) // Red shades
            return false;
        if (g > Mathf.Clamp(target_g + 0.2f, target_g, 1) || g < Mathf.Clamp(target_g - 0.2f, 0, target_g)) // Green shades
            return false;
        if (b > Mathf.Clamp(target_b + 0.2f, target_b, 1) || b < Mathf.Clamp(target_b - 0.2f, 0, target_b)) // Blue shades
            return false;
        // Inside color shades
        return true;
    }

    /// <summary>
    /// Return a Texture2D made from a RenderTexture
    /// </summary>
    /// <param name="rTex"> The RenderTexture to be changed to a Texture2D</param>
    /// <returns> Texture2D </returns>
    Texture2D toTexture2D(RenderTexture rTex)
    {
        Texture2D tex = new Texture2D(50, 50, TextureFormat.RGBA32, false);
        // ReadPixels looks at the active RenderTexture.
        RenderTexture.active = rTex;
        tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
        tex.Apply();
        return tex;
    }

    /// <summary>
    /// Look for the averaged color in a Texture2D
    /// </summary>
    /// <param name="tex"> Texture2D </param>
    /// <returns> Color </returns>
    Color AverageColorFromTexture(Texture2D tex)
    {

        Color[] texColors = tex.GetPixels();

        int total = texColors.Length;

        float r = 0;
        float g = 0;
        float b = 0;

        for (int i = 0; i < total; i++)
        {

            r += texColors[i].r;

            g += texColors[i].g;

            b += texColors[i].b;

        }
        Color col32 = new Color((byte)(r / total), (byte)(g / total), (byte)(b / total), 1);
        return col32;

    }

    /// <summary>
    /// Debugging function
    /// </summary>
    private void DebuggingON()
    {
        if (visualMat != null)
        {
            visualMat.color = newCol;
        }
        Debug.Log("Chosen Color : " + _targetColor);
        Debug.Log("Scanned Color : " + newCol);

        if (ColorMatch())
        {
            Debug.Log("! ! Same Color ! ! ");
        }

    }
}
