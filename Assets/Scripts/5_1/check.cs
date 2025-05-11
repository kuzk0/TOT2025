using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class check : MonoBehaviour
{
    public GameObject check1,check2;
    // Start is called before the first frame update
   public void checkk()
    {
        if(check1.GetComponent<Info>().number==1.935833f)
        {
            check2.GetComponent<Image>().color = Color.green;
        }else
            check2.GetComponent<Image>().color = Color.red;
    }
}
