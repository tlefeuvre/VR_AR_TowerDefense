using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("start");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("On particles collision");

    }
    private void OnParticleTrigger()
    {
        Debug.Log("On particles trigger");
    }
}
