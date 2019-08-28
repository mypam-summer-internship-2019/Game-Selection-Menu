using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slectionOpacity : MonoBehaviour
{
    void Start()
    {
        this.transform.position = new Vector3(200f, 0, 0);
    }
        void Update()
    {
        transform.position = cursorMover.moveTo;
        GetComponent<SpriteRenderer> ().color = new Color(1f, 1f, 1f, (cursorMover.time-0.1f));
    }
}
