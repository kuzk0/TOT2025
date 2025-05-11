using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using XCharts.Runtime;
using System.Security.Cryptography;
using TMPro;
using static UnityEngine.GraphicsBuffer;

public class Diagrams : MonoBehaviour
{
    //btn - first in line of calculations
    //dano - element from dano list
    public GameObject T1_dano, T2_btn, V_dano, mu_dano, Rv_btn;
    // Start is called before the first frame update
    private float T1, T2, V, mu, Rv;

    private LineChart chart; // ������ �� ������


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
        // �������� ����� �� �������
        var dataPoint = series.data[index];

        // ������� ����� ����� � ������������� ���������
        var labelStyle = dataPoint.EnsureComponent<LabelStyle>();
        labelStyle.formatter = label; // ����� �����
        labelStyle.offset = new Vector3(10, 15, 0);

    }
    private bool isValidFloat(float num)
    {
        return (num != Mathf.Infinity && num != 0 && !float.IsNaN(num));
    }
    void Start()
    {

        chart = GetComponent<LineChart>(); // �������� ��������� �������
        chart.GetChartComponent<Title>().text = "PV-��������� (��������� �������)";
        if (chart == null)
        {
            Debug.LogError("LineChart ��������� �� ������!");
            return;
        }
        var xAxis = chart.GetChartComponent<XAxis>();
        var yAxis = chart.GetChartComponent<YAxis>();

        // ��������� ��� � ���������
        xAxis.type = Axis.AxisType.Value;
        xAxis.axisName.name = "V, �^3";
        xAxis.axisLine.showArrow = true;
        yAxis.type = Axis.AxisType.Value;
        yAxis.axisName.name = "P, ���";
        yAxis.axisLine.showArrow = true;

        xAxis.splitNumber = 6; // ������ �������� �� ��� X
        //xAxis.axisLabel.rotate = 30; // ��������� ������� �� 30 ��������
        //xAxis.axisLabel.interval = 1; // ������� ����� ���� (���� 0 � �������� ���)

        xAxis.axisLabel.formatter = "{value:F3}";


        yAxis.axisLabel.formatter = "{value}"; // ������� ��� ����������������� ����
        yAxis.splitNumber = 6; // ���������� ���������� �������

        //GeneratePVGraph();
    }

    void GeneratePVGraph()
    {
        chart.RemoveData(); // ������� ������ ����� ����������

        // ������ ����� ������ ��� ���������� ��������
        var series = chart.AddSerie<Line>("��������� �������");

        series.symbol.type = SymbolType.Arrow;
        series.symbol.gap = 1;

        T1 = GetFloatFromDano(T1_dano);
        T2 = GetFloatFromBtn(T2_btn);
        V = GetFloatFromDano(V_dano);
        mu = GetFloatFromDano(mu_dano);
        Rv = GetFloatFromBtn(Rv_btn);

        if (!isValidFloat(T2 - T1) || !isValidFloat(V) || !isValidFloat(Rv) || !isValidFloat(mu)) return;

        // ��������� ������ (�������� P ��� ���������� ������ V)
        // ���������� �����
        List<Vector2> points = new List<Vector2>();
        for (float T = T1; T <= T2; T += (T2 - T1) / 20) // ��������� �����������, ����� 20 �����
        {
            float P = (mu * Rv * T) / V / 1000f; // ��������� ��������� ��������� ���� (PV = muRT)
            points.Add(new Vector2(V, P));
        }

        // ��������� ����� � �����
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
