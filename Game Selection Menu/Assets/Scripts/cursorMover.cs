using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using System.Collections.Generic;
using System.Collections;
public class cursorMover : MonoBehaviour
{

    public bool moveNext;
    public static Vector3 moveTo = new Vector3 (-4.4f,1f,0f);
    public static int cursorPos = 1;
    private static float i = 1;
    private bool check = true;
    private static float y = 1;
    public bool lockCursor = false;
    public static float time = 0f;
    private static float opacity = 1f;
    private static float timeElapsed = 0f;

    public static int resetNum = 0;
    public static int moveNum = 0;
    public static int posCounter = 1;
    public static int constCounter = 1;

    void Start()
    {
        transform.position = new Vector3(-4.4f, 1f, 0f);
    }

    void Update()
    {
       
        if ( time >= 1)
        {
            lockCursor = true;
        }

            if (Input.GetKey(KeyCode.Space) && lockCursor == false)
        {
            time += Time.deltaTime;
        } 

        if (Input.GetKeyUp("space") && time < 0.25 && lockCursor == false)
        {

            posCounter += 1;
            time = 0f;
            if ((cursorPos % 3) == 0 && (cursorPos % 6) != 0 && cursorPos != 0)
            {
                i = i - 3;
            }

            if ((cursorPos % 3) == 0)
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

            if (constCounter == gameSpawner.count)
            {
                cursorPos = 0;
                i = 0;
                y = 1f;
                check = false;
                constCounter = 0;
            }

            if (cursorPos == 6)
            {
                cursorPos = 0;
                i = 0;
            }

            if (resetNum == gameSpawner.count)
            {
                resetNum = 0;
                moveNum = 0;
            }

            if (moveNum == 6)
            {
                moveNum = 0;
            }


            moveNext = true;
            moveTo = new Vector3(-4.4f + 4.4f * i, y, 0);
            
            i = i + 1;
            cursorPos += 1;
            constCounter += 1;
            resetNum = resetNum +1;
            moveNum = moveNum +1;
        }

        if (Input.GetKeyUp("space") && lockCursor == false)
        {
            time = 0f;
        }

        if (moveNext == true)
        {
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, (opacity));
            timeElapsed = timeElapsed + Time.deltaTime;
            opacity = opacity - timeElapsed / 2;
        }

        if (opacity <= 0)
        {
            transform.position = moveTo;
            timeElapsed = 0f;
            moveNext = false;
        }

        if (moveNext == false && opacity <= 1)
        {
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, (opacity));
            timeElapsed = timeElapsed + Time.deltaTime;
            opacity = opacity + timeElapsed / 2;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
    }
}
