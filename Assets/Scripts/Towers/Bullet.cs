using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float damages = 0;
    [SerializeField]
    private float waitTime = 5f;
    private void Start()
    {
        StartCoroutine("DestroyBullet");
    }
    public void setDamage(float newDamages)
    {
        damages = newDamages;
    }
    public float GetBulletDamage()
    {
        return damages;
    }
    private IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(waitTime);
        Debug.Log("Destruction");
        Destroy(gameObject);
    }
  
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ennemy" || other.gameObject.tag == "mob" )
        {
            Debug.Log("mob detected trigger");
            if(other.gameObject.GetComponent<MobLife>())
                other.gameObject.GetComponent<MobLife>().TakeDamage(damages);
        }
    }
}
