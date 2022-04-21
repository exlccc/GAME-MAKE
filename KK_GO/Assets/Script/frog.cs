using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frog : Enemy
{
    private Rigidbody2D rb;
    private Collider2D coll;
    public LayerMask Ground;

    public Transform leftpoint,rightpoint;//左右检查点
    public float speed,jumpForce;//定义速度、跳跃力量

    private float leftx,rightx;
    private bool faceLeft = true;//青蛙面向左边为真
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        leftx = leftpoint.position.x;//一开始便获得青蛙左右两点的值，并销毁
        rightx = rightpoint.position.x;
        Destroy(leftpoint.gameObject);
        Destroy(rightpoint.gameObject);
    }

    
    void Update()
    {
        SwitchAnim();

    }

    void Movement()//移动
    {
        if (faceLeft)//面向左侧
        { 
            if (transform.position.x < leftx)//如果青蛙的x位置小于检查点
            {
                transform.localScale = new Vector3(-1, 1, 1);//翻转
                faceLeft = false;
            }
            if (coll.IsTouchingLayers(Ground) && faceLeft) //如果青蛙在地面上并且面向左边
            { 
                anim.SetBool("jumping",true);
            rb.velocity = new Vector2(-speed, jumpForce);
            }
           
            
        }
        else//面向右侧
        { 
            if (transform.position.x > rightx)//如果x值大于右边检查点
            {
                transform.localScale = new Vector3(1, 1, 1);//翻转
                faceLeft = true;
            }
            if (coll.IsTouchingLayers(Ground) && !faceLeft)//如果青蛙在地面上并且面向右边
            {
                anim.SetBool("jumping", true);
                rb.velocity = new Vector2(speed, jumpForce);
            }
           
        }
    }
       

    void SwitchAnim()//动画转换
    {
        if (anim.GetBool("jumping"))//当青蛙在空中时
        {
            if(rb.velocity.y < 0.1)//当青蛙在下落状态
            {
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
            }
        }
        if (coll.IsTouchingLayers(Ground) && anim.GetBool("falling"))//如果青蛙在地面上并且处于下落状态
        {
            anim.SetBool("falling", false);
        }
    }

    

}
