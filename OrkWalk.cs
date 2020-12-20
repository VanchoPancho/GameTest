using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrkWalk : MonoBehaviour
{
    public float speed;

    Animator anim;

    public bool angry = false;
    public bool goBack = false;

    private Transform target;
    private Enemy enemy;

    private void Start()
    {
        anim = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        enemy = GetComponent<Enemy>();
    }

    private void Update()
    {
        Animations();
        if (angry)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        else if (goBack)
        {
            StartCoroutine("GoBack");
        }
        Flip();
    }

    void Animations() 
    {
        if (enemy.ooha)
        {
            anim.SetInteger("Ork_onehanded", 2);
            StartCoroutine(StopAttackAnim());
        }
    }

    void Flip()
    {
        if (target.transform.position.x > transform.position.x)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }

    IEnumerator GoBack()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, -speed * Time.deltaTime);
        yield return new WaitForSeconds(0.5f);
    }

    IEnumerator StopAttackAnim()
    {
        yield return new WaitForSeconds(0.5f);
        anim.SetInteger("Ork_onehanded", 1);
    }
}
