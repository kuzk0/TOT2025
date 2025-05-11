using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class Calc_5_2 : MonoBehaviour
{

    private GameObject QStr, qStr, T2Str, R‚Str, CvStr, m‚Str,
        Q, q, T2, R‚, Cv, m‚;

    private float GetFloat(string stringValue, float defaultValue)
    {
        float result = defaultValue;
        float.TryParse(stringValue, out result);
        return result;
    }


    private GameObject F(GameObject obj, string name)
    {
        return obj.transform.Find(name).gameObject;
    }


    //get float value for all informators
    private float ToF(GameObject z)
    {
        return GetFloat(z.GetComponentInChildren<TEXDraw>().text, 0.0f);
    }

    private void SetRes(GameObject btn, GameObject otvet, float res)
    {
        if (!btn || !otvet)
        {
            Debug.Log(btn);
            Debug.Log(otvet);
            return;
        }
        try
        {
            otvet.GetComponentInChildren<TEXDraw>().text = res.ToString();
            btn.GetComponentInChildren<Info>().number = res;
        }
        catch
        {
            otvet.GetComponentInChildren<TEXDraw>().text = null;
            btn.GetComponentInChildren<Info>().number = 0;
        }
    }


    private void Start()
    {
        QStr = F(gameObject, "Q_str");
        qStr = F(gameObject, "q_str");
        T2Str = F(gameObject, "T2_str");
        R‚Str = F(gameObject, "R‚_str");
        CvStr = F(gameObject, "Cv_str");
        m‚Str = F(gameObject, "m‚_str");

        Q = F(QStr, "Q");
        q = F(qStr, "q");
        T2 = F(T2Str, "T2");
        R‚ = F(R‚Str, "R‚");
        Cv = F(CvStr, "Cv");
        m‚ = F(m‚Str, "m‚");
    }



    /**
     * MATH
     * 
     * Q = m * q
     * q = Cv * (T2 - T1)
     * T2 = (p2 * T1) / p1
     * Cv = R‚ * i / 2
     * m‚ = (p1 * V) / (R‚ * T1)
     * R‚ = R / mu
     * 
     */


    private void FixedUpdate()
    {
        try
        {
            SetRes(Q, F(QStr, "Otvet"),
                ToF(F(QStr, "inform/m")) * ToF(F(QStr, "inform/q"))
                );
        }
        catch { }
        try
        {
            SetRes(q, F(qStr, "Otvet"),
               ToF(F(qStr, "inform/Cv")) * (ToF(F(qStr, "inform/T2")) - ToF(F(qStr, "inform/T1")))
               );
        }
        catch { }
        try
        {
            SetRes(T2, F(T2Str, "Otvet"),
               (ToF(F(T2Str, "inform/p2")) * ToF(F(T2Str, "inform/T1"))) / ToF(F(T2Str, "inform/p1"))
               );
        }
        catch { }
        try
        {
            SetRes(R‚, F(R‚Str, "Otvet"),
               ToF(F(R‚Str, "inform/R")) / ToF(F(R‚Str, "inform/mu"))
               );
        }
        catch { }
        try
        {
            SetRes(Cv, F(CvStr, "Otvet"),
                ToF(F(CvStr, "inform/R‚")) * ToF(F(CvStr, "inform/i")) / 2.0f
               );
        }
        catch { }
        try
        {
            SetRes(m‚, F(m‚Str, "Otvet"),
                (ToF(F(m‚Str, "inform/p1")) * ToF(F(m‚Str, "inform/V"))) / (ToF(F(m‚Str, "inform/R‚")) * ToF(F(m‚Str, "inform/T1")))
               );
        }
        catch { }

    }
}
