using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.Net.Sockets;

public class feedback : MonoBehaviour
{
    public InputField feedBackBox;

    public void submitFeedback()
    {
        UDPsend("GSM|sessionFeedback|" + feedBackBox.text);
        feedBackBox.transform.position = new Vector3(200, 0, 0);
    }

    public void closeFeedback()
    {
        feedBackBox.transform.position = new Vector3(200, 0, 0);
    }
    public void move()
    {
        feedBackBox.transform.position = new Vector3(0, 0, 0);
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
