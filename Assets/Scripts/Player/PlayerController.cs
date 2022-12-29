using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    public Collider2D coll;
    public AudioSource jumpAudio,hurtAudio,coinAudio;
    public float deadLiney = -7.0f;//ÕÊº“µÙ¬‰À¿Õˆ±ﬂΩÁ


    public float speed, jumpforce;
    public LayerMask ground;
    public Transform groundCheck;
    public int Coin = 0;

    public bool isGround, isJump;
    int jumpCount;
    bool jumpPressed;
    [SerializeField]
    public Text CoinNum;
    private bool isHurt;//ƒ¨»œfalse

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
      
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Jump") && jumpCount >0)
        {
            jumpPressed = true;
        }
        CoinNum.text = (Coin/2).ToString();

        if (transform.position.y < deadLiney)
        {
            GetComponent<AudioSource>().enabled = false;
            Invoke("Restart", 0.5f);
        }


    }

    private void FixedUpdate()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, 0.1f, ground);
        if(!isHurt)
        {
            Movement();
        }
        jump();
        SwitchAnim();
    }

    // player move
    void Movement()
    {
        float horizontalmove = Input.GetAxis("Horizontal");
        float facedirection = Input.GetAxisRaw("Horizontal");
    
        //move
        if(horizontalmove != 0)
        {
            rb.velocity = new Vector2(horizontalmove * speed * Time.deltaTime , rb.velocity.y);
            anim.SetFloat("walking", Mathf.Abs(facedirection));
        }

        if(facedirection != 0)
        {
            transform.localScale = new Vector3(facedirection *(-1),1,1);
        }


    }

    void jump()
    {
        if(isGround)
        {
            jumpCount = 2;
            isJump = false;
        }
        if(jumpPressed && isGround)
        {
            isJump = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpforce * Time.fixedDeltaTime);
            jumpAudio.Play();
            jumpCount--;
            jumpPressed = false;
        }
        else if(jumpPressed && jumpCount >0 && !isGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpforce * Time.fixedDeltaTime);
            jumpAudio.Play();
            jumpCount--;
            jumpPressed = false;
        }
    }

  

    void SwitchAnim()
    {
        
        if (!isGround && rb.velocity.y >0 && jumpCount == 0)
        {
            anim.SetBool("doublejumping", true);
            anim.SetBool("jumping", false);
        }
        else if(!isGround && rb.velocity.y > 0 && jumpCount == 1)
        {
            anim.SetBool("jumping", true);
            anim.SetBool("doublejumping", false);
        }
        else if (rb.velocity.y < 0 && !isGround)
        {
            anim.SetBool("jumping", false);
            anim.SetBool("doublejumping", false);
            anim.SetBool("falling", true);
        }
        else if (isGround)
        {
            anim.SetBool("falling", false);
        }

        if (isHurt)
        {
            anim.SetBool("hurt", true);
            if (Mathf.Abs(rb.velocity.x) < 0.1f)
            {
                isHurt = false;
                anim.SetBool("hurt", false);
            }
        }
    }

    public void CoinCount()
    {
        Coin = Coin + 1;
    }

    //Coin Collection
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Collection")
        {
           coinAudio.Play();
           collision.GetComponent<Animator>().Play("disappear");
        }
    }


    //hurt
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (transform.position.x < collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(-5, rb.velocity.y);
                hurtAudio.Play();
                isHurt = true;
            }
            else if (transform.position.x > collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(5, rb.velocity.y);
                hurtAudio.Play();
                isHurt = true;
            }
        }
    }

    //Ω«…´À¿Õˆ÷ÿ∆Ù
    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
