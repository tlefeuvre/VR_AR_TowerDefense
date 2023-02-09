using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nexus_Managment : MonoBehaviour
{

    public int NexusCurrentLife;
    private int NexusMaxLife = 150;
    public bool GameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        NexusCurrentLife = NexusMaxLife;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //slider.value = NexusCurrentLife/NexusMaxLife;

        if (NexusCurrentLife <= 0)
        {
            GameOver = true;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemies")
        {
            NexusCurrentLife -= 10;
        }
    }
}