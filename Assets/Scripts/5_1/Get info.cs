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
            transform.GetComponentInChildren<TEXDraw>().text = info.ToString();
        }else { transform.GetComponentInChildren<TEXDraw>().text = null; }
    }
}
