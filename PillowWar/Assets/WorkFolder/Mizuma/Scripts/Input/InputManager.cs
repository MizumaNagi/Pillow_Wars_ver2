using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : SingletonMonoBehaviour<InputManager>
{
    [SerializeField] private InputData[] player;
    [SerializeField] private MoveData moveData;
}
