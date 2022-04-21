using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform cam;
    public float moveRate;
    private float startPointX,startPointY;
    public bool localY;
  
    void Start()
    {
        startPointX = transform.position.x;
        startPointY = transform.position.y;
    }

  
    void Update()
    {
        if (!localY)
        {
            transform.position = new Vector2(startPointX + cam.position.x * moveRate, transform.position.y);
        }
        else
        {
            transform.position = new Vector2(startPointX + cam.position.x * moveRate,startPointY + cam.position.y * moveRate);
        }
    }
}
