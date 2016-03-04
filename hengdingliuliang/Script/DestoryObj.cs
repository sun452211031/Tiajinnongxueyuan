using UnityEngine;
using System.Collections;

public class DestoryObj : MonoBehaviour
{
    public float DesTime;
    void Start()
    {
        Invoke("DesFunction", DesTime);
    }
    private void DesFunction()
    {
        Destroy(gameObject);
    }
}
