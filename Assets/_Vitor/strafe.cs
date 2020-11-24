using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class strafe : MonoBehaviour
{
    Animator anim;
    Vector3 LastPos;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void changePosition(int currZ, int sWidth, float rWidth)
    {
        float position = currZ * rWidth / sWidth;
        LastPos = transform.position;
        transform.position = new Vector3(transform.position.x, transform.position.y, position);
        if(LastPos.z > transform.position.z)
        {
            anim.SetBool("TurnL", false);
            anim.SetBool("TurnR", true);
        }
        if (LastPos.z < transform.position.z)
        {
            anim.SetBool("TurnL", true);
            anim.SetBool("TurnR", false);
        }
        if (LastPos.z == transform.position.z)
        {
            anim.SetBool("TurnL", false);
            anim.SetBool("TurnR", false);
        }
    }
}