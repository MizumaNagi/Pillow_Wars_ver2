using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StageScrollRect : ScrollRect
{
    //1ページの幅
    private float pageWidth;
    //前回のページIndex、最も左を0とする。
    private int prevPageIndex = 0;

    protected override void Awake()
    {
        base.Awake();

        GridLayoutGroup grid = content.GetComponent<GridLayoutGroup>();
        //1ページの幅を取得する。
        pageWidth = grid.cellSize.x + grid.spacing.x;
    }

    //ドラッグを開始した時
    public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);
    }

    //ドラッグを終了した時
    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);

        //ドラッグを終了した時、スクロールをピタッと止める。
        StopMovement();

        //スナップさせるページを決める。
        int pageIndex = Mathf.RoundToInt(content.anchoredPosition.x / pageWidth);

        //ページが変わっていない且つ、素早くドラッグした場合、ドラッグ量調節。
        if (pageIndex == prevPageIndex && Mathf.Abs(eventData.delta.x) >= 5)
        {
            pageIndex += (int)Mathf.Sign(eventData.delta.x);
        }

        //contentをスクロール位置を決定する。
        float destX = pageIndex * pageWidth;
        content.anchoredPosition = new Vector2(destX, content.anchoredPosition.y);

        //
    }
}
