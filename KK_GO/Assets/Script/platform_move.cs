using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platform_move : MonoBehaviour
{
    private Rigidbody2D rb;

    public Transform leftpoint, rightpoint;
    public float speed;

    private float leftx, rightx;
    private bool faceleft = true;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        leftx = leftpoint.position.x;//一开始便获得老鼠左右两点的值，并销毁
        rightx = rightpoint.position.x;
        Destroy(leftpoint.gameObject);
        Destroy(rightpoint.gameObject);
    }

  
    void Update()
    {
        Movement();
    }

    void Movement()//移动
    {
        if (faceleft)//面向左
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            if (transform.position.x < leftx)
            {
                faceleft = false;
            }

        }
        else//面向右
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            if (transform.position.x > rightx)//如果x值大于右边检查点
            {
                faceleft = true;
            }

        }

    }

}
