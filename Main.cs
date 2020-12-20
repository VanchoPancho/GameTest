using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public Player player;
    public Text HpText;
    public Image bar;

    public void Lose()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Update()
    {
        HpText.text = "HP: " + player.GetHp().ToString();
        bar.fillAmount = player.curHp / player.maxHP;
    }



}
