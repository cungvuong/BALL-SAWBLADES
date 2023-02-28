using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    public Button jumpBtn;
    public Button moveLeftBtn;
    public Button moveRightBtn;
    
    private void Start()
    {
        jumpBtn.onClick.AddListener(() => Player.Instance.Jump());
    }
}
