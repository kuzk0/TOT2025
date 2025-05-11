using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using System.Xml;
using UnityEngine;

public class Calculate2 : MonoBehaviour
{
    public GameObject q,w,e,r;
    private GameObject result;
    private float GetFloat(string stringValue, float defaultValue)
    {
        float result = defaultValue;
        float.TryParse(stringValue, out result);
        return result;
    }
    private void Start()
    {
        q.GetComponentInChildren<TextMeshProUGUI>().text = null;
        w.GetComponentInChildren<TextMeshProUGUI>().text = null;
        e.GetComponentInChildren<TextMeshProUGUI>().text = null;
        r.GetComponentInChildren<TextMeshProUGUI>().text = null;
    }

    private void Update()
    {
        {
            float gavno = 0;
            gavno = GetFloat(q.GetComponentInChildren<TextMeshProUGUI>().text, 0.0f) * GetFloat(w.GetComponentInChildren<TextMeshProUGUI>().text, 0.0f) * (GetFloat(e.GetComponentInChildren<TextMeshProUGUI>().text, 0.0f) - GetFloat(r.GetComponentInChildren<TextMeshProUGUI>().text, 0.0f)); 
            if (gavno != Mathf.Infinity && gavno != 0 && !float.IsNaN(gavno))
            {
                gavno = gavno / 1000;
                transform.GetComponent<TextMeshProUGUI>().text = gavno.ToString()+" êÄæ";
                result = GameObject.FindGameObjectWithTag("Q");
                result.GetComponent<Info>().number= gavno;

            }
            else
            {
                transform.GetComponent<TextMeshProUGUI>().text = null;
                result = GameObject.FindGameObjectWithTag("Q");
                result.GetComponent<Info>().number = 0;
            }
        }
    }
}

