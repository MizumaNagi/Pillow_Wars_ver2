using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public GameObject target = null;
    public Vector3 startPos;
    public Vector3 endPos;

    float startFOV = 69;
    float endFOV = 3.7f;
    float goalTime = 2.7f;
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

        float currentX = Mathf.Lerp(startPos.x, endPos.x, fov);
        float currentY = Mathf.Lerp(startPos.y, endPos.y, fov);
        float currentZ = Mathf.Lerp(startPos.z, endPos.z, fov);
        target.transform.localPosition = new Vector3(currentX, currentY, currentZ);
    }
}
