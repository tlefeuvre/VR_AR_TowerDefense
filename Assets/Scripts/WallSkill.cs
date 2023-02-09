using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSkill : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject magicBallParticles;
    [SerializeField] private GameObject explosionParticles;
    [SerializeField] private float lifeDuration;
    [SerializeField] private bool isInfinite = false;

    //[SerializeField] private ParticleSystem explosionParticles;
    void Start()
    {
        Debug.Log("Apparition magic ball");
        magicBallParticles.GetComponent<ParticleSystem>().Play();
        if(!isInfinite)
            explosionParticles.SetActive(false);
        explosionParticles.GetComponent<ParticleSystem>().Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator DestroyWall()
    {
        yield return new WaitForSeconds(lifeDuration);
        Destroy(this.gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Rigidbody>().useGravity = false;

        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);

        Debug.Log("collision");
        magicBallParticles.GetComponent<ParticleSystem>().Stop();
        explosionParticles.SetActive(true);
        explosionParticles.GetComponent<ParticleSystem>().Play();

        if(!isInfinite)
            StartCoroutine("DestroyWall");
    }
}
