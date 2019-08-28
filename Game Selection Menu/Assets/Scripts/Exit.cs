using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class Exit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            UDPsend("GSM|resumeMainMenu");
            Application.Quit();
        }
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
