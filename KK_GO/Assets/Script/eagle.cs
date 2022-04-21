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
        upy = upPoint.position.y;//һ��ʼ������ӥ���������ֵ��������
        downy = downpoint.position.y;
        Destroy(upPoint.gameObject);
        Destroy(downpoint.gameObject);
    }

    
    void Update()
    {
        Movement();
    }

    void Movement()//�ƶ�
    {
        if (faceUp)//���Ϸ�
        {
         rb.velocity = new Vector2(rb.velocity.x, speed);
            if(transform.position.y > upy)
            {
                faceUp = false;
            }


        }
        else//���·�
        { 
            rb.velocity = new Vector2(rb.velocity.x, -speed);
            if (transform.position.y < downy)//���xֵ�����ұ߼���
            {
                faceUp = true;
            }

        }
    }


}
