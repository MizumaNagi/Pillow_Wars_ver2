using System.Collections;
using UnityEngine;

// 生成するオブジェクトにつける
public class TestThrow : MonoBehaviour
{
    private float throwForcePow = 1200f;
    public int angle;

    [Header("直でベクトル変更")]
    [Range(0, 1f)] public float dynamicRotX;
    [Range(0, 1f)] public float dynamicRotY;
    [Range(0, 1f)] public float dynamicRotZ;

    private void Update()
    {
        // 30フレームに1回生成
        if (Time.frameCount % 30 == 0)
        {
            // 作って、座標を生成元に合わせて、必要なコンポーネント付けて、飛ばす
            GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);

            obj.transform.position = transform.position;

            obj.AddComponent<AutoDestroy>();
            Rigidbody rb = obj.AddComponent<Rigidbody>();

            //Vector3 angleVec = new Vector3(0, Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad));
            //rb.AddForce(obj.transform.forward + angleVec * throwForcePow);

            rb.AddForce(new Vector3(dynamicRotX,dynamicRotY,dynamicRotZ) * throwForcePow);
        }
    }
}

// 生成されたオブジェクトにつける
public class AutoDestroy : MonoBehaviour
{
    // これより下に下がったら破棄する
    private float deathPosY = -30f;

    private void Update()
    {
        if (transform.position.y < deathPosY) Destroy(gameObject);
    }
}