using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodManager : MonoBehaviour
{
    [SerializeField] float mouseSensitivity;
    [SerializeField] float positionSensitivity;


    // Update is called once per frame
    void Update()
    {
        //transform.eulerAngles += new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0) * Time.deltaTime * mouseSensitivity;

        /*if (Input.GetKey(KeyCode.Z))
        {
            transform.localPosition += transform.forward * positionSensitivity * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.localPosition -= transform.forward * positionSensitivity * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            transform.localPosition -= transform.right * positionSensitivity * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.localPosition += transform.right * positionSensitivity * Time.deltaTime;
        }
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);*/

/*        if (Input.GetKey(KeyCode.Mouse0))
        {
            RaycastHit hit;
            Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity);

            if (hit.collider.transform.tag == "MobSpawn")
            {
                //hit.point;
            }
        }*/
    }
}
