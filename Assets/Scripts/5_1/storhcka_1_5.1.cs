using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class storhcka_1_5_1 : MonoBehaviour
{
    public GameObject[] znach;
    [SerializeField] private GameObject result;
    private float GetFloat(string stringValue, float defaultValue)
{
    float result = defaultValue;
    float.TryParse(stringValue, out result);
    return result;
}
private void Start()
{
    for(int i = 0; i < znach.Length; i++)
        {
            znach[i].GetComponentInChildren<TEXDraw>().text = null;
        }
}

private void Update()
{
        {
            float gavno = 0;
            gavno = GetFloat(znach[0].GetComponentInChildren<TEXDraw>().text, 0.0f) *
             (Mathf.Pow((GetFloat(znach[1].GetComponentInChildren<TEXDraw>().text, 0.0f) / GetFloat(znach[2].GetComponentInChildren<TEXDraw>().text, 0.0f)),((1-GetFloat(znach[3].GetComponentInChildren<TEXDraw>().text, 0.0f)) /(GetFloat(znach[4].GetComponentInChildren<TEXDraw>().text, 0.0f)))));
        if (gavno != Mathf.Infinity && gavno != 0 && !float.IsNaN(gavno))
        {
            transform.GetComponentInChildren<TEXDraw>().text = gavno.ToString();
            result.GetComponentInChildren<Info>().number = gavno;

        }
        else
        {
            transform.GetComponentInChildren<TEXDraw>().text = null;
            result.GetComponentInChildren<Info>().number = 0;
        }
    }
}
}
