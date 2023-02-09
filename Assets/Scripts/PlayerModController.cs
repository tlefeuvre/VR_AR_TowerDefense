using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR;

public class PlayerModController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject rightHandBase;
    public GameObject rightHandConstruct;
    public GameObject rightHandMagic;

 

    [SerializeField] private int gameModeId = 0;
 
    //private InputDevice right;
    void Start()
    {
        gameModeId = GetComponent<ConstructManager>().GetgameMod();

    }

   
    // Update is called once per frame
    void Update()
    {
        gameModeId = GetComponent<ConstructManager>().GetgameMod();


        if (gameModeId == 0)
        {
            rightHandConstruct.SetActive(true);
            rightHandBase.SetActive(false);
            rightHandMagic.SetActive(false);

            //instantiate construct bullets

           

        }
         if(gameModeId == 1)
        {
            CancelInvoke();

            rightHandMagic.SetActive(true);
            rightHandConstruct.SetActive(false);
            rightHandBase.SetActive(false);
        }
         if (gameModeId == 2)
        {
            CancelInvoke();

            rightHandBase.SetActive(true);
            rightHandConstruct.SetActive(false);
            rightHandMagic.SetActive(false);
        }
    }

  
}
