using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upgradeShop : MonoBehaviour
{
    public int nb = 0;
    public GameObject textSkill;
    public GameObject textCost;
    public GameObject textNotif;
    public PlayerInterface m_PlayerInterface;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Upgrade()
    {
        if (m_PlayerInterface.Coins - 50 * (nb + 1) >= 0)
        {
            nb++;
            textSkill.GetComponent<TMPro.TextMeshProUGUI>().text = nb.ToString();
            textCost.GetComponent<TMPro.TextMeshProUGUI>().text = (50 * (nb + 1)).ToString();
            m_PlayerInterface.Coins = m_PlayerInterface.Coins - (50 * nb);
        }
        else
        {
           // GameObject.Instantiate(textNotif, transform.position, transform.rotation);
            GameObject relay = Instantiate(textNotif, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            relay.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
            Debug.Log("Not Enough Money !");
        }
    }
}
