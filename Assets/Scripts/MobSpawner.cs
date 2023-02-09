using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    [SerializeField] private GameObject mob;
    [SerializeField] private GameObject currentMob;
    public int gameMode;
    // Start is called before the first frame update
    void Start()
    {

        currentMob =  Instantiate(mob, this.transform.position,Quaternion.identity);

    }

    // Update is called once per frame
    void Update()
    {
        if (gameMode == 1)
        {
            if (currentMob)
            { 
                currentMob.transform.position += Vector3.back * Time.deltaTime;

                if ( currentMob.transform.position.z <= -3)
                    currentMob.transform.position = new Vector3(transform.position.x, transform.position.y, 5);
            }
        }   
    }
}
