using System.Collections.Generic;
using UnityEngine;
using XCharts.Runtime;                       // ������������ ��� ������ � ��������� XCharts
using TMPro;                                 // ��������� TMP_InputField ��� ����������������� �����

public class TS_5_4 : MonoBehaviour          // ������, ����������� � �������-�����, ����������� �� ���������� ������� TS
{
    // UI-�������� ��� ��������� ������:
    public GameObject T1_dano, T2_btn, Cv_btn; // T1 �� "����", T2 � Cv � �� ��������� ������

    private float T1, T2, Cv;                 // �������� ����������� � Cv

    [SerializeField] private float arrowWidth = 0.1f; // ������ ������� �� ������� (� ������ ������ �� ������������)

    private LineChart chart;                 // ������ �� ��������� LineChart (������)

    // �������������� ������ � ����� � ������� �� ������
    private float GetFloat(string stringValue, float defaultValue)
    {
        float result = defaultValue;
        float.TryParse(stringValue, out result);
        return result;
    }

    // ��������� ���������� �������� �� ������ � ����������� Info
    private float GetFloatFromBtn(GameObject btn)
    {
        return btn.GetComponentInChildren<Info>().number;
    }

    // ��������� ���������� �������� �� TMP_InputField (���� ������������)
    private float GetFloatFromDano(GameObject z)
    {
        return GetFloat(z.GetComponentInChildren<TMP_InputField>().text, 0.0f);
    }

    // ���������� ����� �� ����������� ����� �������
    private void AddLabelStyle(Line series, int index, string label)
    {
        var dataPoint = series.data[index];                     // �������� ������ ����� �� �����
        var labelStyle = dataPoint.EnsureComponent<LabelStyle>(); // ������ ����� �����, ���� ��� ��� ���
        labelStyle.formatter = label;                           // ������������� ����� �����
        labelStyle.offset = new Vector3(10, 15, 0);             // �������� ����� ������������ �����
    }

    // ��������, �������� �� ����� �������� (�� NaN, �� �������������, �� ����)
    private bool isValidFloat(float num)
    {
        return (num != Mathf.Infinity && num != 0 && !float.IsNaN(num));
    }

    // Start ���������� ��� ������� �����
    void Start()
    {
        chart = GetComponent<LineChart>();                      // �������� ��������� LineChart
        chart.GetChartComponent<Title>().text = "TS-��������� (��������� �������)"; // ��������� �������

        if (chart == null)
        {
            Debug.LogError("LineChart ��������� �� ������!");   // ��������, ���� ������ �� ������
            return;
        }

        var xAxis = chart.GetChartComponent<XAxis>();           // �������� ��� X
        var yAxis = chart.GetChartComponent<YAxis>();           // �������� ��� Y

        // ��������� ��� X
        xAxis.type = Axis.AxisType.Value;
        xAxis.axisLine.showArrow = true;                        // ���������� ������� �� ���
        xAxis.axisName.name = "S, ��/(ʷ����)";                 // �������� ��� X (��������)
        xAxis.splitNumber = 6;                                  // ���������� ������� �� ���
        xAxis.axisLabel.formatter = "{value:F3}";               // ������ ������ ��������

        // ��������� ��� Y
        yAxis.type = Axis.AxisType.Value;
        yAxis.axisLine.showArrow = true;
        yAxis.axisName.name = "T, �";                           // �������� ��� Y (�����������)
        yAxis.axisLabel.formatter = "{value}";
        yAxis.splitNumber = 6;

        // �����������: ����� ���������� ������� �������� (�������������� ��� �������������)
        //GeneratePVGraph();
    }

    // ����� ��������� ������� (TS-��������� ���������� ��������)
    void GeneratePVGraph()
    {
        chart.RemoveData(); // �������� ��� ������ ������ � �������

        // ������ ����� ����� �����
        var series = chart.AddSerie<Line>("��������� �������");

        series.symbol.type = SymbolType.Arrow; // ������ ������� ��� �����������
        series.symbol.gap = 1;                 // �������� ����� ���������

        // �������� �������� �� UI
        T1 = GetFloatFromDano(T1_dano);       // T1 �� ���� �����
        T2 = GetFloatFromBtn(T2_btn);         // T2 �� ��������� ������
        Cv = GetFloatFromBtn(Cv_btn);         // Cv �� ��������� ������

        // �������� ���������� ���������� ������
        if (!isValidFloat(T2 - T1) || !isValidFloat(Cv)) return;

        List<Vector2> points = new List<Vector2>(); // ������ ����� ��� ����������

        // ��������� ������ 20 ������� ����� T1 � T2
        for (float T = T1; T <= T2; T += (T2 - T1) / 20)
        {
            float S = Cv * Mathf.Log(T / T1); // ������� ��� ��������� ��������: S = Cv * ln(T / T1)
            points.Add(new Vector2(S, T));    // ��������� ����� (��������, �����������)
        }

        // ��������� ������ ����� � ������
        foreach (var point in points)
        {
            series.AddData(point.x, point.y);
        }

        // ��������� ����� "1" � "2" � ������ � ����� �������
        AddLabelStyle(series, 0, "1");
        AddLabelStyle(series, series.data.Count - 1, "2");
    }

    // ����� ��� ����������� ������� (����� �������� � ������ � UI)
    public void Replot()
    {
        try { GeneratePVGraph(); } // ���������� ����� ���������
        catch { }                  // ��� ��������� ������ � ������������� ����� ��� ���������
    }
}
