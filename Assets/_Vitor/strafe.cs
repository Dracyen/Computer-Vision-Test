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
            anim.SetBool("TurnR", false);
            anim.SetBool("TurnL", true);
        }
        if (LastPos.z < transform.position.z)
        {
            anim.SetBool("TurnR", true);
            anim.SetBool("TurnL", false);
        }
        if (LastPos.z == transform.position.z)
        {
            anim.SetBool("TurnR", false);
            anim.SetBool("TurnL", false);
        }
    }
}