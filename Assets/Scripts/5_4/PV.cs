using System.Collections.Generic;
using UnityEngine;
using XCharts.Runtime;              // ����������� ���������� XCharts ��� ���������� ��������
using TMPro;                        // ��� ������� � ����� ����� TMP_InputField

public class PV_5_4 : MonoBehaviour // ������ ��� ���������� PV-������� � Unity
{
    //btn - first in line of calculations
    //dano - element from dano list
    // ������� UI, ������ ���������� �������� ������
    public GameObject T1_dano, T2_btn, V_btn, mu_dano, Rv_btn;

    // ���������� ��� �������� �������� ��������
    private float T1, T2, V, mu, Rv;

    private LineChart chart;       // ������ �� ��������� �������

    // ���������� �������������� ������ � float
    private float GetFloat(string stringValue, float defaultValue)
    {
        float result = defaultValue;
        float.TryParse(stringValue, out result);
        return result;
    }

    // ��������� �������� �� ���������� Info �� ������
    private float GetFloatFromBtn(GameObject btn)
    {
        return btn.GetComponentInChildren<Info>().number;
    }

    // ��������� �������� �� TMP_InputField, �������� �� "����"
    private float GetFloatFromDano(GameObject z)
    {
        return GetFloat(z.GetComponentInChildren<TMP_InputField>().text, 0.0f);
    }

    // ���������� ���������� ����� �� ����� �������
    private void AddLabelStyle(Line series, int index, string label)
    {
        var dataPoint = series.data[index];
        var labelStyle = dataPoint.EnsureComponent<LabelStyle>();
        labelStyle.formatter = label;
        labelStyle.offset = new Vector3(10, 15, 0);
    }

    // ��������, ��� ����� ��������� ��� ����������
    private bool isValidFloat(float num)
    {
        return (num != Mathf.Infinity && num != 0 && !float.IsNaN(num));
    }

    // ���������� ��� ������� �����
    void Start()
    {
        chart = GetComponent<LineChart>();                            // �������� ��������� LineChart �� �������
        chart.GetChartComponent<Title>().text = "PV-��������� (��������� �������)"; // ������������� ���������

        if (chart == null)
        {
            Debug.LogError("LineChart ��������� �� ������!");         // ������, ���� ��������� ������� �� ������
            return;
        }

        // �������� ���������� ����
        var xAxis = chart.GetChartComponent<XAxis>();
        var yAxis = chart.GetChartComponent<YAxis>();

        // ��������� ��� X (�����)
        xAxis.type = Axis.AxisType.Value;
        xAxis.axisName.name = "V, �^3";           // �������� ���
        xAxis.axisLine.showArrow = true;          // ���������� �������
        xAxis.splitNumber = 6;                    // ���������� �������
        xAxis.axisLabel.formatter = "{value:F3}"; // ������ ����� � 3 �������

        // ��������� ��� Y (��������)
        yAxis.type = Axis.AxisType.Value;
        yAxis.axisName.name = "P, ���";           // �������� ���
        yAxis.axisLine.showArrow = true;
        yAxis.splitNumber = 6;
        yAxis.axisLabel.formatter = "{value}";

        // ����� ��������� ������� ����� �������� (�������� ���������� ������� ����� Replot)
        //GeneratePVGraph();
    }

    // ����� ��������� PV-�������
    void GeneratePVGraph()
    {
        chart.RemoveData(); // ������� ���������� ������ � �������

        // ������ ����� ����� �� �������
        var series = chart.AddSerie<Line>("��������� �������");
        series.symbol.type = SymbolType.Arrow;  // ������ ������� (���������� �����������)
        series.symbol.gap = 1;                  // �������� ����� ���������

        // �������� ������ �� UI
        T1 = GetFloatFromDano(T1_dano);
        T2 = GetFloatFromBtn(T2_btn);
        V = GetFloatFromBtn(V_btn);
        mu = GetFloatFromDano(mu_dano);
        Rv = GetFloatFromBtn(Rv_btn);

        // �������� ���������� ��������
        if (!isValidFloat(T2 - T1) || !isValidFloat(V) || !isValidFloat(Rv) || !isValidFloat(mu)) return;

        List<Vector2> points = new List<Vector2>(); // ������ ����� �������

        // ���������� �����: ����������� �� T1 �� T2 � �����, ���� 20 �����
        for (float T = T1; T <= T2; T += (T2 - T1) / 20)
        {
            float P = (mu * Rv * T) / V / 1000f; // ��������� ���������: P = mu*Rv*T / V (����� �� 1000 ��� �������� � ���)
            points.Add(new Vector2(V, P));       // ��������� ����� (V, P) � �.�. V ���������, ��� ������������ �����
        }

        // ���������� ����� � ������
        foreach (var point in points)
        {
            series.AddData(point.x, point.y);
        }

        // ����� ������ � ����� ��������
        AddLabelStyle(series, 0, "1");
        AddLabelStyle(series, series.data.Count - 1, "2");
    }

    // ����� ��� ����������� ������� � ����� �������� ������� �� UI
    public void Replot()
    {
        try { GeneratePVGraph(); }
        catch { }
    }
}
