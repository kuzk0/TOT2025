using System;                          // Базовая .NET библиотека, используется для Math.Abs
using TMPro;                           // Подключение TextMeshPro — используется для работы с текстовыми полями ввода
using UnityEngine;                     // Пространство имён Unity, необходимо для всех компонентов Unity

public class Check_5_4 : MonoBehaviour // Скрипт для проверки правильности пользовательского ввода
{
    // Переменные для хранения объектов со значениями (вводимые данные и ответы)
    public GameObject R, V, mu, m, i, p1, p2, T1 // Из блока "дано"
        , OtvetdS, Otvetdu, Otvetdh, OtvetV;    // Вычисленные ответы пользователя

    // Окна с результатами: победа или поражение
    public GameObject win_windows, lose_windows;

    // Метод безопасного парсинга строки в float
    private float GetFloat(string stringValue, float defaultValue)
    {
        float result = defaultValue;
        float.TryParse(stringValue, out result); // Если не удалось распарсить, вернёт defaultValue
        return result;
    }

    // Получает float из TMP_InputField, находящегося внутри GameObject
    private float ToF(GameObject z)
    {
        if (!z) return 0.0f; // Проверка на null
        return GetFloat(z.GetComponentInChildren<TMP_InputField>().text, 0.0f);
    }

    /**
    * MATH
    * 
    * Rв = R / mu
    * V = (Rв * T1) / p1
    * T2 = (p2 * T1) / p1
    *
    * Cv = Rв * i / 2
    * Cp = Rв * (i + 2) / 2
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
      * dS = ((R / mu) * i / 2) * Ln(((p2 * T1) / p1) / T1)
      * du = ((R / mu) * i / 2) * (((p2 * T1) / p1) - T1) 
      * dh = ((R / mu) * (i + 2) / 2) * (((p2 * T1) / p1) - T1) 
      * 
      */

    public void Check()
    {
        bool win = false; // Изначально результат считается "неправильным"

        // Расчёт правильных значений на основе формул
        float calculatedResultV = ((ToF(R) / ToF(mu)) * ToF(T1)) / ToF(p1); // Объём
        float calculatedResultdS = ((ToF(R) / ToF(mu)) * ToF(i) / 2.0f) * Mathf.Log(((ToF(p2) * ToF(T1)) / ToF(p1)) / ToF(T1)); // Энтропия
        float calculatedResultdu = ((ToF(R) / ToF(mu)) * ToF(i) / 2.0f) * (((ToF(p2) * ToF(T1)) / ToF(p1)) - ToF(T1)); // Внутренняя энергия
        float calculatedResultdh = ((ToF(R) / ToF(mu)) * (ToF(i) + 2.0f) / 2.0f) * (((ToF(p2) * ToF(T1)) / ToF(p1)) - ToF(T1)); // Энтальпия

        try
        {
            // Проверка: если пользователь ввёл правильные значения (погрешность < 3% по всем пунктам)
            win = Math.Abs(GetFloat(OtvetV.GetComponentInChildren<TEXDraw>().text, 0.0f) - calculatedResultV) / calculatedResultV * 100 < 3 &&
                  Math.Abs(GetFloat(OtvetdS.GetComponentInChildren<TEXDraw>().text, 0.0f) - calculatedResultdS) / calculatedResultdS * 100 < 3 &&
                  Math.Abs(GetFloat(Otvetdu.GetComponentInChildren<TEXDraw>().text, 0.0f) - calculatedResultdu) / calculatedResultdu * 100 < 3 &&
                  Math.Abs(GetFloat(Otvetdh.GetComponentInChildren<TEXDraw>().text, 0.0f) - calculatedResultdh) / calculatedResultdh * 100 < 3;
        }
        catch { } // Пустой catch блок

        // Отображение соответствующего окна по результатам проверки
        if (win)
        {
            win_windows.SetActive(true); // Если все ответы правильные — показать окно победы
        }
        else
        {
            lose_windows.SetActive(true); // Иначе — окно ошибки/поражения
        }
    }
}
