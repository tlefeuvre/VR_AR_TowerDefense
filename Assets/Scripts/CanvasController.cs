using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class skills
{
    public List<GameObject> List;
}
public class CanvasController : MonoBehaviour
{
    [SerializeField] private GameObject canvas;


    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject classesMenu;
    [SerializeField] private List<skills> canvasMenu = new List<skills>();
    // Start is called before the first frame update
    void Start()
    {
        Exit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Trigger(int skill)
    {
        if (skill == -1)
        {
            mainMenu.SetActive(false);
            classesMenu.SetActive(true);
        }
        else
        {
            int classe = GetComponent<ConstructManager>().GetClass();
            Debug.Log("classe :" + classe + " skill: " + skill);
            mainMenu.SetActive(false);
            canvasMenu[classe].List[skill].SetActive(true);
        }
       
    }
    public void SetCanvas(bool isActive)
    {
        canvas.SetActive(isActive);
    }
    public void SetClass(int newClassIndex)
    {
        GetComponent<ConstructManager>().SetClass(newClassIndex);

        Exit();

    }
    public void SetMagic(int newMagicIndex)
    {

        GetComponent<ConstructManager>().SetMagic(newMagicIndex);
    }
    public void SetBuilding(int newBuildingIndex)
    {

        GetComponent<ConstructManager>().SetBuilding(newBuildingIndex);
    }
    public void Exit()
    {
        mainMenu.SetActive(true);
        classesMenu.SetActive(false);
        for(int i = 0; i < canvasMenu.Count; i++)
        {
            for (int j = 0; j < canvasMenu[i].List.Count; j++)
                canvasMenu[i].List[j].SetActive(false);
        }
    }
}
