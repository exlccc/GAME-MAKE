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
                faceleft = false;
            }

        }
        else//������
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            if (transform.position.x > rightx)//���xֵ�����ұ߼���
            {
                faceleft = true;
            }

        }

    }

}
