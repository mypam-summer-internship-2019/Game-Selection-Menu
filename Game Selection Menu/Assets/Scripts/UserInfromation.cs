using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.Net.Sockets;
public class UserInfromation : MonoBehaviour
{
    public GameObject userInfo;
    public InputField firstName;
    public InputField seccondName;
    public InputField age;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openUserInfo()
    {
        userInfo.transform.position = new Vector3(0, 0, 0);
    }

    public void closeUserInfo()
    {
        userInfo.transform.position = new Vector3(200, 0, 0);
    }

    public void setUserInfo()
    {
        UDPsend("GSM|firstName|" + firstName.text);
        UDPsend("GSM|seccondName|" + seccondName.text);
        UDPsend("GSM|age|" + age.text);
        userInfo.transform.position = new Vector3(200, 0, 0);
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
