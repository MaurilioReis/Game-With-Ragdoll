using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SoulSystem : MonoBehaviour
{

    public float totalSouls;

    public TMP_Text valueTxt;
    public float speedAtt;

    float newValueTxt;

    public Image panel;

    Vector4 fixedColor = new Vector4(255,255,255,0.13f);

    public void AddValue(int valueAdd)
    {
        totalSouls += valueAdd;
        panel.color = new Vector4(255,255,255,1);
    }

    public void RemoveValue(int valueRemove)
    {
        totalSouls -= valueRemove;
        panel.color = new Vector4(255, 255, 255, 1);
    }

    private void Update()
    {
        if (newValueTxt < totalSouls)
        {
            float delta = totalSouls - newValueTxt;
            delta *= speedAtt * Time.deltaTime;

            newValueTxt += delta;

            valueTxt.text = "" + ((int)newValueTxt+1);
        }

        if (newValueTxt > totalSouls)
        {
            float delta = newValueTxt - totalSouls;
            delta *= speedAtt * Time.deltaTime;

            newValueTxt -= delta;

            valueTxt.text = "" + ((int)newValueTxt + 1);
        }

        if(panel.color.a != fixedColor.w)
        {
            panel.color = Vector4.Lerp(panel.color, fixedColor, speedAtt * Time.deltaTime);
        }
    }

}
