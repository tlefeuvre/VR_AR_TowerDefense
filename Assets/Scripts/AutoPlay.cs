using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoPlay : MonoBehaviour
{
    public ParticleSystem explosion;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("AutoPlayExplosion", 2.0f, 3.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void AutoPlayExplosion()
    {
        if (explosion)
        {
            explosion.Stop();

            explosion.Play();
        }
    }
}
