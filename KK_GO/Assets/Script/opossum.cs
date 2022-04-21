using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class opossum : Enemy
{
    private Rigidbody2D rb;

    public Transform leftpoint, rightpoint;
    public float speed;

    private float leftx, rightx;
    private bool faceleft = true;
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        leftx = leftpoint.position.x;//һ��ʼ�����������������ֵ��������
        rightx = rightpoint.position.x;
        Destroy(leftpoint.gameObject);
        Destroy(rightpoint.gameObject);
    }


    void Update()
    {
        Movement();
    }

    void Movement()//�ƶ�
    {
        if (faceleft)//������
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            if (transform.position.x < leftx)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                faceleft = false;
            }


        }
        else//������
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            if (transform.position.x > rightx)//���xֵ�����ұ߼���
            {
                transform.localScale = new Vector3(1, 1, 1);
                faceleft = true;
            }

        }

    }
}
