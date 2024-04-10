using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Info : MonoBehaviour
{
    public float number;
    private float GetFloat(string stringValue, float defaultValue)
    {
        float result = defaultValue;
        float.TryParse(stringValue, out result);
        return result;
    }
    public void ButClick(GameObject Input)
    {
        number = GetFloat(Input.GetComponent<TextMeshProUGUI>().text,0.0f);
        Debug.Log(Input.GetComponent<TextMeshProUGUI>().text);
    }
}
