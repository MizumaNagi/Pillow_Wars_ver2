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
        //MainCamera取得する。
        Camera camera = Camera.main;
    }

    void Update()
    {
        //カメラがターゲットに向き続ける処理。
        this.transform.LookAt(target.transform);
        //経過時間格納
        deltaTime += Time.deltaTime;
        //遂行率計算
        var fov = deltaTime / goalTime;
        //カメラをゆっくりズームさせる処理。
        Camera.main.fieldOfView = Mathf.Lerp(startFOV, endFOV, fov);

        float currentX = Mathf.Lerp(startPos.x, endPos.x, fov);
        float currentY = Mathf.Lerp(startPos.y, endPos.y, fov);
        float currentZ = Mathf.Lerp(startPos.z, endPos.z, fov);
        target.transform.localPosition = new Vector3(currentX, currentY, currentZ);
    }
}
