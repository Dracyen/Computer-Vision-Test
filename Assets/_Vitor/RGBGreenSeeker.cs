using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RGBGreenSeeker : MonoBehaviour
{

    WebCamTexture webcamTexture;
    public RawImage rawimage;
    public Color32[] data;
    public move _movegreen;
    public int greenThreshold = 25;
    public int otherThreshold = 35;

    public float TrackSize = 3;
    public Player_Move _moveplayer;

    int avgGreenx = 0;
    int avgGreeny = 0;

    int greenPixelCount = 0;

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
        data = new Color32[webcamTexture.width * webcamTexture.height];
    }

    void Update()
    {
        GreenDetector();
    }

    void GreenDetector()
    {
        avgGreenx = 0;
        avgGreeny = 0;

        greenPixelCount = 0;

        if (webcamTexture.didUpdateThisFrame)
        {
            webcamTexture.GetPixels32(data);

            AlgorithmRGB();

            if (greenPixelCount > 0)
            {
                _movegreen.changePosition(Screen.width - avgGreenx / greenPixelCount, avgGreeny / greenPixelCount);
                _moveplayer.changePosition(avgGreenx / greenPixelCount, Screen.width, TrackSize);
                _moveplayer.ScreenPosition(avgGreeny / greenPixelCount, Screen.height);
            }
        }
    }

    void AlgorithmRGB()
    {
        for (int x = 0; x < webcamTexture.width; x++)
        {
            for (int y = 0; y < webcamTexture.height; y++)
            {
                if (data[x + y * webcamTexture.width].g > 255 - greenThreshold)
                {
                    if (data[x + y * webcamTexture.width].r < data[x + y * webcamTexture.width].g - otherThreshold && data[x + y * webcamTexture.width].b < data[x + y * webcamTexture.width].g - otherThreshold)
                    {
                        avgGreenx += Screen.width * x / webcamTexture.width;
                        avgGreeny += Screen.height * y / webcamTexture.height;

                        greenPixelCount += 1;
                    }
                }
            }
        }
    }
}