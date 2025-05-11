using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Load_Scene_gg : MonoBehaviour
{
    public int gg;
    public void Load_new_Scene()
    {
        SceneManager.LoadScene(gg);
    }
}
