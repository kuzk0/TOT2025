using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowClose_window : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject window;
    [SerializeField] private bool on;
    public void gg()
    {
        window.SetActive(on);
    }
}
