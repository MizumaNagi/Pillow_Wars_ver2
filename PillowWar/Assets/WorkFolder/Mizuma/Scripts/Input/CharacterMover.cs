using System.Collections;
using UnityEngine;
using System.Threading.Tasks;

// キャラクター移動クラス
public class CharacterMover
{
    public void Move(Vector3 _movVec, CharacterData data)
    {
        Transform movTransform = data.myBodyTransform;
        Rigidbody movRb = data.myBodyRigidbody;

        Vector3 movVec;
        if(data.buffInfo.remainFastSpdTime > 0) movVec = movTransform.rotation * _movVec * InputManager.Instance.moveData.moveForce * GameManager.Instance.itemData.fastMoveSpdMulti;
        else movVec = movTransform.rotation * _movVec * InputManager.Instance.moveData.moveForce;

        movRb.AddForce(movVec * Time.deltaTime);

        if(data.isSquat)
        {
            if (movRb.velocity.magnitude > InputManager.Instance.moveData.squatMoveSpdLimit)
            {
                movRb.velocity /= 1.1f;
            }
        }
        else
        {
            if (movRb.velocity.magnitude > InputManager.Instance.moveData.walkMoveSpdLimit)
            {
                movRb.velocity /= 1.1f;
            }
        }
    }

    public void ViewMove(Vector3 _viewMovVec, CharacterData data)
    {
        Vector3 rotVec = _viewMovVec * InputManager.Instance.moveData.viewMoveSpd * Time.deltaTime;

        data.myBodyTransform.Rotate(0, rotVec.x, 0);
        data.myCameraTransform.Rotate(-rotVec.z, 0, 0);
    }

    public void Dash(CharacterData data, bool isDash)
    {
        data.isDash = isDash;
    }

    public void Jump(CharacterData data)
    {
        data.canJump = false;
        data.myBodyRigidbody.AddForce(0, InputManager.Instance.moveData.jumpForce, 0);
    }

    public async void PillowThrow(CharacterData data)
    {
        data.animatorManager.TriggerThrow();

        data.isHavePillow = false;
        await DelayThrow(data);
    }

    private async Task DelayThrow(CharacterData data, float delayTime = 0.4f)
    {
        // 待機
        await Task.Delay((int)(delayTime * 1000));

        // 投射角度 (バフON/OFF)
        float angle;
        if (data.buffInfo.remainFastThrowTime > 0) angle = InputManager.Instance.moveData.throwAngleInBuff;
        else angle = InputManager.Instance.moveData.throwAngle;

        // 誤差範囲 (PL/NPC)
        float missVec;
        if (data.isNpc) missVec = InputManager.Instance.moveData.npcThrowMissVec;
        else
        {
            if(data.isZoom) missVec = InputManager.Instance.moveData.throwMissVec / 3f;
            else missVec = InputManager.Instance.moveData.throwMissVec;
        }

        // 枕を有効化
        data.pillowCollider.enabled = true;

        // 枕を視線目前に出す
        data.myPillowTransform.position = data.myCameraTransform.position + data.myCameraTransform.forward * 0.6f;

        // 枕を枕管理オブジェクトの子にする
        data.myPillowTransform.SetParent(PlayerManager.Instance.PillowParent, true);
        // CT更新
        data.remainthrowCT = GameManager.Instance.ruleData.pillowThrowCT;
        // 枕固定化解除
        data.myPillowRigidbody.isKinematic = false;

        // 枕を飛ばす向き
        Quaternion forwardRotation = data.myBodyTransform.rotation;
        Vector3 angleVec = new Vector3(0, Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad)).normalized;
        Vector3 rndVec = new Vector3(Random.Range(-missVec, 0), Random.Range(-missVec, 0), Random.Range(-missVec, 0));

