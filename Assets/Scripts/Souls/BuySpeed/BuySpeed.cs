using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuySpeed : MonoBehaviour
{
    bool activeSystem;
    public SystemLevel systemLevel;
    public SoulSystem systemSoul;

    public Image fillBar;
    public float speedFill = 0.1f;

    int limitBonusSpeed = 20;

    public TMP_Text valueToBuy;
    float valueBuy = 100;

    bool timeLock;

    public GameObject fx;

    private void Start()
    {
        valueToBuy.text = "" + valueBuy;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (limitBonusSpeed > 0 && systemSoul.totalSouls >= valueBuy)
        {
            if (other.tag == "Player" && activeSystem == false)
            {
                activeSystem = true;
            }
        }
        else if (systemSoul.totalSouls < valueBuy)
        {
            systemSoul.panel.color = new Vector4(255, 0, 0, 1);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && activeSystem == true)
        {
            activeSystem = false;
            timeLock = false;
            StopCoroutine("TimeLock");
            fillBar.fillAmount = 0;
        }
    }

    void Update()
    {
        if (activeSystem == true && timeLock == false && systemSoul.totalSouls >= valueBuy && limitBonusSpeed > 0)
        {
            if (fillBar.fillAmount < 1)
            {
                fillBar.fillAmount += speedFill * Time.deltaTime;
            }
            else
            {
                systemLevel.bonusSpeed += 0.25f ; // add capacity
                systemLevel.UpdateSpeed();
                systemLevel.AddXP(valueBuy / 2);

                systemSoul.RemoveValue((int)valueBuy);

                Instantiate(fx, transform.position, transform.rotation);

                limitBonusSpeed--;

                valueBuy += 100;
                valueToBuy.text = "" + valueBuy;

                timeLock = true;
                StartCoroutine("TimeLock");
                fillBar.fillAmount = 0;

                if (limitBonusSpeed <= 0)
                {
                    valueToBuy.text = "MAX";
                    this.enabled = false;
                }
            }
        }
        else if (activeSystem == true && systemSoul.totalSouls < valueBuy)
        {
            systemSoul.panel.color = new Vector4(255, 0, 0, 1);
            activeSystem = false;
        }
    }

    IEnumerator TimeLock()
    {
        yield return new WaitForSeconds(2);
        timeLock = false;
    }
}
