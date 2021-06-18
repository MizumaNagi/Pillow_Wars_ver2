using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StageScrollRect : ScrollRect
{
    //1�y�[�W�̕�
    private float pageWidth;
    //�O��̃y�[�WIndex�A�ł�����0�Ƃ���B
    private int prevPageIndex = 0;

    protected override void Awake()
    {
        base.Awake();

        GridLayoutGroup grid = content.GetComponent<GridLayoutGroup>();
        //1�y�[�W�̕����擾����B
        pageWidth = grid.cellSize.x + grid.spacing.x;
    }

    //�h���b�O���J�n������
    public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);
    }

    //�h���b�O���I��������
    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);

        //�h���b�O���I���������A�X�N���[�����s�^�b�Ǝ~�߂�B
        StopMovement();

        //�X�i�b�v������y�[�W�����߂�B
        int pageIndex = Mathf.RoundToInt(content.anchoredPosition.x / pageWidth);

        //�y�[�W���ς���Ă��Ȃ����A�f�����h���b�O�����ꍇ�A�h���b�O�ʒ��߁B
        if (pageIndex == prevPageIndex && Mathf.Abs(eventData.delta.x) >= 5)
        {
            pageIndex += (int)Mathf.Sign(eventData.delta.x);
        }

        //content���X�N���[���ʒu�����肷��B
        float destX = pageIndex * pageWidth;
        content.anchoredPosition = new Vector2(destX, content.anchoredPosition.y);

        //
    }
}
