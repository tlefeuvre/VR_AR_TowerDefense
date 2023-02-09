using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonPuddle : MonoBehaviour
{
    bool takingdamage = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(takingdamage == true)
        {

            takingdamage = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "enemies")
            takingdamage = true;
    }
}
