using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Life : MonoBehaviour
{
    public Slider Life;
    public Image LifeColor;
    int PlayerLife;
    int PlayerMaxLife = 100;
    int damege = 25;
    public GameObject EndScreen;
    bool canTakeDamege = true;
    // Start is called before the first frame update
    void Start()
    {
        PlayerLife = PlayerMaxLife;
    }
    IEnumerator HitCoolDown()
    {
        canTakeDamege = false;
        yield return new WaitForSeconds(0.7f);
        canTakeDamege = true;
    }
    // Update is called once per frame
    void Update()
    {
        if(PlayerLife > 50)
        {
            LifeColor.color = Color.green;
        }
        if (PlayerLife == 50)
        {
            LifeColor.color = Color.yellow;
        }
        if (PlayerLife == 25)
        {
            LifeColor.color = Color.red;
        }
        Life.value = PlayerLife;
        if (PlayerLife <= 0)
        {
            this.gameObject.SetActive(false);
            EndScreen.gameObject.SetActive(true);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Barrier" && canTakeDamege)
        {
            StartCoroutine("HitCoolDown");
            PlayerLife -= damege;
        }

    }
}
