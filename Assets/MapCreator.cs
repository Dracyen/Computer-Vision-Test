using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator : MonoBehaviour
{
    public GameObject[] road;
    GameObject backRoad;
    GameObject middleRoad;
    GameObject frontRoad;
    Vector3 instantiatePos = new Vector3(30, 0, 0);
    public Player_Move Player;
    bool build = false;
    // Start is called before the first frame update
    void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Move>();
        backRoad = Instantiate(road[0], new Vector3(-30, 0, 0), Quaternion.identity);
        middleRoad = Instantiate(road[0], new Vector3(0, 0, 0), Quaternion.identity);
        frontRoad = Instantiate(road[0], instantiatePos, Quaternion.identity);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Player.Hit && !build)
        {
            Rebuild();
            build = true;
        }
        if (!Player.Hit)
        {
            build = false;
        }
    }

    void Rebuild()
    {
        Debug.Log("Player");
        instantiatePos = instantiatePos + new Vector3(30, 0, 0);
        Destroy(backRoad);
        backRoad = middleRoad;
        middleRoad = frontRoad;
        frontRoad = Instantiate(road[Random.Range(0,road.Length)], instantiatePos, Quaternion.identity);
    }
    
}
