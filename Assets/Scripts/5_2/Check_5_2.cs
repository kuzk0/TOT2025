using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Check_5_2 : MonoBehaviour
{
    public GameObject R, V, mu, i, p1, p2, T1 //from dano
        , Otvet;

    public GameObject win_windows, lose_windows;

    private float GetFloat(string stringValue, float defaultValue)
    {
        float result = defaultValue;
        float.TryParse(stringValue, out result);
        return result;
    }
    private float ToF(GameObject z)
    {
        if (!z) return 0.0f;

        return GetFloat(z.GetComponentInChildren<TMP_InputField>().text, 0.0f);
    }

   /**
   * MATH
   * 
   * Q = m * q
   * q = Cv * (T2 - T1)
   * T2 = (p2 * T1) / p1
   * Cv = Râ * i / 2
   * mâ = (p1 * V) / (Râ * T1)
   * Râ = R / mu
   * 
   */

    /**
   * MATH simpled
   * 
   * Q = ((p1 * V) / ((R / mu) * T1)) * (((R / mu) * i / 2) * (((p2 * T1) / p1) - T1))
   */

    public void Check()
    {
        bool win = false;
        float calculatedResult = ((ToF(p1) * ToF(V)) / ((ToF(R) / ToF(mu)) * ToF(T1))) * (((ToF(R) / ToF(mu)) * ToF(i) / 2.0f) * (((ToF(p2) * ToF(T1)) / (ToF(p1)) - ToF(T1))));
        try
        {
            win = Math.Abs(GetFloat(Otvet.GetComponentInChildren<TEXDraw>().text, 0.0f) - calculatedResult) / calculatedResult * 100 < 3; 
        }
        catch { }
        if (win)
        { win_windows.SetActive(true); }
        else
        { lose_windows.SetActive(true); }
    }
}
