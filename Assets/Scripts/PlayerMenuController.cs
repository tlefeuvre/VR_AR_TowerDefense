using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMenuController : MonoBehaviour
{
    public static PlayerMenuController Instance { get; private set; }

    [SerializeField] private TMP_Dropdown dropdown;
    [SerializeField] private int gameModeId;

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
   

    // Update is called once per frame
    void Update()
    {
        gameModeId = dropdown.value;
    }
    
    public int GetGamemode()
    {
        return gameModeId;
    }
}
