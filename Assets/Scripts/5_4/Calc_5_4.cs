using System;                           // ����������� ����������� ���������� .NET ��� ������� ������� (��������, Math.Log).
using UnityEngine;                     // �������� ������������ ��� ��� Unity-������.

public class Calc_5_4 : MonoBehaviour   // ����� MonoBehaviour, ������������ � �������� ������� � Unity.
{
    // ���������� ��� �������� GameObject'�� ����� ����� � ������ ��������
    private GameObject VStr, qStr, T2Str, R�Str, CvStr, CpStr, dSStr, duStr, dhStr,
         V, q, T2, R�, Cv, Cp, dS, du, dh;

    // ����� ��� ����������� �������������� ������ � float. ���������� �������� �� ��������� ��� �������.
    private float GetFloat(string stringValue, float defaultValue)
    {
        float result = defaultValue;
        float.TryParse(stringValue, out result); // �������� ������������� ������ � float
        return result;
    }

    // ����� ��� ������ ��������� ������� �� �����
    private GameObject F(GameObject obj, string name)
    {
        return obj.transform.Find(name).gameObject;
    }

    // ����� ��� ��������� float �� ���� TEXDraw (��������� ����������� ����������� ������/������)
    private float ToF(GameObject z)
    {
        return GetFloat(z.GetComponentInChildren<TEXDraw>().text, 0.0f);
    }

    // ����� ��� ��������� ���������� � UI
    private void SetRes(GameObject btn, GameObject otvet, float res)
    {
        if (!btn || !otvet) // �������� �� null
        {
            Debug.Log(btn);   // ����� � ������� ����������� �������
            Debug.Log(otvet);
            return;
        }
        try
        {
            // ������������� ����� � TEXDraw � ��������� �������� � Info-���������
            otvet.GetComponentInChildren<TEXDraw>().text = res.ToString();
            btn.GetComponentInChildren<Info>().number = res;
        }
        catch
        {
            // ���� ���-�� ����� �� ���, ���������� ��������
            otvet.GetComponentInChildren<TEXDraw>().text = null;
            btn.GetComponentInChildren<Info>().number = 0;
        }
    }

    // ������������� �������� � �����
    private void Start()
    {
        // ����� ���� ��������� ������ (������������)
        VStr = F(gameObject, "V_str");
        qStr = F(gameObject, "q_str");
        T2Str = F(gameObject, "T2_str");
        R�Str = F(gameObject, "R�_str");
        CvStr = F(gameObject, "Cv_str");
        CpStr = F(gameObject, "Cp_str");
        dSStr = F(gameObject, "dS_str");
        duStr = F(gameObject, "du_str");
        dhStr = F(gameObject, "dh_str");

        // ����� ���������� UI-��������� ��� �����/������ ��������
        V = F(VStr, "V");
        q = F(qStr, "q");
        T2 = F(T2Str, "T2");
        R� = F(R�Str, "R�");
        Cv = F(CvStr, "Cv");
        Cp = F(CpStr, "Cp");
        dS = F(dSStr, "dS");
        du = F(duStr, "du");
        dh = F(dhStr, "dh");
    }

    /*
     * �������, ������� ����������� � FixedUpdate():
     * 
     * MATH
     * R� = R / mu
     * V = (R� * T1) / p1
     * T2 = (p2 * T1) / p1
     * Cv = R� * i / 2
     * Cp = R� * (i + 2) / 2
     * q  = Cv * (T2 - T1)
     * dS = Cv * ln(T2 / T1)
     * du = Cv * (T2 - T1)
     * dh = Cp * (T2 - T1)
     */

    // FixedUpdate ������������ ��� ���������� ���������� � ���������� ��������� �����
    private void FixedUpdate()
    {
        try
        {
            // ���������� q
            SetRes(q, F(qStr, "Otvet"),
               ToF(F(qStr, "inform/Cv")) * (ToF(F(qStr, "inform/T2")) - ToF(F(qStr, "inform/T1")))
               );
        }
        catch { }

        try
        {
            // ���������� T2
            SetRes(T2, F(T2Str, "Otvet"),
               (ToF(F(T2Str, "inform/p2")) * ToF(F(T2Str, "inform/T1"))) / ToF(F(T2Str, "inform/p1"))
               );
        }
        catch { }

        try
        {
            // ���������� R�
            SetRes(R�, F(R�Str, "Otvet"),
               ToF(F(R�Str, "inform/R")) / ToF(F(R�Str, "inform/mu"))
               );
        }
        catch { }

        try
        {
            // ���������� Cv
            SetRes(Cv, F(CvStr, "Otvet"),
                ToF(F(CvStr, "inform/R�")) * ToF(F(CvStr, "inform/i")) / 2.0f
               );
        }
        catch { }

        try
        {
            // ���������� Cp
            SetRes(Cp, F(CpStr, "Otvet"),
                ToF(F(CpStr, "inform/R�")) * (ToF(F(CpStr, "inform/i")) + 2) / 2.0f
               );
        }
        catch { }

        try
        {
            // ���������� V
            SetRes(V, F(VStr, "Otvet"),
                (ToF(F(VStr, "inform/R�")) * ToF(F(VStr, "inform/T1"))) / ToF(F(VStr, "inform/p1"))
               );
        }
        catch { }

        try
        {
            // ���������� dS (��������)
            SetRes(dS, F(dSStr, "Otvet"),
                (float)(ToF(F(dSStr, "inform/Cv")) * Math.Log(ToF(F(dSStr, "inform/T2")) / ToF(F(dSStr, "inform/T1"))))
                );
        }
        catch { }

        try
        {
            // ���������� du (��������� ���������� �������)
            SetRes(du, F(duStr, "Otvet"),
                 ToF(F(duStr, "inform/Cv")) * (ToF(F(duStr, "inform/T2")) - ToF(F(duStr, "inform/T1")))
                );
        }
        catch { }

        try
        {
            // ���������� dh (��������� ���������)
            SetRes(dh, F(dhStr, "Otvet"),
                 ToF(F(dhStr, "inform/Cp")) * (ToF(F(dhStr, "inform/T2")) - ToF(F(dhStr, "inform/T1")))
                );
        }
        catch { }
    }
}
