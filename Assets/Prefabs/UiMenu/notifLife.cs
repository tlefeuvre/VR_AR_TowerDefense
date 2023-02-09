using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class notifLife : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(timerLife());
    }


    IEnumerator timerLife()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
