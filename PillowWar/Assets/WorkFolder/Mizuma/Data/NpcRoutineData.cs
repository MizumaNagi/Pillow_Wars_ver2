using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObject/NpcRoutineData", order = 4)]
public class NpcRoutineData : ScriptableObject
{
    public GameObject targetMark;
    public Vector3 negativeStageRange;
    public Vector3 positiveStageRange;

    [Range(1f, 5f)] public float searchNavMeshRange;        // �����_�����W������s�\��NavMesh��T���o���͈�
    [Range(1, 10)] public int searchMaxCount;               // �����_�����W��������Ȃ������ۂɍő剽��J��Ԃ��������邩
    public float distanceToEnemy;                           // �G�Ƃ̐ړG����
    public float warRangeToEnemy;                           // �G�Ƃ̐퓬����
    public float maxSearchAngle;                            // ����
    public float failedInBedPercent;                        // �ڂ̑O�̋�x�b�h�ɓ��铮������s����\��
    public float startGoBedRemHpPercent;                    // �x�b�h���荞�݃C�x���g���J�n����HP����
    public float minStartGoBedPercent;                      // �x�b�h���荞�݃C�x���g�̍Œ���s��
    public float shootingDamageChgTargetRoutinePercent;     // �ˌ����ɍU�����󂯂��ۂɃ^�[�Q�b�g��ύX����\��
    public float shootingDamageChgEscapeRoutinePercent;     // �ˌ����ɍU�����󂯂��ۂɃ^�[�Q�b�g���瓦������\��
    public float shootingDamageChgEscapeAndJumpRoutinePercent; // �ˌ����ɍU�����󂯂��ۂɃ^�[�Q�b�g���璵�˂Ȃ��瓦������\��
    public float minStartGoBedTime;                         // �x�b�h�֌������ŏ��P���c�莞��
    public float maxStartGoBedTime;                         // �x�b�h�֌������ő�P���c�莞��
}
