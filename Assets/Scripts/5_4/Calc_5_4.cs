using System;                           // Подключение стандартной библиотеки .NET для базовых функций (например, Math.Log).
using UnityEngine;                     // Основное пространство имён для Unity-движка.

public class Calc_5_4 : MonoBehaviour   // Класс MonoBehaviour, подключённый к игровому объекту в Unity.
{
    // Переменные для хранения GameObject'ов полей ввода и вывода значений
    private GameObject VStr, qStr, T2Str, RвStr, CvStr, CpStr, dSStr, duStr, dhStr,
         V, q, T2, Rв, Cv, Cp, dS, du, dh;

    // Метод для безопасного преобразования строки в float. Возвращает значение по умолчанию при неудаче.
    private float GetFloat(string stringValue, float defaultValue)
    {
        float result = defaultValue;
        float.TryParse(stringValue, out result); // Пытаемся преобразовать строку в float
        return result;
    }

    // Метод для поиска дочернего объекта по имени
    private GameObject F(GameObject obj, string name)
    {
        return obj.transform.Find(name).gameObject;
    }

    // Метод для получения float из поля TEXDraw (компонент визуального отображения формул/текста)
    private float ToF(GameObject z)
    {
        return GetFloat(z.GetComponentInChildren<TEXDraw>().text, 0.0f);
    }

    // Метод для установки результата в UI
    private void SetRes(GameObject btn, GameObject otvet, float res)
    {
        if (!btn || !otvet) // Проверка на null
        {
            Debug.Log(btn);   // Вывод в консоль проблемного объекта
            Debug.Log(otvet);
            return;
        }
        try
        {
            // Устанавливаем текст в TEXDraw и сохраняем значение в Info-компонент
            otvet.GetComponentInChildren<TEXDraw>().text = res.ToString();
            btn.GetComponentInChildren<Info>().number = res;
        }
        catch
        {
            // Если что-то пошло не так, сбрасываем значения
            otvet.GetComponentInChildren<TEXDraw>().text = null;
            btn.GetComponentInChildren<Info>().number = 0;
        }
    }

    // Инициализация объектов в сцене
    private void Start()
    {
        // Поиск всех текстовых блоков (информаторов)
        VStr = F(gameObject, "V_str");
        qStr = F(gameObject, "q_str");
        T2Str = F(gameObject, "T2_str");
        RвStr = F(gameObject, "Rв_str");
        CvStr = F(gameObject, "Cv_str");
        CpStr = F(gameObject, "Cp_str");
        dSStr = F(gameObject, "dS_str");
        duStr = F(gameObject, "du_str");
        dhStr = F(gameObject, "dh_str");

        // Поиск внутренних UI-элементов для ввода/вывода значений
        V = F(VStr, "V");
        q = F(qStr, "q");
        T2 = F(T2Str, "T2");
        Rв = F(RвStr, "Rв");
        Cv = F(CvStr, "Cv");
        Cp = F(CpStr, "Cp");
        dS = F(dSStr, "dS");
        du = F(duStr, "du");
        dh = F(dhStr, "dh");
    }

    /*
     * Формулы, которые реализуются в FixedUpdate():
     * 
     * MATH
     * Rв = R / mu
     * V = (Rв * T1) / p1
     * T2 = (p2 * T1) / p1
     * Cv = Rв * i / 2
     * Cp = Rв * (i + 2) / 2
     * q  = Cv * (T2 - T1)
     * dS = Cv * ln(T2 / T1)
     * du = Cv * (T2 - T1)
     * dh = Cp * (T2 - T1)
     */

    // FixedUpdate используется для выполнения вычислений с постоянным временным шагом
    private void FixedUpdate()
    {
        try
        {
            // Вычисление q
            SetRes(q, F(qStr, "Otvet"),
               ToF(F(qStr, "inform/Cv")) * (ToF(F(qStr, "inform/T2")) - ToF(F(qStr, "inform/T1")))
               );
        }
        catch { }

        try
        {
            // Вычисление T2
            SetRes(T2, F(T2Str, "Otvet"),
               (ToF(F(T2Str, "inform/p2")) * ToF(F(T2Str, "inform/T1"))) / ToF(F(T2Str, "inform/p1"))
               );
        }
        catch { }

        try
        {
            // Вычисление Rв
            SetRes(Rв, F(RвStr, "Otvet"),
               ToF(F(RвStr, "inform/R")) / ToF(F(RвStr, "inform/mu"))
               );
        }
        catch { }

        try
        {
            // Вычисление Cv
            SetRes(Cv, F(CvStr, "Otvet"),
                ToF(F(CvStr, "inform/Rв")) * ToF(F(CvStr, "inform/i")) / 2.0f
               );
        }
        catch { }

        try
        {
            // Вычисление Cp
            SetRes(Cp, F(CpStr, "Otvet"),
                ToF(F(CpStr, "inform/Rв")) * (ToF(F(CpStr, "inform/i")) + 2) / 2.0f
               );
        }
        catch { }

        try
        {
            // Вычисление V
            SetRes(V, F(VStr, "Otvet"),
                (ToF(F(VStr, "inform/Rв")) * ToF(F(VStr, "inform/T1"))) / ToF(F(VStr, "inform/p1"))
               );
        }
        catch { }

        try
        {
            // Вычисление dS (энтропия)
            SetRes(dS, F(dSStr, "Otvet"),
                (float)(ToF(F(dSStr, "inform/Cv")) * Math.Log(ToF(F(dSStr, "inform/T2")) / ToF(F(dSStr, "inform/T1"))))
                );
        }
        catch { }

        try
        {
            // Вычисление du (изменение внутренней энергии)
            SetRes(du, F(duStr, "Otvet"),
                 ToF(F(duStr, "inform/Cv")) * (ToF(F(duStr, "inform/T2")) - ToF(F(duStr, "inform/T1")))
                );
        }
        catch { }

        try
        {
            // Вычисление dh (изменение энтальпии)
            SetRes(dh, F(dhStr, "Otvet"),
                 ToF(F(dhStr, "inform/Cp")) * (ToF(F(dhStr, "inform/T2")) - ToF(F(dhStr, "inform/T1")))
                );
        }
        catch { }
    }
}
