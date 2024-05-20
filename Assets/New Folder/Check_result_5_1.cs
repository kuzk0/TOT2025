using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Check_result_5_1 : MonoBehaviour
{
    public GameObject[] Znachenia;
    public float[] znach;
    public bool[] srav;
    public GameObject[] togle;
    public GameObject win_windows, lose_windows;
    private bool win = true;
    private float GetFloat(string stringValue, float defaultValue)
    {
        float result = defaultValue;
        float.TryParse(stringValue, out result);
        return result;
    }
    public void check()
    {
        win = true;
        for(int i = 0;i<togle.Length;i++)
        {
            if (!togle[i].GetComponent<Toggle>().isOn) { 
                win = false; i = togle.Length;
            }
        }
        for(int i = 0;i<Znachenia.Length&& Znachenia.Length==znach.Length&&srav.Length == znach.Length; i++)
        {
            if (srav[i])
            {
                if (Znachenia[i].GetComponentInChildren<Info>().number != znach[i])
                {
                    win = false; 
                }
            }else
            {
                if (Znachenia[i].GetComponentInChildren<Info>().number >= 0)
                {
                    if (((znach[i] * 1.03f) < Znachenia[i].GetComponentInChildren<Info>().number) || (znach[i] * 0.97f) > Znachenia[i].GetComponentInChildren<Info>().number)
                    {
                        win = false; Debug.Log(Znachenia[i].GetComponentInChildren<Info>().number);
                    }
                }else
                {
                    if (((znach[i] * 1.03f) > Znachenia[i].GetComponentInChildren<Info>().number) || (znach[i] * 0.97f) < Znachenia[i].GetComponentInChildren<Info>().number)
                    {
                        win = false; Debug.Log(Znachenia[i].GetComponentInChildren<Info>().number);
                    }
                }
            }
        }
        if(win)
        { win_windows.SetActive(true); }
        else
        { lose_windows.SetActive(true); }
    }
}
