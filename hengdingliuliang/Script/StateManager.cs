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
    private static float q = 0;
    public static float Q
    {
        get
        {
            if (q >= 149.3f)
            {
                return 149.3f;
            }
            else
            {
                return q;
            }
        }
        set
        {
            q = value;
        }
    }
    public static float R = 0.42f;
    public static float V = 2.97f;
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
            else if (step >= 4)
            {
                return 4;
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
