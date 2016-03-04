using UnityEngine;
using System.Collections;

public class OnEnableEvent : MonoBehaviour
{
    public GameObject Xiayibu;
    void OnEnable()
    {
        if (Xiayibu.activeSelf == true)
        {
            Xiayibu.SetActive(false);
        }
    }
}
