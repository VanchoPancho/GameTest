using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rbPlayer;
    Animator anim;
    public Main main;
    public Transform groundCheck;

    public float speed;
    public float jumpHeight;
    public float curHp;
    public float maxHP = 3f;

    bool isGrounded;
    bool canWallJump;
    public bool isHit = false;
    public bool canTP = true;
    // Start is called before the first frame update
    void Start()
    {   
        rbPlayer = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        curHp = maxHP;
    }
        
    // Update is called once per frame
    void Update()
    {
        Flip();
        CheckGround();
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded || Input.GetKeyDown(KeyCode.Space) && canWallJump)
        {
            rbPlayer.AddForce(transform.up * jumpHeight, ForceMode2D.Impulse);
            if (canWallJump)
            {
                StartCoroutine("WallJamp");
            }
        }
        AnimationPlayer();
    }

    private void FixedUpdate()
    {
        if (!isHit)
        {
            rbPlayer.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rbPlayer.velocity.y);
        }
    }

    void Flip()
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        if(Input.GetAxis("Horizontal") < 0)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }
 
    void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, 0.3f);
        isGrounded = colliders.Length > 1;
    }

    void AnimationPlayer()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            anim.SetInteger("State", 4);
        }
        else if (Input.GetAxis("Horizontal") == 0 && isGrounded)
        {
            anim.SetInteger("State", 1);
        }
        else if (isGrounded)
        {
            anim.SetInteger("State", 2);
        }
        else if (!isGrounded)
        {
            anim.SetInteger("State", 3);
        }
    }

    public void RecountHP(int deltaHp)
    {
        if (isHit == false)
        {
            curHp += deltaHp;
            if (deltaHp < 0)
            {
                StartCoroutine("OnHit");
            }
        }

        if(curHp <= 0)
        {
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            StartCoroutine("Lose");
        }
    }

    IEnumerator WallJamp()
    {
        isHit = true;
        rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, rbPlayer.velocity.y);
        rbPlayer.AddForce(transform.right * -8f, ForceMode2D.Impulse);
        yield return new WaitForSeconds(1f);
        isHit = false;

    }
    
    IEnumerator OnHit()
    {
        isHit = true;
        rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, rbPlayer.velocity.y);
        rbPlayer.AddForce(transform.right * -6f, ForceMode2D.Impulse);
        rbPlayer.AddForce(transform.up * 8f, ForceMode2D.Impulse);
        GetComponent<SpriteRenderer>().color = new Color(1f, 0.15f, 0.15f);
        yield return new WaitForSeconds(1f);
        GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);
        isHit = false;      
    }

    IEnumerator Lose()
    {
        yield return new WaitForSeconds(2f);
        main.GetComponent<Main>().Lose();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Door")
        {
            if (collision.gameObject.GetComponent<DoorTeleport>().isOpen && canTP)
            {
                collision.gameObject.GetComponent<DoorTeleport>().Teleport(gameObject);
                canTP = false;
                StartCoroutine("TPwait");
            }
        }

        if (collision.gameObject.tag == "Heart")
        {
            if (curHp < maxHP)
            {
                Destroy(collision.gameObject);
                RecountHP(1);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            canWallJump = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            canWallJump = false;
        }
    }

    IEnumerator TPwait()
    {
        yield return new WaitForSeconds(1f);
        canTP = true;
    }

    public float GetHp()
    {
        return curHp;
    }


}
