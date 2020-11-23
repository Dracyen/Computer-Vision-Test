using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Move : MonoBehaviour
{
    public Text Speed;
    public Text Distance;
    Rigidbody rb;
    public bool Hit;
    bool jump = true;
    Animator anim;
    float vel;
    int inicialVel = 20;
    float normalvel;
    int TurnVel = 10;
    int normalTurnVel = 10;

    // Start is called before the first frame update
    void Start()
    {
        normalvel = inicialVel;
        vel = 0;
        StartCoroutine("UpTheSpeed");
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    IEnumerator JumpCoolDown()
    {
        yield return new WaitForSeconds(0.2f);
        jump = false;
    }

    IEnumerator HitCoolDown()
    {
        yield return new WaitForSeconds(2f);
        TurnVel = normalTurnVel;
    }

    IEnumerator UpTheSpeed()
    {
        yield return new WaitForSeconds(1f);
        if (vel < 35)
        {
            vel += 0.1f;
            StartCoroutine("UpTheSpeed");
        }
       
    }


    // Update is called once per frame
    void Update()
    {
        normalvel = vel + inicialVel;
        Speed.text = "Speed: " + normalvel.ToString();
        Distance.text = "Score: " + ((int) transform.position.x / 10).ToString();
        rb.velocity = new Vector3(normalvel, 0,0);
        if (Input.GetKey(KeyCode.D))
        {
            anim.SetBool("TurnR",true);
            MoveRight();
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            anim.SetBool("TurnR", false);
        }
        if (Input.GetKey(KeyCode.A))
        {
            anim.SetBool("TurnL", true);
            MoveLeft();
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            anim.SetBool("TurnL", false);
        }
        if (Input.GetKeyDown(KeyCode.Space) && jump)
        {
            StartCoroutine("JumpCoolDown");
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Crouch();
        }
        
    }

    void MoveRight()
    {
        rb.velocity = new Vector3(normalvel, 0, -TurnVel);
    }

    void MoveLeft()
    {
        rb.velocity = new Vector3(normalvel, 0, TurnVel);
    }

    void Jump()
    {
        anim.SetTrigger("Jump");
        rb.velocity = new Vector3(normalvel, 15, 0);
    }

    void Crouch()
    {
        anim.SetTrigger("Crouch");
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger");
        if (other.tag == "Destroyer")
        {
            Hit = true;
        }
        
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Trigger");
        if (other.tag == "Destroyer")
        {
            Hit = false;
        }
        if (other.tag == "Barrier")
        {
            StartCoroutine("HitCoolDown");
            vel = 0;
            //TurnVel = TurnVel = normalTurnVel / 2; ;
            anim.SetTrigger("Hit");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            jump = true;
        }
    }
}
