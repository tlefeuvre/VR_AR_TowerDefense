using UnityEngine;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using TMPro;
using System.Net;
using System.Net.Sockets;

public class NetworkUI : MonoBehaviour
{
/*	[SerializeField] TextMeshProUGUI ipAddressText;
	[SerializeField] TMP_InputField ip;

	void Start()
	{
		ipAddress = "0.0.0.0";
		SetIpAddress(); // Set the Ip to the above address
		pcAssigned = false;
	}

	// To Host a game
	public void StartHost()
	{
		NetworkManager.Singleton.StartHost();
		GetLocalIPAddress();
	}

	// To Join a game
	public void StartClient()
	{
		ipAddress = ip.text;
		SetIpAddress();
		NetworkManager.Singleton.StartClient();
	}

	// ONLY FOR HOST SIDE 
	public string GetLocalIPAddress()
	{
		var host = Dns.GetHostEntry(Dns.GetHostName());
		foreach (var ip in host.AddressList)
		{
			if (ip.AddressFamily == AddressFamily.InterNetwork)
			{
				ipAddressText.text = ip.ToString();
				ipAddress = ip.ToString();
				return ip.ToString();
			}
		}
		throw new System.Exception("No network adapters with an IPv4 address in the system!");
	}

	*//* Sets the Ip Address of the Connection Data in Unity Transport
	to the Ip Address which was input in the Input Field *//*
	// ONLY FOR CLIENT SIDE
	public void SetIpAddress()
	{
		transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
		transport.ConnectionData.Address = ipAddress;
	}*/
}
