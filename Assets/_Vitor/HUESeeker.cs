using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUESeeker : MonoBehaviour
{

    WebCamTexture webcamTexture;
    public RawImage rawimage;
    public RawImage cvimage;
    public Color[] data;
    public move _movegreen;

    public float hueThreshold = 0.05f;
    public float saturationThreshold = 0.35f;
    public float valueThreshold = 0.35f;
    
    public Color targetColor;
    public float tH, tS, tV;

    public float TrackSize = 3;
    public Player_Move _moveplayer;

    int avgGreenx = 0;
    int avgGreeny = 0;

    int pixelCount = 0;
    int spotCount = 0;

    void Start()
    {
        WebCamDevice[] devices = WebCamTexture.devices;

        for (int i = 0; i < devices.Length; i++)
        {
            print("Webcam available: " + devices[i].name);
        }

        webcamTexture = new WebCamTexture(devices[0].name, 800, 600);
        webcamTexture.requestedFPS = 30;
        rawimage.texture = webcamTexture;

        webcamTexture.Play();

        Color.RGBToHSV(targetColor, out tH, out tS, out tV);
    }

    void Update()
    {
        ColorDetector();
    }

    void ColorDetector()
    {
        avgGreenx = 0;
        avgGreeny = 0;

        pixelCount = 0;

        if (webcamTexture.didUpdateThisFrame)
        {
            data = webcamTexture.GetPixels();

            float H, S, V;

            Color.RGBToHSV(data[0 + 0 * webcamTexture.width], out H, out S, out V);

            AlgorithmHSV();

            if (pixelCount > 0)
            {
                Debug.Log("PixelCount - " + pixelCount);
                _movegreen.changePosition(Screen.width - avgGreenx / pixelCount, avgGreeny / pixelCount);
                _moveplayer.changePosition(avgGreenx / pixelCount, Screen.width, TrackSize);
                _moveplayer.ScreenPosition(avgGreeny / pixelCount, Screen.height);
            }
        }
    }

    void AlgorithmHSV()
    {
        for (int x = 0; x < webcamTexture.width; x++)
        {
            for (int y = 0; y < webcamTexture.height; y++)
            {
                float H, S, V;

                Color.RGBToHSV(data[x + y * webcamTexture.width], out H, out S, out V);

                if(tH + hueThreshold > H && H > tH - hueThreshold)
                {
                    if(tS - saturationThreshold < S && tV - valueThreshold < V)
                    {
                        spotCount = 0;

                        for(int j = x - 1; j < x + 2; j++)
                        {
                            for (int k = y - 1; k < y + 2; k++)
                            {
                                if (tH + hueThreshold > H && H > tH - hueThreshold)
                                {
                                    if (tS - saturationThreshold < S && tV - valueThreshold < V)
                                    {
                                        spotCount += 1;
                                    }
                                }
                            }
                        }

                        if(spotCount >= 6)
                        {
                            avgGreenx += Screen.width * x / webcamTexture.width;
                            avgGreeny += Screen.height * y / webcamTexture.height;

                            pixelCount += 1;
                        }
                    }
                }
            }
        }
    }
}