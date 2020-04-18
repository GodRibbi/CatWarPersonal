using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseHome : MonoBehaviour
{

    private Image HP;
    public float healthPoint;
    public float maxHP;
    public static bool GameOver;
    // Start is called before the first frame update
    void Start()
    {
        GameOver = false;
        HP = transform.Find("UI").Find("bloodbar").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void HealthDelay(float delay)
    {

        healthPoint += delay;
        
        if (healthPoint <= 0)
        {
            GameOver = true;
            Dead();
            return;
        }
        else if (healthPoint >= maxHP)
        {
            healthPoint = maxHP;
            return;
        }
        HP.fillAmount = GetHealthPercent();
    }

    public float GetHealthPercent()
    {
        return healthPoint / maxHP;
    }

    public void Dead()
    {
        Destroy(gameObject);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
