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
    public static string d = "";
    public static string H = "";
    public static string Wo = "";
    public static string t_kongkou = "";
    public static string t_guanzui = "";
    public static string W_kongkou = "";
    public static string W_guanzui = "";
    public static string T_W_kongkou = "";
    public static string T_W_guanzui = "";
    public static string Q_kongkou = "";
    public static string Q_guanzui = "";
    public static string u_kongkou = "";
    public static string u_guanzui = "";
    public static string o_kongkou = "";
    public static string o_guanzui = "";
    public static string y_kongkou = "";
    public static string y_guanzui = "";
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
            else if (step >= 8)
            {
                return 8;
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
