using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

public class UDP_Handling : MonoBehaviour
{
    public static GameObject instance;
    

    public static string[] gameDirectories;
    public static float yPos = 0f;
    public static float zPos = 0f;
    public static string gameFolderDir;

    public static float shoulderAngle = 0f;
    public static float elbowAngle = 0f;

    public static string receivedData = "";
    public static string sendData = "";

    public static bool spawn = false;

    void Start()
    {
    }

    void Awake()
    {   
        if (instance == null)
        {
            instance = gameObject;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != gameObject)
        {
            Destroy(gameObject);
        }
        UDPsend("GSM|gameSelectorMenuStarted");
        getData();
    }

    void Update()
    {

    }

    private void getData()
    {
        string data = listenForData();
        string[] spiltData = data.Split('|');
        switch (spiltData[0])
        {
            case "MM":
                switch (spiltData[1])
                {
                   case "gameFolderDir":
                        gameFolderDir = spiltData[2];
                        spawn = true;
                   break;
                }
            break;           
        }
    }

    public string listenForData()
    {

        UdpClient listener = new UdpClient(1800);
        IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, 1800);
        byte[] bytes = listener.Receive(ref groupEP);
        string message = $"{Encoding.ASCII.GetString(bytes, 0, bytes.Length)}";
        receivedData = message;
        listener.Close();
        return (message);
    }
    private static void UDPsend(string datagram)
    {
        byte[] data = System.Text.ASCIIEncoding.ASCII.GetBytes(datagram);
        string IP = "127.0.0.1";
        IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(IP), 1400);
        Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        client.SendTo(data, endPoint);
    }
}
