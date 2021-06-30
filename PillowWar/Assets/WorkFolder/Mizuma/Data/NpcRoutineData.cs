using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObject/NpcRoutineData", order = 4)]
public class NpcRoutineData : ScriptableObject
{
    public GameObject targetMark;
    public Vector3 stageRange;

    [Range(1f, 5f)] public float searchNavMeshRange;        // �����_�����W������s�\��NavMesh��T���o���͈�
    public float distanceToEnemy;                           // �G�Ƃ̐ړG����
    public float warRangeToEnemy;                           // �G�Ƃ̐퓬����
    public float maxSearchAngle;                            // ����
    public float startGoBedRemHpPercent;                    // �x�b�h���荞�݃C�x���g���J�n����HP����
    public float minStartGoBedPercent;                      // �x�b�h���荞�݃C�x���g�̍Œ���s��
    public float shootingDamageChgTargetRoutinePercent;     // �ˌ����ɍU�����󂯂��ۂɃ^�[�Q�b�g��ύX����\��
    public float shootingDamageChgEscapeRoutinePercent;     // �ˌ����ɍU�����󂯂��ۂɃ^�[�Q�b�g���瓦������\��
    public float shootingDamageChgEscapeAndJumpRoutinePercent; // �ˌ����ɍU�����󂯂��ۂɃ^�[�Q�b�g���璵�˂Ȃ��瓦������\��
}
