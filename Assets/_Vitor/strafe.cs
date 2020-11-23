using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class strafe : MonoBehaviour
{
    public void changePosition(int currZ, int sWidth, float rWidth)
    {
        float position = currZ * rWidth / sWidth;

        transform.position = new Vector3(transform.position.x, transform.position.y, position * 4);
    }
}