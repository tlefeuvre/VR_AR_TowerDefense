using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;
using TMPro;
using Unity.Netcode;

[System.Serializable]
public class Buildings
{
    public List<GameObject> buildings;
}

[System.Serializable]
public class Magic
{
    public List<GameObject> magics;
}
public class ConstructManager : MonoBehaviour //NetworkBehaviour
{
    [Header("GameMod Manager")]
    [SerializeField] private int currentGameModIndex;
    public GameObject RightHandGameObject;
    [SerializeField] private TMP_Dropdown dropdown;

    [Header("Class Manager")]
    [SerializeField] int currentClassIndex;
    [SerializeField] private GameObject skill;   

    [Header("Construct Manager")]
    [SerializeField] int currentBuildingIndex;
    [SerializeField] GameObject currentSpawner;
    [SerializeField] bool isConstructAvailable;
    public List<Buildings> classBuildings = new List<Buildings>();

    [Header("Magic Manager")]
    [SerializeField] int currentMagicIndex;
    [SerializeField] private bool isCasting = false;
    [SerializeField] private GameObject magicSpawner;
    public List<Magic> classMagics = new List<Magic>();

    private InputDevice targetDeviceRight;
    private InputDevice targetDeviceLeft;
    private bool isControllerRightFound = false;
    private bool isControllerLeftFound = false;
    private float constructCD;

    #region general

    void Start()
    {
        currentClassIndex = 0;
        currentBuildingIndex = 0;
        constructCD = 3.0f;
        isConstructAvailable = true;

        GetControllers();

    }
    void Update()
    {
        currentGameModIndex = dropdown.value;

        if (!isControllerRightFound || !isControllerLeftFound)
            GetControllers();

        if (isControllerRightFound && isControllerLeftFound)
        {

            if (currentGameModIndex == 0)
            {
                if (targetDeviceRight.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue) && primaryButtonValue && isConstructAvailable)
                {
                    isConstructAvailable = false;
                    ConstructBuilding();
                    StartCoroutine("ConstructCD");
                    Debug.Log("primary button pressed");

                }

                targetDeviceLeft.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue);
                if (triggerValue > 0.3f)
                {
                    //GetComponent<CanvasController>().SetCanvas(true);
                    Debug.Log(triggerValue);

                }
                else
                {
                    //GetComponent<CanvasController>().SetCanvas(false);

                }
            }
            if (currentGameModIndex == 1)
            {
                targetDeviceRight.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue);
                if (triggerValue > 0.3f)
                {
                    if (!isCasting)
                    {
                        skill = Instantiate(classMagics[currentClassIndex].magics[currentMagicIndex]); //TODO
                        //SPAWN SKILL ON NETWORK
                        //skill.GetComponent<NetworkObject>().Spawn(true);

                        //skill.transform.SetParent(RightHandGameObject.transform);
                        isCasting = true;

                    }
                    if (isCasting && skill)
                    {
                        skill.transform.position = RightHandGameObject.transform.position;
                        skill.transform.rotation = RightHandGameObject.transform.rotation;
                    }
                    //Debug.Log(triggerValue);

                }
                else
                {
                    if (isCasting)
                    {
                        isCasting = false;
                        skill.transform.SetParent(null);

                        skill.GetComponent<Rigidbody>().velocity = skill.transform.forward * 20;
                    }

                }
            }

        }
    }
    private void GetControllers()
    {

        List<UnityEngine.XR.InputDevice> devices = new List<UnityEngine.XR.InputDevice>();
        InputDeviceCharacteristics righControllerCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDeviceCharacteristics leftControllerCharacteristics = InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(righControllerCharacteristics, devices);

        if (devices.Count > 0)
        {
            targetDeviceRight = devices[0];
            isControllerRightFound = true;
        }
        InputDevices.GetDevicesWithCharacteristics(leftControllerCharacteristics, devices);
        if (devices.Count > 0)
        {
            targetDeviceLeft = devices[0];
            isControllerLeftFound = true;
        }

    }
    public int GetgameMod()
    {
        return currentGameModIndex;
    }
    public int GetClass()
    {
        return currentClassIndex;
    }
    public void SetClass(int newClassIndex)
    {
        currentClassIndex = newClassIndex;
        currentBuildingIndex = 0;
        return;
    }
    #endregion

    #region ConstructManager
   

    public void SetBuilding(int newBuildingIndex)
    {
        currentBuildingIndex = newBuildingIndex;
        return;
    }
   
    public void OnSpawnerEnter(GameObject spawn)
    {
        currentSpawner = spawn;
        Debug.Log("enterspawn");
    }
    public void OnSpawnerExit()
    {
        Debug.Log("exitspawn");

        currentSpawner = null;
    }

    private void ConstructBuilding()
    {
        Debug.Log("Construct building 0 ");

        if (classBuildings.Count >= currentClassIndex && currentSpawner)
        {
            Debug.Log("Construct building 2");

            GameObject newObj = Instantiate(classBuildings[currentClassIndex].buildings[currentBuildingIndex],currentSpawner.transform.position,currentSpawner.transform.rotation); //TODO
            newObj.transform.parent = currentSpawner.transform;
        }
    }

     private IEnumerator ConstructCD()
    {
        
        yield return new WaitForSeconds(constructCD);
        isConstructAvailable = true;
       // print("WaitAndPrint ");
        
    }
    #endregion

    #region MagicManager
   
    public void SetMagic(int newMagicIndex)
    {
        currentMagicIndex = newMagicIndex;
        return;
    }

    #endregion
}
