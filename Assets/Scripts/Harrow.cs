using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harrow : MonoBehaviour
{
    [SerializeField] public int Health = 10;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="damages">damages deal to the harrow by the mob attacking</param>
    /// <returns> true if the harrow still have health however false</returns>
    public bool takeDamage(int damages)
    {
        Health -= damages;
        if (Health <= 0)
        {
            return false;
        }
        return true;
    }    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "mob")
        {
            Debug.Log(collision.transform.name);
            collision.transform.GetComponent<MobManager>().attackHarrow(this);
        }
    }
}
