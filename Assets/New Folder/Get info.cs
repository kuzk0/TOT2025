using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Getinfo : MonoBehaviour
{

    public void GetInformation(float  info,bool nul)
    {
        if (nul)
        {
            transform.GetComponentInChildren<TextMeshProUGUI>().text = info.ToString();
        }else { transform.GetComponentInChildren<TextMeshProUGUI>().text = null; }
    }
}
