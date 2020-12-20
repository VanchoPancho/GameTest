using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamageForEnemy : MonoBehaviour
{
    public int healthPointEnemy;
    Rigidbody2D rbEnemy;

    private void Start()
    {
        rbEnemy = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(int damage)
    {
        healthPointEnemy -= damage;
        rbEnemy.AddForce(transform.right * -12f, ForceMode2D.Impulse);
        rbEnemy.AddForce(transform.up * 12f, ForceMode2D.Impulse);
        StartCoroutine("TakePizdi");

        if (healthPointEnemy <= 0)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator TakeDamage()
    {
        GetComponent<SpriteRenderer>().color = new Color(1f, 0.15f, 0.15f);
        yield return new WaitForSeconds(1f);
        GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);
    }
}
