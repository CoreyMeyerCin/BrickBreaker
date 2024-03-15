using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    public GameObject SkillCanvasObject;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowSkillCanvas()
    {
        Debug.Log("ShowSkillCanvas TRUE");
        SkillCanvasObject.SetActive(true);
    }
    public void HideSkillCanvas()
    {
        Debug.Log("ShowSkillCanvas FALSE");
        SkillCanvasObject.SetActive(false);
    }
}
