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
    Vector3 startSizeLVL;

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

    [Header("Change the material according to the level")]
    public SkinnedMeshRenderer meshR;
    float step;

    private void Start()
    {
        UpdateSpeed();
        UpdateCapacity();

        startSizeLVL = textLVL.transform.localScale;
    }

    private void Update()
    {
        if(textLVL.transform.localScale.magnitude > startSizeLVL.magnitude)
        {
            textLVL.transform.localScale = Vector3.Lerp(textLVL.transform.localScale, startSizeLVL, 5 * Time.deltaTime);
        }
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

        step = (float)level / (float)maxLevel;
        meshR.material.SetFloat("_Step", step);

        textLVL.transform.localScale = new Vector3(textLVL.transform.localScale.x * 3, textLVL.transform.localScale.y * 3, textLVL.transform.localScale.z*3);

        UpdateSpeed();
        UpdateCapacity();
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

    public void UpdateSpeed()
    {
        float bonusLevel = (float)level / 6;

        defaultSpeed = 5 + (bonusSpeed + bonusLevel);

        float apparenceSpeed = defaultSpeed * 2;
        txtSpeed.text = "SPEED: " + apparenceSpeed.ToString("0.00");

        playerC.moveSpeed = defaultSpeed;
        float speedAnim = defaultSpeed - 5;
        playerC.GetComponent<Animator>().speed = 1 + (speedAnim / 10);
    }

    public void UpdateCapacity()
    {
        int bonusLevel = level;

        defaultCapacity = 1 + (bonusCapacity + bonusLevel);

        txtCapacity.text = "CAPACITY: " + defaultCapacity;

        pileC.limitPile = defaultCapacity;
    }
}