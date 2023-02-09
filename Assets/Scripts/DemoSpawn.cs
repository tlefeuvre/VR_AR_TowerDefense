using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoSpawn : MonoBehaviour
{
    public List<GameObject> mobs;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn()
    {
        for(int i = 0; i < mobs.Count; i++)
        {
            mobs[i].SetActive(true);
        }
    }
}
