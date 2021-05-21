using UnityEngine;
using System.Text;

public class HitCharacterController : MonoBehaviour
{
    private int objNum;

    private void Start()
    {
        StringBuilder sb = new StringBuilder(gameObject.name);
        sb.Replace("Player","");
        objNum = int.Parse(sb.ToString());
        sb.Clear();
    }

    public void OnCollisionEnter(Collision collison)
    {
        if (collison.gameObject.tag == "Pillow")
        {
            int pillowNum = int.Parse(collison.gameObject.name);
            if (pillowNum == objNum) return;

            PlayerManager.Instance.charaDatas[objNum].Damage();
        }

        if (collison.gameObject.tag == "Ground")
        {
            PlayerManager.Instance.charaDatas[objNum].canJump = true;
        }
    }
}
