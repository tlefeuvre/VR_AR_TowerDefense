using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR;

public class PlayerNetwork : NetworkBehaviour
{
    [SerializeField] private Transform mobPrefab; //change to list when their will be some differents
    [SerializeField] private Transform hersePrefab; //change to list when their will be some differents
    private List<Transform> mobList = new List<Transform>();
    [SerializeField] bool isPlacingHerse = false;
    [SerializeField] Quaternion herseRotation = Quaternion.identity;

    [SerializeField] float moveSpeed;
    [SerializeField] float rotateSpeed;

    [SerializeField] GameObject canvaPlayer;

    [SerializeField] GameObject cam;

    bool SelectMod = false;

    [SerializeField] int mobselected = -1;
    List<InputDevice> inputDevices = new List<InputDevice>();

    //private NavMeshSurface surface;

    /*    private NetworkVariable<MyCustomData> newValue = new NetworkVariable<MyCustomData>(
            new MyCustomData { 
                _int= 75,
                _bool = false, 
                _string = "nono",
            }, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

        public struct MyCustomData : INetworkSerializable
        {
            public int _int;
            public bool _bool;
            public FixedString128Bytes _string;

            public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
            {
                serializer.SerializeValue(ref _int);
                serializer.SerializeValue(ref _bool);
                serializer.SerializeValue(ref _string);
            }
        }

        public override void OnNetworkSpawn()
        {
            newValue.OnValueChanged += (MyCustomData previousValue, MyCustomData newValue) => {
                Debug.Log(OwnerClientId + "; " + newValue._int + "; " + newValue._bool + "; "+ newValue._string);
            };
        }*/

    private void Start()
    {
        InputDevices.GetDevices(inputDevices);

        cam = GetComponentInChildren<Camera>().gameObject;
        cam.SetActive(false);

        canvaPlayer.SetActive(false);

        if (!IsOwner)
            return;

        if (inputDevices.Count == 0)
        {
            cam.SetActive(true);
        }
    }

    void Update()
    {
        if (!IsOwner) return;

        if (Input.GetKeyDown(KeyCode.Y))
        {
            if (mobList[0])
                Destroy(mobList[0]);
        }

        Vector3 InputVector = new Vector3(0, 0, 0);

        if (!SelectMod)
            transform.eulerAngles += new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0) * Time.deltaTime * rotateSpeed;

        if (Input.GetKey(KeyCode.Z))
        {
            InputVector += transform.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            InputVector -= transform.forward;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            InputVector -= transform.right;
        }
        if (Input.GetKey(KeyCode.D))
        {
            InputVector += transform.right;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            isPlacingHerse = isPlacingHerse ? false : true;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && !SelectMod && mobselected != -1)
        {
            RaycastHit hit;
            Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity);

            if (hit.collider.transform.tag == "MobSpawn")
            {
                SpawnMobsServerRPC(hit.point, mobselected);
            }
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            SpawnMobsServerRPC(GameManager.Instance.GetSpawnPointList()[Random.Range(0, 3)].position, -1);

        }
        if (Input.GetKey(KeyCode.Escape) && inputDevices.Count == 0)
        {
            canvaPlayer.SetActive(true);
            //Lock mouse and set it not visible
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            SelectMod = true;
        }

        if (isPlacingHerse)
        {
            herseRotation.eulerAngles = new Vector3(0, ((int)herseRotation.eulerAngles.y + (int)Input.mouseScrollDelta.y), 0); //marche po

            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
                {
                    Transform herse = Instantiate(hersePrefab, hit.point, herseRotation, transform.parent);
                    herse.GetComponent<NetworkObject>().Spawn(true);
                    //surface.BuildNavMesh();
                }
            }
        }

        if (!SelectMod)
            transform.localPosition += InputVector * moveSpeed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }

    [ServerRpc]
    public void SpawnMobsServerRPC(Vector3 spawnPosition, int mobselectedinServer)
    {
        //Debug.Log("TestServerRPC : " + OwnerClientId + " ; " + serverRpcParams.Receive.SenderClientId);

        //SPAWN MOB AND INIT HIS INITIAL POSITION
        int mobid = mobselectedinServer;
        if (mobselectedinServer == -1)
            mobid = Random.Range(0, 5);


        Transform spawnedObjectTransform = Instantiate(GameManager.Instance.GetMobSelected(mobid), spawnPosition, Quaternion.identity, transform.parent);
        spawnedObjectTransform.GetComponent<NetworkObject>().Spawn(true);

        //INIT HIS DESTINATION
        //spawnedObjectTransform.position = spawnPosition;
        SetDestinationToPosition(spawnedObjectTransform, GameManager.Instance.GetCastlePosition());

        mobList.Add(spawnedObjectTransform);
    }

    public Vector3 GetRandomSpawnPoint(List<Transform> spawnPoints)
    {
        int rand = Random.Range(0, 3);
        return spawnPoints[rand].position;
    }

    public void SetDestinationToPosition(Transform mob, Vector3 destination)
    {
        NavMeshAgent mobAgent = mob.GetComponent<NavMeshAgent>();
        if (mobAgent != null)
            mobAgent.SetDestination(destination);
    }

    [ClientRpc]
    public void TestClientRPC(ClientRpcParams clientRpcParams)
    {
        Debug.Log("TestServerRPC : " + OwnerClientId + " ; " + clientRpcParams.Send.TargetClientIds);
    }

    public void BackToGodMod()
    {
        if (!IsOwner) return;

        //Lock mouse and set it not visible
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        canvaPlayer.SetActive(false);

        SelectMod = false;
        mobselected = canvaPlayer.GetComponent<PlayerInterface>().mobSelection;
    }
}
