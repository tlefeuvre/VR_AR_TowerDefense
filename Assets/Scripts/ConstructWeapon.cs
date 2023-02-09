using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructWeapon : MonoBehaviour
{

    public GameObject bulletPrefab;
    public GameObject bulletSpawner;
    [SerializeField] private float timeBetweenShoot = 0.5f;
    [SerializeField] private float bulletSpeed = 30f;
    // Start is called before the first frame update
    void Start()
    {
            InvokeRepeating("InstantiateBullet", 0.5f, timeBetweenShoot);

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void InstantiateBullet()
    {
        Vector3 velocity = transform.forward * bulletSpeed;
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawner.transform.position, bulletSpawner.transform.rotation);
        bullet.GetComponent<Rigidbody>().velocity = velocity;
    }
}
