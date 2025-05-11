using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using XCharts.Runtime;
using System.Security.Cryptography;
using TMPro;
using static UnityEngine.GraphicsBuffer;

public class TS_5_4 : MonoBehaviour
{//btn - first in line of calculations
    //dano - element from dano list
    public GameObject T1_dano, T2_btn, Cv_btn;
    // Start is called before the first frame update
    private float T1, T2, Cv;

    [SerializeField] private float arrowWidth = 0.1f;

    private LineChart chart; // Ссылка на график


    private float GetFloat(string stringValue, float defaultValue)
    {
        float result = defaultValue;
        float.TryParse(stringValue, out result);
        return result;
    }

    private float GetFloatFromBtn(GameObject btn)
    {
        return btn.GetComponentInChildren<Info>().number;
    }

    private float GetFloatFromDano(GameObject z)
    {
        return GetFloat(z.GetComponentInChildren<TMP_InputField>().text, 0.0f);
    }

    private void AddLabelStyle(Line series, int index, string label)
    {
        // Получаем точку по индексу
        var dataPoint = series.data[index];

        // Создаем стиль метки и устанавливаем параметры
        var labelStyle = dataPoint.EnsureComponent<LabelStyle>();
        labelStyle.formatter = label; // Текст метки
        labelStyle.offset = new Vector3(10, 15, 0);

    }

    private bool isValidFloat(float num)
    {
        return (num != Mathf.Infinity && num != 0 && !float.IsNaN(num));
    }

    void Start()
    {

        chart = GetComponent<LineChart>(); // Получаем компонент графика
        chart.GetChartComponent<Title>().text = "TS-Диаграмма (Изохорный процесс)";
        if (chart == null)
        {
            Debug.LogError("LineChart компонент не найден!");
            return;
        }
        var xAxis = chart.GetChartComponent<XAxis>();
        var yAxis = chart.GetChartComponent<YAxis>();

        // Добавляем оси и заголовок
        xAxis.type = Axis.AxisType.Value;
        xAxis.axisLine.showArrow = true;
        xAxis.axisName.name = "S, Дж/(К·моль)";
        yAxis.type = Axis.AxisType.Value;
        yAxis.axisLine.showArrow = true;
        yAxis.axisName.name = "T, К";

        xAxis.splitNumber = 6; // Меньше подписей на оси X
        //xAxis.axisLabel.rotate = 30; // Повернуть подписи на 30 градусов
        //xAxis.axisLabel.interval = 1; // Подписи будут реже (если 0 — ставятся все)

        xAxis.axisLabel.formatter = "{value:F3}";


        yAxis.axisLabel.formatter = "{value}"; // Подписи без экспоненциального вида
        yAxis.splitNumber = 6; // Ограничить количество делений

        //GeneratePVGraph();
    }

    void GeneratePVGraph()
    {
        chart.RemoveData(); // Очищаем график перед генерацией



        // Создаём серию данных для изохорного процесса
        var series = chart.AddSerie<Line>("Изохорный процесс");

        series.symbol.type = SymbolType.Arrow;
        series.symbol.gap = 1;

        T1 = GetFloatFromDano(T1_dano);
        T2 = GetFloatFromBtn(T2_btn);
        Cv = GetFloatFromBtn(Cv_btn);

        if (!isValidFloat(T2 - T1) || !isValidFloat(Cv)) return;

        // Примерные данные (давление P при постоянном объеме V)
        // Постоянный объем
        List<Vector2> points = new List<Vector2>();
        for (float T = T1; T <= T2; T += (T2 - T1) / 20) // Изменение температуры, берем 20 точек
        {
            float S = Cv * Mathf.Log(T / T1); // Энтропия (S = S0 + nRln(T/T0))
            points.Add(new Vector2(S, T));
        }

        // Добавляем точки в серию
        foreach (var point in points)
        {
            series.AddData(point.x, point.y);
        }

        AddLabelStyle(series, 0, "1");
        AddLabelStyle(series, series.data.Count - 1, "2");
    }





    // Update is called once per frame
    public void Replot()
    {
        try { GeneratePVGraph(); }
        catch { }
    }
}
