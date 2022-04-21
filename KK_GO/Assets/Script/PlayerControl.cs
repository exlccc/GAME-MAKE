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
    public int HP = 5;//生命值
    public GameObject GAMEGVER_menu;//死亡菜单
    public GameObject heartimage1, heartimage2, heartimage3, heartimage4, heartimage5;//对应生命值图标
   
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
        if (Input.GetButtonDown("Jump") && jumpCount > 0)//当按下跳跃键并且跳跃次数大于0
        {
            jumpPressed = true;
        }
    }

    private void FixedUpdate()
    {
       
        if (!isHurt)//当没有受伤时
        {
            GroundMovement();
        }
        

        isGround = Physics2D.OverlapCircle(belowCheck.position, 0.1f, ground);//判是否在地面上
        
        Jump();
        SwitchAnim();
        Heart();
    }


   
     void GroundMovement() //角色移动
    {
        //水平移动
        float horizontalMove = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2 (horizontalMove * speed, rb.velocity.y);
        
        //翻转
        if(horizontalMove != 0)
        {
            transform.localScale = new Vector3(horizontalMove,1, 1);
        }
        
    }


        void Jump() //角色跳跃
    {
        if (isGround)//当在地面上
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
        if(jumpPressed && jumpCount > 0)//当按下跳跃键并在地面上
        {
            jumpAudio.Play();
            isJump = true;
            rb.velocity = new Vector2 (rb.velocity.x, jumpForce);
            jumpCount--;
            jumpPressed = false;
        }
      
    }

     void SwitchAnim() //动画转换
    {
        anim.SetFloat("running",Mathf.Abs(rb.velocity.x));//角色水平移动动画
        
        if (isGround)//当在地面上时
        {
            anim.SetBool("falling",false);
        }
        else if(!isGround && rb.velocity.y > 0)//当不在地面上并且有向上的速度时
        {
            anim.SetBool("jumping",true);
        }
        else if (rb.velocity.y < 0)//当有向下的速度时
        {
            anim.SetBool("jumping",false);
            anim.SetBool("falling",true);
        }

        if (isHurt)//当受伤时
        {
            anim.SetBool("hurt", true);
            if (Mathf.Abs(rb.velocity.x) < 0.1f)//当反弹的距离小于0.1时
            {    
             isHurt = false;
                anim.SetBool("hurt", false);
            }
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) //碰撞触发器
    {//收集物品
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

        //掉落死亡
        if (collision.tag == "DeadLine")
        {
            HP = 0;
            GetComponent<AudioSource>().enabled = false;
            fallAudio.Play();
            Invoke("Restart", 3f);
        }
    }

    
    private void OnCollisionEnter2D(Collision2D collision)//碰撞检测器
    {
        
        if (collision.gameObject.tag == "Enemy")
        {//消灭敌人
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();//调用Enemy类
            if(Physics2D.OverlapCircle(belowCheck.position, 0.2f, Enemy)) //判断groundCheck是否接触敌人
            {
                destroy_Enemy++;
                enemy.Jumpon();//调用其他类方法
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
            //敌人对玩家造成伤害
            else if (transform.position.x < collision.gameObject.transform.position.x)//如果玩家在敌人的左边
            {
                CHP = true;
                hurtAudio.Play();
                rb.velocity = new Vector2(-reboundDistance,rb.velocity.y);
                isHurt = true;
           
            }
            else if (transform.position.x > collision.gameObject.transform.position.x)//如果玩家在敌人的右边
            {
                CHP = true;
                hurtAudio.Play();
                rb.velocity = new Vector2(reboundDistance,rb.velocity.y);
                isHurt = true;
            }

        }
    }

    void Crouch()//蹲下
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

    void Heart()//生命值
    {
        if (CHP)//消耗生命值
        {
            HP--;
            CHP = false;
        }

        if (HP == 5)//当生命值减少时关闭对应图标
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
