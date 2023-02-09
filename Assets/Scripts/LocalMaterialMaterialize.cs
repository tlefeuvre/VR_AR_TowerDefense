using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalMaterialMaterialize : MonoBehaviour
{
    public Shader shader;
    public Color color;
    // Start is called before the first frame update
    void Start()
    {
        
        Renderer rend = GetComponent<Renderer>();
        rend.material = new Material(shader);
        rend.material.SetColor("_MainColor", color);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
