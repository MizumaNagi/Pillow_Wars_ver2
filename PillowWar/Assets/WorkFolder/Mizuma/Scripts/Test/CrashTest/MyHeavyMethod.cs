using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyHeavyMethod : MonoBehaviour
{
    public void Awake()
    {
        isPlayHeavyMethod = CrashTest.isAddHeavyMethodClass;
    }

    public bool isPlayHeavyMethod = false;

    private int loop = 10;

    private int frameCnt;
    private int movSwitchVecPerFrame = 30;
    private bool isMoveRight = true;

    private float movSpd = 0.5f;

    void Update()
    {
        if(isPlayHeavyMethod)
        {
        for (int i = 0; i < loop; i++)
            {
                gameObject.AddComponent<Rigidbody>();
                Destroy(GetComponent<Rigidbody>());
            }
        }

        transform.Translate(1 * BoolToInt(isMoveRight) * movSpd, 0, 0);

        if (frameCnt % movSwitchVecPerFrame == 0)
        {
            isMoveRight = !isMoveRight;
        }

        frameCnt++;
    }

    int BoolToInt(bool b)
    {
        if (b) return 1;
        else return -1;
    }
}
