using System.Collections.Generic;
using UnityEngine;
using XCharts.Runtime;                       // Используется для работы с графиками XCharts
using TMPro;                                 // Поддержка TMP_InputField для пользовательского ввода

public class TS_5_4 : MonoBehaviour          // Скрипт, привязанный к объекту-сцене, отвечающему за построение графика TS
{
    // UI-элементы для получения данных:
    public GameObject T1_dano, T2_btn, Cv_btn; // T1 из "дано", T2 и Cv — из расчётных кнопок

    private float T1, T2, Cv;                 // Значения температуры и Cv

    [SerializeField] private float arrowWidth = 0.1f; // Ширина стрелок на графике (в данный момент не используется)

    private LineChart chart;                 // Ссылка на компонент LineChart (график)

    // Преобразование строки в число с защитой от ошибок
    private float GetFloat(string stringValue, float defaultValue)
    {
        float result = defaultValue;
        float.TryParse(stringValue, out result);
        return result;
    }

    // Получение численного значения из кнопки с компонентом Info
    private float GetFloatFromBtn(GameObject btn)
    {
        return btn.GetComponentInChildren<Info>().number;
    }

    // Получение численного значения из TMP_InputField (ввод пользователя)
    private float GetFloatFromDano(GameObject z)
    {
        return GetFloat(z.GetComponentInChildren<TMP_InputField>().text, 0.0f);
    }

    // Добавление метки на определённую точку графика
    private void AddLabelStyle(Line series, int index, string label)
    {
        var dataPoint = series.data[index];                     // Получаем нужную точку из серии
        var labelStyle = dataPoint.EnsureComponent<LabelStyle>(); // Создаём стиль метки, если его ещё нет
        labelStyle.formatter = label;                           // Устанавливаем текст метки
        labelStyle.offset = new Vector3(10, 15, 0);             // Смещение метки относительно точки
    }

    // Проверка, является ли число валидным (не NaN, не бесконечность, не ноль)
    private bool isValidFloat(float num)
    {
        return (num != Mathf.Infinity && num != 0 && !float.IsNaN(num));
    }

    // Start вызывается при запуске сцены
    void Start()
    {
        chart = GetComponent<LineChart>();                      // Получаем компонент LineChart
        chart.GetChartComponent<Title>().text = "TS-Диаграмма (Изохорный процесс)"; // Заголовок графика

        if (chart == null)
        {
            Debug.LogError("LineChart компонент не найден!");   // Проверка, если график не найден
            return;
        }

        var xAxis = chart.GetChartComponent<XAxis>();           // Получаем ось X
        var yAxis = chart.GetChartComponent<YAxis>();           // Получаем ось Y

        // Настройка оси X
        xAxis.type = Axis.AxisType.Value;
        xAxis.axisLine.showArrow = true;                        // Показываем стрелку на оси
        xAxis.axisName.name = "S, Дж/(К·моль)";                 // Название оси X (энтропия)
        xAxis.splitNumber = 6;                                  // Количество делений на оси
        xAxis.axisLabel.formatter = "{value:F3}";               // Формат вывода значений

        // Настройка оси Y
        yAxis.type = Axis.AxisType.Value;
        yAxis.axisLine.showArrow = true;
        yAxis.axisName.name = "T, К";                           // Название оси Y (температура)
        yAxis.axisLabel.formatter = "{value}";
        yAxis.splitNumber = 6;

        // Комментарий: вызов построения графика отключён (раскомментируй при необходимости)
        //GeneratePVGraph();
    }

    // Метод генерации графика (TS-диаграмма изохорного процесса)
    void GeneratePVGraph()
    {
        chart.RemoveData(); // Очистить все старые данные с графика

        // Создаём новую серию точек
        var series = chart.AddSerie<Line>("Изохорный процесс");

        series.symbol.type = SymbolType.Arrow; // Символ стрелки для направления
        series.symbol.gap = 1;                 // Интервал между стрелками

        // Получаем значения из UI
        T1 = GetFloatFromDano(T1_dano);       // T1 из поля ввода
        T2 = GetFloatFromBtn(T2_btn);         // T2 из расчётной кнопки
        Cv = GetFloatFromBtn(Cv_btn);         // Cv из расчётной кнопки

        // Проверка валидности полученных данных
        if (!isValidFloat(T2 - T1) || !isValidFloat(Cv)) return;

        List<Vector2> points = new List<Vector2>(); // Список точек для построения

        // Заполняем график 20 точками между T1 и T2
        for (float T = T1; T <= T2; T += (T2 - T1) / 20)
        {
            float S = Cv * Mathf.Log(T / T1); // Формула для изменения энтропии: S = Cv * ln(T / T1)
            points.Add(new Vector2(S, T));    // Добавляем точку (энтропия, температура)
        }

        // Добавляем каждую точку в график
        foreach (var point in points)
        {
            series.AddData(point.x, point.y);
        }

        // Добавляем метки "1" и "2" в начало и конец графика
        AddLabelStyle(series, 0, "1");
        AddLabelStyle(series, series.data.Count - 1, "2");
    }

    // Метод для перерисовки графика (можно вызывать с кнопки в UI)
    public void Replot()
    {
        try { GeneratePVGraph(); } // Безопасный вызов генерации
        catch { }                  // Без обработки ошибки — потенциальное место для улучшения
    }
}
