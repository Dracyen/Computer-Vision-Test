using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class strafe : MonoBehaviour
{
    Animator anim;
    Vector3 LastPosHor;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void changePosition(int currZ, int sWidth, float rWidth)
    {
        float position = currZ * rWidth / sWidth;

        LastPosHor = transform.position;

        transform.position = new Vector3(transform.position.x, transform.position.y, position);

        if(LastPosHor.z > transform.position.z)
        {
            anim.SetBool("TurnL", false);
            anim.SetBool("TurnR", true);
        }

        if (LastPosHor.z < transform.position.z)
        {
            anim.SetBool("TurnL", true);
            anim.SetBool("TurnR", false);
        }

        if (LastPosHor.z == transform.position.z)
        {
            anim.SetBool("TurnL", false);
            anim.SetBool("TurnR", false);
        }
    }
}