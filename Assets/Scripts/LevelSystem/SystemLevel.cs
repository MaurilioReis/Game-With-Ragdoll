using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SystemLevel : MonoBehaviour
{
    public int level = 1;
    int maxLevel = 30;
    //fill material Lerp colors

    float currentXpLevel = 0;
    float maxXpLevel = 400;
    float leftoverNextLevel;

    public Image fillBarXp;
    public TMP_Text textInfoLevel;
    public TMP_Text textLVL;

    [Header("Parameters")]
    public PlayerController playerC;
    public TMP_Text txtSpeed;
    float defaultSpeed = 5;
    [HideInInspector]
    public float bonusSpeed = 0;

    [Space(5)]
    public PileController pileC;
    public TMP_Text txtCapacity;
    int defaultCapacity = 2;
    [HideInInspector]
    public int bonusCapacity = 0;

    private void Start()
    {
        AttSpeed();
        AttCapacity();
    }

    public void AddXP(float xp)
    {
        if (level < maxLevel)
        {
            currentXpLevel += xp;

            if (currentXpLevel >= maxXpLevel)
            {
                LevelUp();
            }

            UpdateFillBar();
        }
    }

    void LevelUp()
    {
        level++;

        if(level < maxLevel)
        {
            currentXpLevel -= maxXpLevel;
            maxXpLevel += 400;
            textLVL.text = "" + level;
        }
        else
        {
            textLVL.text = "MAX";
        }

        AttSpeed();
        AttCapacity();
    }

    void UpdateFillBar()
    {
        if (level < maxLevel)
        {
            float fillAmount = currentXpLevel / maxXpLevel;
            fillBarXp.fillAmount = fillAmount;

            textInfoLevel.text = currentXpLevel + "/" + maxXpLevel;
        }
        else
        {
            fillBarXp.fillAmount = 1;

            textInfoLevel.text = maxXpLevel + "/" + maxXpLevel;
        }
    }

    public void AttSpeed()
    {
        float bonusLevel = (float)level / 6;

        defaultSpeed = 5 + (bonusSpeed + bonusLevel);

        float apparenceSpeed = defaultSpeed * 2;
        txtSpeed.text = "SPEED: " + apparenceSpeed.ToString("0.00");

        playerC.moveSpeed = defaultSpeed;

        // preciso converter os valores para o animator, considerando que inicia com speed: 1, + 1 do bonus e + 1 do level.
    }

    public void AttCapacity()
    {
        int bonusLevel = level;

        defaultCapacity = 1 + (bonusCapacity + bonusLevel);

        txtCapacity.text = "CAPACITY: " + defaultCapacity;

        pileC.limitPile = defaultCapacity;

        // aqui tudo certo e o LEVEL funciona perfeitamente ja
    }

    // ANOTAÇÕES

    // Adc xp no soco, adc xp ao derrubar, diminuir a velocidade para pegar os corpos, fazer o shader de troca de cores e fazer as compras.

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            AddXP(999);
        }
    }
}