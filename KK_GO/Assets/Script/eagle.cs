using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eagle : Enemy
{
    private Rigidbody2D rb;

    public Transform upPoint, downpoint;
    public float speed;

    private float upy, downy;
    private bool faceUp = true;


    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        upy = upPoint.position.y;//一开始便获得老鹰左右两点的值，并销毁
        downy = downpoint.position.y;
        Destroy(upPoint.gameObject);
        Destroy(downpoint.gameObject);
    }

    
    void Update()
    {
        Movement();
    }

    void Movement()//移动
    {
        if (faceUp)//向上飞
        {
         rb.velocity = new Vector2(rb.velocity.x, speed);
            if(transform.position.y > upy)
            {
                faceUp = false;
            }


        }
        else//向下飞
        { 
            rb.velocity = new Vector2(rb.velocity.x, -speed);
            if (transform.position.y < downy)//如果x值大于右边检查点
            {
                faceUp = true;
            }

        }
    }


}
