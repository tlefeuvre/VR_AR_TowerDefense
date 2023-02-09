using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using TMPro;
using UnityEngine.EventSystems;

public class ARCursor : MonoBehaviour
{
    public GameObject cursorChildObject;
    public List<GameObject> mobToPlace;
    public ARRaycastManager raycastManager;
    [SerializeField] public GameObject ARPlaneObject;
    [SerializeField] private PlayerInterface m_PlayerInterface;
    [SerializeField] private List<GameObject> UIComponents;
    [SerializeField] private GameObject MobCursorPrefab;
    private GameObject MobCursor;

    public Camera arCam;

    public bool useCursor = true;

    private bool MapSpawned;
    [SerializeField] private GameObject Map;

    void Start()
    {
        Screen.sleepTimeout = (int)0f;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        cursorChildObject.SetActive(useCursor);
        MapSpawned = false;
        //MobCursor.SetActive(false);
    }

    void Update()
    {
        if (useCursor)
        {
            UpdateCursor();
        }
        /*if (MapSpawned)
        {
            RaycastHit hit;
            Ray ray = arCam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.tag == "Terrain")
                {
                    GameObject.Instantiate(MobCursor, transform.position, transform.rotation);
                }
            }
        }*/
        /*if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (useCursor)
            {
                if (m_PlayerInterface.Coins - PlayerPrefs.GetInt("costOfMob") >= 0)
                {
                    m_PlayerInterface.Coins = m_PlayerInterface.Coins - PlayerPrefs.GetInt("costOfMob");
                    GameObject.Instantiate(mobToPlace[PlayerPrefs.GetInt("typeOfMob")], transform.position, transform.rotation);
                }
                else
                    Debug.Log("Not Enough Money !");
            }
            else
            {
                if (!MapSpawned)
                {
                    List<ARRaycastHit> hits = new List<ARRaycastHit>();
                    raycastManager.Raycast(Input.GetTouch(0).position, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);
                    if (hits.Count > 0)
                    {
                        //GameObject.Instantiate(Map, hits[0].pose.position + new Vector3(-2.562f, 0, -2.223f), hits[0].pose.rotation);
                        GameObject.Instantiate(Map, hits[0].pose.position/* + new Vector3(-1.145f, 0, -1.145f)*//*, hits[0].pose.rotation);

                        ARPlaneObject.GetComponent<ARPlaneManager>().requestedDetectionMode = 0;

                        MapSpawned = true;
                    }
                }
                else
                {
                    RaycastHit hit;
                    Ray ray = arCam.ScreenPointToRay(Input.mousePosition);
                    //if (Physics.Raycast(arCam.transform.position, arCam.transform.forward, out hit, Mathf.Infinity))
                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.collider.gameObject.tag == "Terrain")
                        {
                            //GameObject.Instantiate(mobToPlace[PlayerPrefs.GetInt("typeOfMob")], hit.point, transform.rotation);
                            if (m_PlayerInterface.Coins - PlayerPrefs.GetInt("costOfMob") >= 0)
                            {
                                m_PlayerInterface.Coins = m_PlayerInterface.Coins - PlayerPrefs.GetInt("costOfMob");
                                GameObject.Instantiate(mobToPlace[PlayerPrefs.GetInt("typeOfMob")], transform.position, transform.rotation);
                            }
                            else
                                Debug.Log("Not Enough Money !");
                        }
                    }
                }
            }
        }*/
    }

    public void SpawnMap()
    {
        if(!MapSpawned)
        {
            List<ARRaycastHit> hits = new List<ARRaycastHit>();
            raycastManager.Raycast(arCam.ViewportToScreenPoint(new Vector2(0.5f, 0.5f)), hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);
            //raycastManager.Raycast(new Vector2 (arCam.scaledPixelWidth / 2, arCam.scaledPixelHeight / 2), hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);
            if (hits.Count > 0)
            {
                GameObject.Instantiate(Map, hits[0].pose.position, hits[0].pose.rotation);

                ARPlaneObject.GetComponent<ARPlaneManager>().requestedDetectionMode = 0;
                MapSpawned = true;
                foreach (GameObject UIComponent in UIComponents)
                {
                    UIComponent.SetActive(true);
                }
                UIComponents[0].SetActive(false);
                MobCursor = GameObject.Instantiate(MobCursorPrefab, hits[0].pose.position, hits[0].pose.rotation);
                cursorChildObject.SetActive(false);

            }
        }
    }

    public void SpawnMob(int ID)
    {
        RaycastHit hit;
        Ray ray = arCam.ScreenPointToRay(arCam.ViewportToScreenPoint(new Vector2(0.5f, 0.5f)));
        //Ray ray = arCam.ScreenPointToRay(new Vector2(arCam.scaledPixelWidth / 2, arCam.scaledPixelHeight / 2));
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.tag == "Terrain")
            {
                if (m_PlayerInterface.Coins - PlayerPrefs.GetInt("costOfMob") >= 0)
                {
                    m_PlayerInterface.Coins = m_PlayerInterface.Coins - PlayerPrefs.GetInt("costOfMob");
                    GameObject.Instantiate(mobToPlace[PlayerPrefs.GetInt("typeOfMob")], hit.point, hit.transform.rotation);
                }
                else
                    Debug.Log("Not Enough Money !");
            }
        }
    }

    void UpdateCursor()
    {
        if (!MapSpawned)
        {
            Vector2 screenPosition = arCam.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));
            List<ARRaycastHit> hits = new List<ARRaycastHit>();
            raycastManager.Raycast(screenPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);

            if (hits.Count > 0)
            {
                transform.position = hits[0].pose.position;
                transform.rotation = hits[0].pose.rotation;
            }
        }
        else
        {
            RaycastHit hit;
            Ray ray = arCam.ScreenPointToRay(arCam.ViewportToScreenPoint(new Vector2(0.5f, 0.5f)));
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.tag == "Terrain")
                {
                    MobCursor.transform.position = hit.point;
                    MobCursor.transform.eulerAngles = hit.normal;
                }
            }
        }
    }
}