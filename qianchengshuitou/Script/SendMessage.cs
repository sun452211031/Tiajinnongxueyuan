using UnityEngine;
using System.Collections;

public class SendMessage : MonoBehaviour
{
    public GameObject Target;
    public string FunctionName;
    public void OnMouseDown()
    {
        Target.SendMessage(FunctionName, SendMessageOptions.DontRequireReceiver);
        Destroy(gameObject);
    }
}
