using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frog : Enemy
{
    private Rigidbody2D rb;
    private Collider2D coll;
    public LayerMask Ground;

    public Transform leftpoint,rightpoint;//���Ҽ���
    public float speed,jumpForce;//�����ٶȡ���Ծ����

    private float leftx,rightx;
    private bool faceLeft = true;//�����������Ϊ��
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        leftx = leftpoint.position.x;//һ��ʼ�����������������ֵ��������
        rightx = rightpoint.position.x;
        Destroy(leftpoint.gameObject);
        Destroy(rightpoint.gameObject);
    }

    
    void Update()
    {
        SwitchAnim();

    }

    void Movement()//�ƶ�
    {
        if (faceLeft)//�������
        { 
            if (transform.position.x < leftx)//������ܵ�xλ��С�ڼ���
            {
                transform.localScale = new Vector3(-1, 1, 1);//��ת
                faceLeft = false;
            }
            if (coll.IsTouchingLayers(Ground) && faceLeft) //��������ڵ����ϲ����������
            { 
                anim.SetBool("jumping",true);
            rb.velocity = new Vector2(-speed, jumpForce);
            }
           
            
        }
        else//�����Ҳ�
        { 
            if (transform.position.x > rightx)//���xֵ�����ұ߼���
            {
                transform.localScale = new Vector3(1, 1, 1);//��ת
                faceLeft = true;
            }
            if (coll.IsTouchingLayers(Ground) && !faceLeft)//��������ڵ����ϲ��������ұ�
            {
                anim.SetBool("jumping", true);
                rb.velocity = new Vector2(speed, jumpForce);
            }
           
        }
    }
       

    void SwitchAnim()//����ת��
    {
        if (anim.GetBool("jumping"))//�������ڿ���ʱ
        {
            if(rb.velocity.y < 0.1)//������������״̬
            {
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
            }
        }
        if (coll.IsTouchingLayers(Ground) && anim.GetBool("falling"))//��������ڵ����ϲ��Ҵ�������״̬
        {
            anim.SetBool("falling", false);
        }
    }

    

}
