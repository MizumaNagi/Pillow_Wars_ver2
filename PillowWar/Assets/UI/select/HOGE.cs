using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HOGE : MonoBehaviour
{
    [SerializeField] private InputData[] playerInputs;
    private CharacterMover characterMover = new CharacterMover();

    private int enableConNum = 0;

    private void Update()
    {
        // ����������ĂȂ������疳������
        if (Input.anyKey == false) return;

        if(Input.GetButton(playerInputs[enableConNum].Ok))
        {
            Debug.Log("OK �{�^������");
        }

        if (Input.GetButton(playerInputs[enableConNum].Cancel))
        {
            Debug.Log("Cancel �{�^������");
        }
    }
}
