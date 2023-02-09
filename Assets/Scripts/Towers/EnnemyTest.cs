using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyTest : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float currentHealth = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger coté ennemy");
        if(other.tag == "Bullet")
        {
            currentHealth -= other.GetComponent<Bullet>().GetBulletDamage();
        }

    }
    private void OnParticleTrigger()
    {
        Debug.Log("particles trigger");
    }
    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("particles collisionb");

    }
}