        // 枕に力を加える
        if(data.buffInfo.remainFastThrowTime > 0) data.myPillowRigidbody.AddForce(forwardRotation * (angleVec + rndVec) * InputManager.Instance.moveData.throwForce * GameManager.Instance.itemData.upThrowMulti, ForceMode.Acceleration);
        else data.myPillowRigidbody.AddForce(forwardRotation * (angleVec + rndVec) * InputManager.Instance.moveData.throwForce, ForceMode.Acceleration);

        // 枕サイズ上昇のバフ効果を反映させる
        if (data.buffInfo.remainBigPillowTime > 0) data.myPillowTransform.localScale *= 2;
    }

    public void ToNonADS(CharacterData data)
    {
        data.isZoom = false;
        if (data.myCamera.fieldOfView >= InputManager.Instance.moveData.maxFOV) { return; }

        float frameChgValueADS = InputManager.Instance.moveData.fovChangeSpd * Time.deltaTime;
        if (data.myCamera.fieldOfView + frameChgValueADS > InputManager.Instance.moveData.maxFOV)
        {
            data.myCamera.fieldOfView = InputManager.Instance.moveData.maxFOV;
        }
        else
        {
            data.myCamera.fieldOfView += frameChgValueADS;
        }
    }

    public void ToADS(CharacterData data)
    {
        data.isZoom = true;
        if (data.myCamera.fieldOfView <= InputManager.Instance.moveData.minFOV) { return; }

        float frameChgValueADS = InputManager.Instance.moveData.fovChangeSpd * Time.deltaTime;
        if (data.myCamera.fieldOfView - frameChgValueADS < InputManager.Instance.moveData.minFOV)
        {
            data.myCamera.fieldOfView = InputManager.Instance.moveData.minFOV;
        }
        else
        {
            data.myCamera.fieldOfView -= frameChgValueADS;
        }
    }

    public void InteractBed(CharacterData data, bool isInBed, Vector3 bedPos)
    {
        if (data.bedStatus == null)
        {
            Debug.LogWarning("data.bedStatus == null \nオブジェクトが破棄されているか確認");
        }

        data.isInBed = isInBed;
        if (isInBed == true)
        {
            data.bedStatus.ChangeBedActive(false, data);
            data.myBodyTransform.localPosition = bedPos;
            AudioManager.Instance.SEPlay(SEName.InBed);
        }
        else
        {
            data.bedStatus.ChangeBedActive(true, null);
            data.bedStatus = null;
        }
        data.HideCharacter(isInBed);
    }

    public void Squat(CharacterData data, bool isSquated)
    {
        if (isSquated)
        {
            data.myCameraTransform.localPosition = InputManager.Instance.moveData.standingCameraPos;
            Debug.Log("しゃがみ解除");
        }
        else
        {
            data.myCameraTransform.localPosition = InputManager.Instance.moveData.squatingCameraPos;
            Debug.Log("しゃがみ開始");
        }

        data.isSquat = !isSquated;
    }

    // 参考:
    private Vector3 CalculateVelocity(Vector3 pointA, Vector3 pointB, float angle)
    {
        // 射出角をラジアンに変換
        float rad = angle * Mathf.PI / 180;

        // 水平方向の距離x
        float x = Vector2.Distance(new Vector2(pointA.x, pointA.z), new Vector2(pointB.x, pointB.z));

        // 垂直方向の距離y
        float y = pointA.y - pointB.y;

        // 斜方投射の公式を初速度について解く
        float speed = Mathf.Sqrt(-Physics.gravity.y * Mathf.Pow(x, 2) / (2 * Mathf.Pow(Mathf.Cos(rad), 2) * (x * Mathf.Tan(rad) + y)));

        if (float.IsNaN(speed))
        {
            // 条件を満たす初速を算出できなければVector3.zeroを返す
            return Vector3.zero;
        }
        else
        {
            return (new Vector3(pointB.x - pointA.x, x * Mathf.Tan(rad), pointB.z - pointA.z).normalized * speed);
        }
    }
}