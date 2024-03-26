using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using System.Xml;
using UnityEngine;

public class Calculate : MonoBehaviour
{
    public GameObject down;
    public GameObject up;
    private GameObject result;
    private float GetFloat(string stringValue, float defaultValue)
    {
        float result = defaultValue;
        float.TryParse(stringValue, out result);
        return result;
    }
    private void Start()
    {
        up.GetComponentInChildren<TextMeshProUGUI>().text = null;
        down.GetComponentInChildren<TextMeshProUGUI>().text = null;
        result = GameObject.FindGameObjectWithTag("Q");
    }
   
    private void Update()
    {
        {
            float gavno = (GetFloat(up.GetComponentInChildren<TextMeshProUGUI>().text, 0.0f)*1000) / GetFloat(down.GetComponentInChildren<TextMeshProUGUI>().text, 0.0f);
            if (gavno != Mathf.Infinity && gavno!=0&&!float.IsNaN(gavno))
            {
                result = GameObject.FindGameObjectWithTag("N");
                transform.GetComponent<TextMeshProUGUI>().text = gavno.ToString()+" Âò";
                result.GetComponent<Info>().number = gavno;

            }
            else
            {
                transform.GetComponent<TextMeshProUGUI>().text = null;
                result = GameObject.FindGameObjectWithTag("N");
                result.GetComponent<Info>().number = 0;
            }
        }
    }
}
