using UnityEngine;

// ��������I�u�W�F�N�g�ɂ���
public class TestThrow : MonoBehaviour
{
    private float throwForcePow = 1200f;
    public int angle;

    public bool forwardNormalized;
    public bool angleVecNormalized;
    public bool allNormalized;

    public Vector3 angleVec;
    public Vector3 forwardVec;
    public Vector3 normalizedVec;

    public Vector3 dynamicVec;

    private void Update()
    {
        angleVec = new Vector3(0, Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad));
        forwardVec = transform.forward;
        normalizedVec = (angleVec + forwardVec).normalized;

        //dynamicNormalized = forwardVec.normalized;

        // 30�t���[����1�񐶐�
        if (Time.frameCount % 30 == 0)
        {
            // ����āA���W�𐶐����ɍ��킹�āA�K�v�ȃR���|�[�l���g�t���āA��΂�
            GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);

            obj.transform.position = transform.position;

            obj.AddComponent<AutoDestroy>();
            Rigidbody rb = obj.AddComponent<Rigidbody>();

            //rb.AddForce((angleVec + forwardVec).normalized * throwForcePow);
            rb.AddForce(dynamicVec * throwForcePow);
            //rb.AddForce(obj.transform.forward + new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0) * throwForcePow);

            /*
            if (allNormalized)
            {
                if (forwardNormalized)
                {
                    if (angleVecNormalized)
                        rb.AddForce((transform.forward.normalized + angleVec.normalized).normalized * throwForcePow);
                    else
                        rb.AddForce((transform.forward.normalized + angleVec).normalized * throwForcePow);
                }
                else
                {
                    if (angleVecNormalized)
                        rb.AddForce((transform.forward + angleVec.normalized).normalized * throwForcePow);
                    else
                        rb.AddForce((transform.forward + angleVec).normalized * throwForcePow);
                }
            }
            else
            {
                if (forwardNormalized)
                {
                    if (angleVecNormalized)
                        rb.AddForce((transform.forward.normalized + angleVec.normalized) * throwForcePow);
                    else
                        rb.AddForce((transform.forward.normalized + angleVec) * throwForcePow);
                }
                else
                {
                    if (angleVecNormalized)
                        rb.AddForce((transform.forward + angleVec.normalized) * throwForcePow);
                    else
                        rb.AddForce((transform.forward + angleVec) * throwForcePow);
                }
            }
            */

            //rb.AddForce(transform.forward.normalized * throwForcePow);
            //rb.AddForce(angleVec * throwForcePow);
            rb.AddForce((transform.forward.normalized + angleVec.normalized).normalized * throwForcePow);
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