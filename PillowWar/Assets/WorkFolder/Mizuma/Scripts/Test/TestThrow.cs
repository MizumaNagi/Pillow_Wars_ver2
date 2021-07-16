using UnityEngine;

// ��������I�u�W�F�N�g�ɂ���
public class TestThrow : MonoBehaviour
{
    private float throwForcePow = 1200f;
    public int angle;

    private void Update()
    {
        // 30�t���[����1�񐶐�
        if (Time.frameCount % 30 == 0)
        {
            // ����āA���W�𐶐����ɍ��킹�āA�K�v�ȃR���|�[�l���g�t���āA��΂�
            GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);

            obj.transform.position = transform.position;

            obj.AddComponent<AutoDestroy>();
            Rigidbody rb = obj.AddComponent<Rigidbody>();

            Vector3 angleVec = new Vector3(0, Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad));
            rb.AddForce((transform.forward.normalized + angleVec.normalized) * throwForcePow);
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