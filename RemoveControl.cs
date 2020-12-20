using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveControl : MonoBehaviour
{
    public GameObject enemy;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            enemy.gameObject.GetComponent<OrkWalk>().angry = true;
            StartCoroutine("SuicidBoys");
        }
    }

    IEnumerator SuicidBoys()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
