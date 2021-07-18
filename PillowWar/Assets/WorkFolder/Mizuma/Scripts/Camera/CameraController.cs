using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CameraController : MonoBehaviour
{
    private float arriveTime = 2f;
    private float stayTime = 2.5f;

    private Camera cameraCompo;
    private Transform myPillowTransform;
    public Transform myCharacterTransform;

    private void Start()
    {
        cameraCompo = GetComponent<Camera>();
        myCharacterTransform = transform.parent.transform;
        myPillowTransform = transform.GetChild(0).transform;
    }

    void Update()
    {
        Vector3 rot = transform.localEulerAngles;

        float limitRotY = InputManager.Instance.moveData.limitRotY;

        // 345 >= 15 → o
        // 15 > 180 → x
        if (rot.x > limitRotY && rot.x < 180) rot.x = limitRotY;
        // 180 > 345 → x
        else if (rot.x > 180 && rot.x < 360 - limitRotY) rot.x = -limitRotY;
        
        transform.localEulerAngles = rot;

    }

    [SerializeField] private float deltaTime = 0;
    public IEnumerator StartMoveCorutine(Vector3 endPos, Quaternion endRot, UnityAction callBack)
    {
        deltaTime = 0;
        Vector3 startPos = cameraCompo.transform.position;
        Quaternion startRot = cameraCompo.transform.rotation;

        // 枕,カメラ パージ
        myPillowTransform.SetParent(myCharacterTransform, false);
        cameraCompo.transform.SetParent(PlayerManager.Instance.CameraParent, false);

        while (true)
        {
            yield return null;

            deltaTime += Time.deltaTime;
            float completePercent = deltaTime / arriveTime;

            cameraCompo.transform.position = BezierCurve2D(startPos, Vector3.Lerp(startPos, endPos, 0.5f) + new Vector3(0, 3, 0), endPos, completePercent);

            cameraCompo.transform.rotation = Quaternion.Lerp(startRot, endRot, completePercent * completePercent);

            if (completePercent > 1)
            {
                yield return new WaitForSeconds(stayTime);

                // 枕,カメラ くっつける
                cameraCompo.transform.SetParent(myCharacterTransform, false);
                myPillowTransform.SetParent(transform, false);

                cameraCompo.transform.localPosition = InputManager.Instance.moveData.standingCameraPos;
                cameraCompo.transform.localRotation = Quaternion.identity;
                GameEventScript.Instance.canAction = true;

                yield return new WaitForSeconds(0.5f);
                callBack();
                yield break;
            }
        }
    }

    private Vector3 BezierCurve2D(Vector3 startPos, Vector3 halfPos, Vector3 endPos, float t)
    {
        Vector3 a = Vector3.Lerp(startPos, halfPos, t);
        Vector3 b = Vector3.Lerp(halfPos, endPos, t);
        return Vector3.Lerp(a, b, t);
    }
}
