using UnityEngine;
using System.Collections;

public class InterfaceManager : MonoBehaviour
{
    public void Kaishishiyan()
    {
        Application.LoadLevel("Shiyanchangjing");
    }
    public void Tuichushiyan()
    {
#if UNITY_STANDALONE
        Application.Quit();
#endif
#if UNITY_WEBPLAYER
        Debug.Log("关闭当前网页");
#endif
    }
}
