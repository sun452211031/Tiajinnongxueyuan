using UnityEngine;
using System.Collections;

public class StateManager : MonoBehaviour
{
    void Start()
    {
        State.Step = 1;
    }
}
public class State
{
    #region 实验数据
    public static float h1 = 0;
    public static float h2 = 0;
    public static float Q = 0;
    public static float T = 0;
    public static float T_h = 0;
    public static float V = 0;
    public static float u = 0;
    public static float Re = 0;
    public static float r = 0;
    #endregion
    private static int step;
    public static int Step
    {
        get
        {
            if (step <= 1)
            {
                return 1;
            }
            else if (step >= 9)
            {
                return 9;
            }
            else
            {
                return step;
            }
        }
        set
        {
            step = value;
        }
    }
}
