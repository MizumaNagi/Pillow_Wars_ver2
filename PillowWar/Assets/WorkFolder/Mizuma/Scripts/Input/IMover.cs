using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IMover
{
    void Move(Vector3 movVec);
    void ViewMove(Vector3 viewMovVec);
    void Jump(Vector3 jumpVec);
    void PillowThrow();
}
