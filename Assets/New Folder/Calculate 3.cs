using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using System.Xml;
using UnityEngine;

public class Calculate3 : MonoBehaviour
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
    }

    private void Update()
    {
        {
            float gavno = GetFloat(up.GetComponentInChildren<TextMeshProUGUI>().text, 0.0f) * GetFloat(down.GetComponentInChildren<TextMeshProUGUI>().text, 0.0f);
            if (gavno != Mathf.Infinity && gavno != 0 && !float.IsNaN(gavno))
            {
                gavno = gavno / 1000;
                transform.GetComponent<TextMeshProUGUI>().text = gavno.ToString() + " êÄæ";
                result = GameObject.FindGameObjectWithTag("Q2");
                result.GetComponent<Info>().number = gavno;

            }
            else
            {
                transform.GetComponent<TextMeshProUGUI>().text = null;
                result = GameObject.FindGameObjectWithTag("Q2");
                result.GetComponent<Info>().number = 0;
            }
        }
    }
}
