using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricityTower : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> ennemiesList = new List<GameObject>();
    public GameObject bulletPrefab;
    public GameObject bulletSpawner;
    public ParticleSystem particleLauncher;

    private GameObject objectToRotate;

    [SerializeField] private bool isShooting = false;
    [SerializeField] private float timeBetweenShoot = 0.5f;
    [SerializeField] private float bulletDamages = 1f;
    [SerializeField] private float bulletSpeed = 30f;
    void Start()
    {
        particleLauncher.Stop();
        objectToRotate = particleLauncher.gameObject;
        bulletPrefab.GetComponent<Bullet>().setDamage(bulletDamages);
    }

    // Update is called once per frame
    void Update()
    {
        if (isShooting)
        {
            particleLauncher.Play();

        }
        else
        {
            particleLauncher.Stop();
            CancelInvoke();
        }

        if (ennemiesList.Count > 0)
        {
            GameObject ennemy = ennemiesList[0];
            float dist = Vector3.Distance(objectToRotate.transform.position, ennemy.transform.position);
            // dist = vitesse des prticules

            //rotation of the stone
            /*var dir = ennemy.transform.position - objectToRotate.transform.position; //a vector pointing from pointA to pointB
            var rot = Quaternion.LookRotation(dir, Vector3.up); //calc a rotation that
            objectToRotate.transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, rot.z);*/

            // rotation of the flamethrower
            objectToRotate.transform.LookAt(ennemy.transform);
            float newSpeed = (dist * 5) / 3;

            ParticleSystem.MainModule psmain = particleLauncher.main;
            psmain.startSpeed = newSpeed;

            //Shoot invisible object to ennemy to deal damage
            if (!isShooting)
            {
                isShooting = true;
                //StartCoroutine("InstantiateBullet", ennemy);
                InvokeRepeating("InstantiateBullet", 0.5f, timeBetweenShoot);

            }

        }
        else
        {
            isShooting = false;

        }
    }

    void InstantiateBullet()
    {
        Debug.Log("repeat");
        if (ennemiesList.Count > 0)
        {
            Vector3 ennemyPos = ennemiesList[0].transform.position;
            Vector3 currentPos = bulletSpawner.transform.position;

            Vector3 fromCurrentToEnnemy = ennemyPos - currentPos;
            fromCurrentToEnnemy.Normalize();

            Vector3 velocity = fromCurrentToEnnemy * bulletSpeed;

            GameObject bullet = Instantiate(bulletPrefab, bulletSpawner.transform.position, bulletSpawner.transform.rotation);
            bullet.GetComponent<Rigidbody>().velocity = velocity;
        }


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ennemy")
        {

            ennemiesList.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ennemy")
        {
            for (int i = 0; i < ennemiesList.Count; i++)
            {
                if (other.gameObject == ennemiesList[i])
                {
                    ennemiesList.Remove(ennemiesList[i]);
                }
            }


        }
    }
}
