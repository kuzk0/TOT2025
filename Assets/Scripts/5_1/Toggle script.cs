using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Togglescript : MonoBehaviour
{
    [SerializeField] private GameObject togle, togle1;
    private bool on=false;
    public void check()
    { 
    togle.GetComponent<Toggle>().interactable = on;
    togle1.GetComponent<Toggle>().interactable = on;
     on =! on;
    }

        
}
