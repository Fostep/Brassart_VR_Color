using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorScan : MonoBehaviour
{
   
    private RenderTexture _rtex;
    private Texture2D _tex2D; // Texture2D derived from the RenderTexture
    private Camera _cam;
    private Material _material;

    public bool onOff;
    [Header("Color for activation")]
    [Tooltip("The object to be activated if there is a color match.")]
    public GameObject objectToActivate;
    [Tooltip("The color that is needed for the activation. Shades of color are taken into account.")]
    public GameData.ColorSelection targetColor;

    private Color _targetColor; // Targeted color
    private Color newCol; // The new color detected

    [Header("Debugging")]
    [Tooltip("Check it if you wan't Debug logs and visual debugging.")]
    public bool debugON;
    [Tooltip("Material used for visual debug, can be left null.")]
    private GameObject _visualObject; // Material for visual debug


    // Start is called before the first frame update
    void Start()
    {
        // Set the chosen color
        _targetColor = GameData.GetColor(targetColor);
        _rtex = new RenderTexture(50, 50, 16, RenderTextureFormat.ARGB32);
        _tex2D = new Texture2D(50, 50, TextureFormat.RGBA32, false);
        _cam = transform.Find("Camera").GetComponent<Camera>();
        
        _material = new Material(Shader.Find("Specular"));
        _material.mainTexture = _rtex;

        _visualObject = transform.Find("VisualObject").gameObject;
        _visualObject.GetComponent<Renderer>().material = _material;

        if(_cam != null)
        {
            _cam.targetTexture = _rtex;
        }


        _tex2D = toTexture2D(_rtex);
        newCol = AverageColorFromTexture(_tex2D);
        _visualObject.SetActive(debugON);

    }

    // Update is called once per frame
    void Update()
    {
        // Get the average color


        if (onOff)
        {
            _tex2D = toTexture2D(_rtex);
            newCol = AverageColorFromTexture(_tex2D);
            if (ColorMatch())
            {
            

                if (objectToActivate != null)
                {
                    // objectToActivate.Activate(); Activate the GameObject
                }
                onOff = false;

            }
        }

        if (debugON)
        {
            DebuggingON();
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
        Debug.Log("AverageColor: "+col32);
        return col32;

    }

    /// <summary>
    /// Debugging function
    /// </summary>
    private void DebuggingON()
    {
        
        Debug.Log("Chosen Color : " + _targetColor);
        Debug.Log("Scanned Color : " + newCol);

        if (ColorMatch())
        {
            Debug.Log("! ! Same Color ! ! ");
        }

    }
}
