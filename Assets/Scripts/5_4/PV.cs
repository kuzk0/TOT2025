using System.Collections.Generic;
using UnityEngine;
using XCharts.Runtime;              // Подключение библиотеки XCharts для построения графиков
using TMPro;                        // Для доступа к полям ввода TMP_InputField

public class PV_5_4 : MonoBehaviour // Скрипт для построения PV-графика в Unity
{
    //btn - first in line of calculations
    //dano - element from dano list
    // Объекты UI, откуда получаются исходные данные
    public GameObject T1_dano, T2_btn, V_btn, mu_dano, Rv_btn;

    // Переменные для хранения числовых значений
    private float T1, T2, V, mu, Rv;

    private LineChart chart;       // Ссылка на компонент графика

    // Безопасное преобразование строки в float
    private float GetFloat(string stringValue, float defaultValue)
    {
        float result = defaultValue;
        float.TryParse(stringValue, out result);
        return result;
    }

    // Получение значения из компонента Info на кнопке
    private float GetFloatFromBtn(GameObject btn)
    {
        return btn.GetComponentInChildren<Info>().number;
    }

    // Получение значения из TMP_InputField, например из "дано"
    private float GetFloatFromDano(GameObject z)
    {
        return GetFloat(z.GetComponentInChildren<TMP_InputField>().text, 0.0f);
    }

    // Добавление визуальной метки на точку графика
    private void AddLabelStyle(Line series, int index, string label)
    {
        var dataPoint = series.data[index];
        var labelStyle = dataPoint.EnsureComponent<LabelStyle>();
        labelStyle.formatter = label;
        labelStyle.offset = new Vector3(10, 15, 0);
    }

    // Проверка, что число допустимо для вычислений
    private bool isValidFloat(float num)
    {
        return (num != Mathf.Infinity && num != 0 && !float.IsNaN(num));
    }

    // Вызывается при запуске сцены
    void Start()
    {
        chart = GetComponent<LineChart>();                            // Получаем компонент LineChart на объекте
        chart.GetChartComponent<Title>().text = "PV-Диаграмма (Изохорный процесс)"; // Устанавливаем заголовок

        if (chart == null)
        {
            Debug.LogError("LineChart компонент не найден!");         // Ошибка, если компонент графика не найден
            return;
        }

        // Получаем компоненты осей
        var xAxis = chart.GetChartComponent<XAxis>();
        var yAxis = chart.GetChartComponent<YAxis>();

        // Настройка оси X (объём)
        xAxis.type = Axis.AxisType.Value;
        xAxis.axisName.name = "V, м^3";           // Название оси
        xAxis.axisLine.showArrow = true;          // Показывать стрелку
        xAxis.splitNumber = 6;                    // Количество делений
        xAxis.axisLabel.formatter = "{value:F3}"; // Формат чисел с 3 знаками

        // Настройка оси Y (давление)
        yAxis.type = Axis.AxisType.Value;
        yAxis.axisName.name = "P, кПа";           // Название оси
        yAxis.axisLine.showArrow = true;
        yAxis.splitNumber = 6;
        yAxis.axisLabel.formatter = "{value}";

        // Вызов генерации графика здесь отключён (возможно вызывается вручную через Replot)
        //GeneratePVGraph();
    }

    // Метод генерации PV-графика
    void GeneratePVGraph()
    {
        chart.RemoveData(); // Очищаем предыдущие данные с графика

        // Создаём новую линию на графике
        var series = chart.AddSerie<Line>("Изохорный процесс");
        series.symbol.type = SymbolType.Arrow;  // Символ стрелки (показывает направление)
        series.symbol.gap = 1;                  // Интервал между стрелками

        // Получаем данные из UI
        T1 = GetFloatFromDano(T1_dano);
        T2 = GetFloatFromBtn(T2_btn);
        V = GetFloatFromBtn(V_btn);
        mu = GetFloatFromDano(mu_dano);
        Rv = GetFloatFromBtn(Rv_btn);

        // Проверка валидности значений
        if (!isValidFloat(T2 - T1) || !isValidFloat(V) || !isValidFloat(Rv) || !isValidFloat(mu)) return;

        List<Vector2> points = new List<Vector2>(); // Список точек графика

        // Построение точек: температура от T1 до T2 с шагом, берём 20 точек
        for (float T = T1; T <= T2; T += (T2 - T1) / 20)
        {
            float P = (mu * Rv * T) / V / 1000f; // Уравнение состояния: P = mu*Rv*T / V (делим на 1000 для перевода в кПа)
            points.Add(new Vector2(V, P));       // Добавляем точку (V, P) — т.к. V постоянен, это вертикальная линия
        }

        // Добавление точек в график
        foreach (var point in points)
        {
            series.AddData(point.x, point.y);
        }

        // Метки начала и конца процесса
        AddLabelStyle(series, 0, "1");
        AddLabelStyle(series, series.data.Count - 1, "2");
    }

    // Метод для перерисовки графика — можно вызывать вручную из UI
    public void Replot()
    {
        try { GeneratePVGraph(); }
        catch { }
    }
}
