using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Create_Scrollbar : MonoBehaviour
{
     void Start()
    {
        transform.GetComponent<Scrollbar>().value = 1;
    } 
}
