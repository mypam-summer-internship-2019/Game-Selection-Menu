using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class userSelection : MonoBehaviour
{
    private bool selected = false;

    public bool restOnce = true;
    public bool moveOnce = true;

    public int constCounter = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (cursorMover.resetNum == gameSpawner.count)
        {
            if (restOnce == true)
            {
                Debug.Log("Reset");
                transform.position = transform.position + new Vector3(4.4f*(gameSpawner.count/6)*3,0f,0f);
                restOnce = false;
            }
        }

        else
        {
            restOnce = true;
        }

        if ((cursorMover.moveNum) == 6)
        {
            if (moveOnce == true)
            {
                Debug.Log("Move");
                transform.position = transform.position - new Vector3(4.4f*3, 0f, 0f);
                moveOnce = false;
            }
        }

        else
        {
            moveOnce = true;
        }


        if (cursorMover.time >= 1f)
        {
            if (selected == false)
            {
                GetComponent<Collider2D>().enabled = false;
                transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            }
            else
            {
                UDPsend("GSM|gameSelected|" + this.name);
                Thread.Sleep(1000);
                Application.Quit();
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        selected = true;
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        selected = false;
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
