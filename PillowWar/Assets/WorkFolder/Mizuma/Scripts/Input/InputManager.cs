using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public InputStatus[] inputStatuses = new InputStatus[4];

    public void Awake()
    {
        for(int i = 0; i < 4; i++)
        {
            inputStatuses[i] = new InputStatus();
        }
    }
}
