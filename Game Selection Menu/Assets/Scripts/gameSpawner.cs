using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class gameSpawner : MonoBehaviour
{

    public RawImage gamePic;
    public Text gameName;
    public GameObject canvas;
    public GameObject nameBackground;
    public static int count = 0;
    void Start()
    {
        
    }

    void Update()
    {
        if (UDP_Handling.spawn == true)
        {
            spawn();
        }
    }

    private void spawn()
    {
        float i = 0;
        float y = 0;
        bool check = true;
        string[] gameTypeDirs = Directory.GetDirectories(UDP_Handling.gameFolderDir+ @"\games");
        string[] games;

        foreach (string dir in gameTypeDirs)
        {
            
            string currentType = Path.GetFileName(dir);
            if (currentType != "screenshots")
            {
                games = Directory.GetFiles(dir);
                foreach (string game in games)
                {
                    string currentGame = Path.GetFileName(game);
                    if ((count % 3) == 0 && (count % 6) != 0 && count != 0)
                    {
                        i = i - 3;
                    }

                    if ((count % 3) == 0)
                    {
                        if (check)
                        {
                            y = 1f;
                        }
                        else
                        {
                            y = -2.5f;
                        }
                        check = !check;
                    }

                    var newPic = Instantiate(gamePic, new Vector3(-4.4f + 4.4f * i, y, 0), Quaternion.Euler(0, 0, 0));
                    newPic.transform.parent = canvas.transform;
                    byte[] byteArray;
                    try
                    {
                        byteArray = File.ReadAllBytes(UDP_Handling.gameFolderDir + @"\games\screenshots\" + currentGame.Substring(0, currentGame.Length - 4) + " screenshot.png");
                    }
                    catch
                    {
                        byteArray = File.ReadAllBytes(UDP_Handling.gameFolderDir + @"\games\screenshots\" + currentGame.Substring(0, currentGame.Length - 4) + " screenshot.jpg");
                    }
                    
                    Texture2D sampleTexture = new Texture2D(3, 2);
                    bool loaded = sampleTexture.LoadImage(byteArray);
                    if (loaded)
                    {
                        newPic.GetComponent<RawImage>().texture = sampleTexture;
                    }

                    var newName = Instantiate(gameName, canvas.transform, false);
                    newName.transform.parent = newPic.transform;
                    newName.transform.position = newPic.transform.position + new Vector3(0f, 1.6f, 0f);
                    newName.text = currentGame.Substring(0, currentGame.Length - 8);
                    var textBackground = Instantiate(nameBackground, newName.transform, false);
                    textBackground.transform.position = newName.transform.position + new Vector3(0f, 0.07f, 0f); ;
                    newPic.name = game +"-"+ currentType;

                    i = i + 1;
                    count = count + 1;
                }
            }
        }
        UDP_Handling.spawn = false;
    }
}
