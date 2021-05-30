using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public GameObject target = null;

    float startFOV = 110;
    float endFOV = 50;
    float goalTime = 3.0f;
    float deltaTime;

    void Start()
    {
        //MainCamera�擾����B
        Camera camera = Camera.main;
    }

    void Update()
    {
        //�J�������^�[�Q�b�g�Ɍ��������鏈���B
        this.transform.LookAt(target.transform);
        //�o�ߎ��Ԋi�[
        deltaTime += Time.deltaTime;
        //���s���v�Z
        var fov = deltaTime / goalTime;
        //�J�������������Y�[�������鏈���B
        Camera.main.fieldOfView = Mathf.Lerp(startFOV, endFOV, fov);
    }
}
