using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{    
    [SerializeField] private Button PauseButton;

    void OnEnable()
    {
        PauseButton.Select();
    }
}
