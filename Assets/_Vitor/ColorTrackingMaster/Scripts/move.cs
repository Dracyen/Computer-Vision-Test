﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour {

    public void changePosition(int x, int y)
    {
        transform.position = new Vector2(x, y);

        Debug.Log("Position = " + x + " - " + y);
    }
}