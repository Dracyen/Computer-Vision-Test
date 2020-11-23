using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyX : MonoBehaviour
{
    public GameObject original;

    public GameObject copy;

    public int offset = 0;

    void Update()
    {
        copy.transform.position = new Vector3(original.transform.position.x + offset, copy.transform.position.y, copy.transform.position.z);
    }
}
