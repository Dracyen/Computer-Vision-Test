using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpAlgo : MonoBehaviour
{
    int lastPos = 0;
    int currPos;

    public float velMargin = 0;
    public Text display;

    bool readyJump = false;

    public void Jump(int currY)
    {
        lastPos = currPos;

        currPos = currY;

        if (currPos > lastPos)
        {
            display.text = "Up";

            if(currPos - lastPos > velMargin)
            {
                display.text = "Fast Up";
                readyJump = true;
            }
        }
        else if(readyJump)
        {
            display.text = "Jump";
            readyJump = false;
        }
    }
}
