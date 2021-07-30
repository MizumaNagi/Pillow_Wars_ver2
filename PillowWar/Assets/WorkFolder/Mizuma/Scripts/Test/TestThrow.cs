using UnityEngine;

// ��������I�u�W�F�N�g�ɂ���
public class TestThrow : MonoBehaviour
{
    private float throwForcePow = 1200f;
    public int angle;
    public float missVec;

    private void Update()
    {
        // 30�t���[����1�񐶐�
        if (Time.frameCount % 15 == 0)
        {
            // ����āA���W�𐶐����ɍ��킹�āA�K�v�ȃR���|�[�l���g�t���āA��΂�
            GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);

            obj.transform.position = transform.position;

            obj.AddComponent<AutoDestroy>();
            Rigidbody rb = obj.AddComponent<Rigidbody>();

            Quaternion forwardRotation = transform.rotation;
            Vector3 angleVec = new Vector3(0, Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad)).normalized;
            Vector3 rndVec = new Vector3(Random.Range(-missVec, missVec), Random.Range(-missVec, missVec), Random.Range(-missVec, missVec));

            rb.AddForce(forwardRotation * (angleVec + rndVec) * throwForcePow);
        }
    }
}

// �������ꂽ�I�u�W�F�N�g�ɂ���
public class AutoDestroy : MonoBehaviour
{
    // �����艺�ɉ���������j������
    private float deathPosY = -30f;

    private void Update()
    {
        if (transform.position.y < deathPosY) Destroy(gameObject);
    }
}