using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalMaterialMaterialize : MonoBehaviour
{
    public GameObject[] parts;
    public List<Renderer> materials;
    public float dissolve = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public float GetDissolve()
    {
        return dissolve;

    }
    public void SetDissolve(float newDissolve)
    {
        dissolve = newDissolve;
    }
    // Update is called once per frame
    void Update()
    {
        if(materials.Count == 0)
        {
            foreach (GameObject part in parts)
                if(part.GetComponent<Renderer>() != null)
                    materials.Add(part.GetComponent<Renderer>());
        }
        foreach (Renderer mat in materials)
            mat.material.SetFloat("_Dissolve", dissolve);
        
    }

 
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ConstructBullet")
        {
            dissolve -= other.GetComponent<Bullet>().GetBulletDamage();

        }

    }
}
