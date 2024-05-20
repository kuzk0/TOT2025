using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InputField : MonoBehaviour
{
    public GameObject button;
    private float GetFloat(string stringValue, float defaultValue)
    {
        float result = defaultValue;
        float.TryParse(stringValue, out result);
        return result;
    }
    void FixedUpdate()
    {
        if (button != null) { 
       button.GetComponentInChildren<Info>().number= GetFloat(transform.GetComponent<TMP_InputField>().text,0);}
    }
}
