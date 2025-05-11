using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Check_5_4 : MonoBehaviour
{
    public GameObject R, V, mu, m, i, p1, p2, T1 //from dano
        , OtvetdS, Otvetdu, Otvetdh, OtvetV;

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
      * Râ = R / mu
      * V = (Râ * T1) / p1
      * T2 = (p2 * T1) / p1
      *
      * Cv = Râ * i / 2
      * Cp = Râ * (i + 2) / 2
      * 
      * q = Cv * (T2 - T1)
      * 
      * dS = Cv * Ln(T2 / T1)
      * du = Cv * (T2 - T1) 
      * dh = Cp * (T2 - T1) 
      */

    /**
   * MATH simpled
   * 
   * V = (R / mu * T1) / p1
   * dS = R / mu * i / 2 * Ln((p2 * T1) / p1 / T1)
   * du = R / mu * i / 2 * ((p2 * T1) / p1 - T1) 
   * dh = R / mu * (i + 2) / 2 * ((p2 * T1) / p1 - T1) 
   * 
   */

    public void Check()
    {
        bool win = false;
        float calculatedResultV =  ((ToF(R) / ToF(mu)) * ToF(T1)) / ToF(p1);
        float calculatedResultdS = (float)(((ToF(R) / ToF(mu)) * ToF(i) / 2.0f) * Math.Log(((ToF(p2) * ToF(T1))/ ToF(p1)) / ToF(T1)));
        float calculatedResultdu = ((ToF(R) / ToF(mu)) * ToF(i) / 2.0f) * ((ToF(p2) * ToF(T1)) / ToF(p1)) - ToF(T1);
        float calculatedResultdh = ((ToF(R) / ToF(mu)) * (ToF(i) + 2.0f) / 2.0f) * ((ToF(p2) * ToF(T1)) / ToF(p1)) - ToF(T1);
        try
        {
            win = Math.Abs(GetFloat(OtvetV.GetComponentInChildren<TEXDraw>().text, 0.0f) - calculatedResultV) / calculatedResultV * 100 < 3 &&
                 Math.Abs(GetFloat(OtvetdS.GetComponentInChildren<TEXDraw>().text, 0.0f) - calculatedResultdS) / calculatedResultdS * 100 < 3 &&
                   Math.Abs(GetFloat(Otvetdu.GetComponentInChildren<TEXDraw>().text, 0.0f) - calculatedResultdu) / calculatedResultdu * 100 < 3 &&
                    Math.Abs(GetFloat(Otvetdh.GetComponentInChildren<TEXDraw>().text, 0.0f) - calculatedResultdh) / calculatedResultdh * 100 < 3; 
        }
        catch { }
        if (win)
        { win_windows.SetActive(true); }
        else
        { lose_windows.SetActive(true); }
    }
}
