using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonOnSelect : MonoBehaviour
{
    private void Update()
    {
        Debug.Log(Selection.activeGameObject);
    }

    public void ButtonSelectEnter(Button b)
    {
        Debug.Log(b.gameObject.name + " :‘I‘ð");
    }

    public void ButtonSelectExit(Button b)
    {
        Debug.Log(b.gameObject.name + " :‘I‘ðŠO");
    }
}
