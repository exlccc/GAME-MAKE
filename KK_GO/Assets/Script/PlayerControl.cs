using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    private Collider2D coll;
    private Rigidbody2D rb;
    private Animator anim;
    private bool isHurt,CHP,hjump;

    public float speed, jumpForce,reboundDistance;
    public Transform belowCheck;
    public LayerMask ground,Enemy;
    public bool isGround, isJump;
    public Text CherryNum,GenNum;
    public AudioSource jumpAudio,hurtAudio,gemAudio,cherryAudio,fallAudio;
    public Collider2D Discoll;
    public Transform cellCheck;
    public int HP = 5;//����ֵ
    public GameObject GAMEGVER_menu;//�����˵�
    public GameObject heartimage1, heartimage2, heartimage3, heartimage4, heartimage5;//��Ӧ����ֵͼ��
   
    public int Gem = 0;    
    public int destroy_Enemy = 0;
    bool jumpPressed;
    int jumpCount = 1;
    

    void Start()
    {
       rb = GetComponent<Rigidbody2D>();
       coll = GetComponent<Collider2D>();
       anim = GetComponent<Animator>();
    }

    
    void Update()
    {
        Crouch();
        if (Input.GetButtonDown("Jump") && jumpCount > 0)//��������Ծ��������Ծ��������0
        {
            jumpPressed = true;
        }
    }

    private void FixedUpdate()
    {
       
        if (!isHurt)//��û������ʱ
        {
            GroundMovement();
        }
        

        isGround = Physics2D.OverlapCircle(belowCheck.position, 0.1f, ground);//���Ƿ��ڵ�����
        
        Jump();
        SwitchAnim();
        Heart();
    }


   
     void GroundMovement() //��ɫ�ƶ�
    {
        //ˮƽ�ƶ�
        float horizontalMove = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2 (horizontalMove * speed, rb.velocity.y);
        
        //��ת
        if(horizontalMove != 0)
        {
            transform.localScale = new Vector3(horizontalMove,1, 1);
        }
        
    }


        void Jump() //��ɫ��Ծ
    {
        if (isGround)//���ڵ�����
        {
            if (hjump)
            {
                jumpCount = 2;
                isJump = false;
            }
            else
            {
                jumpCount = 1;
                isJump = false;
            }
        }
        if(jumpPressed && jumpCount > 0)//��������Ծ�����ڵ�����
        {
            jumpAudio.Play();
            isJump = true;
            rb.velocity = new Vector2 (rb.velocity.x, jumpForce);
            jumpCount--;
            jumpPressed = false;
        }
      
    }

     void SwitchAnim() //����ת��
    {
        anim.SetFloat("running",Mathf.Abs(rb.velocity.x));//��ɫˮƽ�ƶ�����
        
        if (isGround)//���ڵ�����ʱ
        {
            anim.SetBool("falling",false);
        }
        else if(!isGround && rb.velocity.y > 0)//�����ڵ����ϲ��������ϵ��ٶ�ʱ
        {
            anim.SetBool("jumping",true);
        }
        else if (rb.velocity.y < 0)//�������µ��ٶ�ʱ
        {
            anim.SetBool("jumping",false);
            anim.SetBool("falling",true);
        }

        if (isHurt)//������ʱ
        {
            anim.SetBool("hurt", true);
            if (Mathf.Abs(rb.velocity.x) < 0.1f)//�������ľ���С��0.1ʱ
            {    
             isHurt = false;
                anim.SetBool("hurt", false);
            }
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) //��ײ������
    {//�ռ���Ʒ
        if(collision.tag == "Collection_Cherry")
        {
            cherryAudio.Play();
            Destroy(collision.gameObject);
            HP++;
        }else
        if(collision.tag == "Collection_Gen")
        {
            gemAudio.Play();
            Destroy(collision.gameObject);
            Gem += 1;
            GenNum.text = Gem.ToString();
        }
        else
        if (collision.tag == "Collection_apple")
        {
            gemAudio.Play();
            Destroy(collision.gameObject);
            hjump = true;
        }

        //��������
        if (collision.tag == "DeadLine")
        {
            HP = 0;
            GetComponent<AudioSource>().enabled = false;
            fallAudio.Play();
            Invoke("Restart", 3f);
        }
    }

    
    private void OnCollisionEnter2D(Collision2D collision)//��ײ�����
    {
        
        if (collision.gameObject.tag == "Enemy")
        {//�������
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();//����Enemy��
            if(Physics2D.OverlapCircle(belowCheck.position, 0.2f, Enemy)) //�ж�groundCheck�Ƿ�Ӵ�����
            {
                destroy_Enemy++;
                enemy.Jumpon();//���������෽��
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
            //���˶��������˺�
            else if (transform.position.x < collision.gameObject.transform.position.x)//�������ڵ��˵����
            {
                CHP = true;
                hurtAudio.Play();
                rb.velocity = new Vector2(-reboundDistance,rb.velocity.y);
                isHurt = true;
           
            }
            else if (transform.position.x > collision.gameObject.transform.position.x)//�������ڵ��˵��ұ�
            {
                CHP = true;
                hurtAudio.Play();
                rb.velocity = new Vector2(reboundDistance,rb.velocity.y);
                isHurt = true;
            }

        }
    }

    void Crouch()//����
    {
        if (!Physics2D.OverlapCircle(cellCheck.position,0.2f,ground)) 
        { 
        if (Input.GetButton("Crouch"))
        {
            anim.SetBool("crouching",true);
            Discoll.enabled = false;
        }else
        {
            anim.SetBool("crouching", false);
            Discoll.enabled = true;
        }
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Heart()//����ֵ
    {
        if (CHP)//��������ֵ
        {
            HP--;
            CHP = false;
        }

        if (HP == 5)//������ֵ����ʱ�رն�Ӧͼ��
        {
            heartimage5.SetActive(true);
            heartimage4.SetActive(true);
            heartimage3.SetActive(true);
            heartimage2.SetActive(true);
            heartimage1.SetActive(true);
        }

        if (HP == 4)
        {
            heartimage5.SetActive(false);
            heartimage4.SetActive(true);
            heartimage3.SetActive(true);
            heartimage2.SetActive(true);
            heartimage1.SetActive(true);
        }

        if (HP == 3)
        {
            heartimage5.SetActive(false);
            heartimage4.SetActive(false);
            heartimage3.SetActive(true);
            heartimage2.SetActive(true);
            heartimage1.SetActive(true);
        }

        if (HP == 2)
        {
            heartimage5.SetActive(false);
            heartimage4.SetActive(false);
            heartimage3.SetActive(false);
            heartimage2.SetActive(true);
            heartimage1.SetActive(true);
        }

        if (HP == 1)
        {
            heartimage5.SetActive(false);
            heartimage4.SetActive(false);
            heartimage3.SetActive(false);
            heartimage2.SetActive(false);
            heartimage1.SetActive(true);
        }

        if (HP == 0)
        {
            heartimage5.SetActive(false);
            heartimage4.SetActive(false);
            heartimage3.SetActive(false);
            heartimage2.SetActive(false);
            heartimage1.SetActive(false);
            Time.timeScale = 0f;
            GAMEGVER_menu.SetActive(true);
        }
    }

}
