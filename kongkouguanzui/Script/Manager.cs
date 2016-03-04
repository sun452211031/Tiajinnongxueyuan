using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Manager : MonoBehaviour
{
    public GameObject[] StepObj;
    public GameObject Model;
    public GameObject Water_kongkou;
    public GameObject Water_guanzui;
    public Text Xinxitext;
    public Image Shiyanshuju;
    public Image XiayibuButton;
    public Image Biaopan;
    public Image Shuiwei;
    public Image Miaobiao;
    public Sprite[] AllSprite;
    public Text Gongshi;
    public GameObject Heiban;
    void Start()
    {
        Step();
    }
    public void Xiayibu()
    {
        State.Step += 1;
        Step();
        XiayibuClose();
        Debug.Log(State.Step);
    }
    public void Shangyibu()
    {
        State.Step -= 1;
        Step();
        Debug.Log(State.Step);
    }
    public void Step()
    {
        switch (State.Step)
        {
            case 1:
                StepObj[0].SetActive(true);
                Tishixinxi("查看孔口和管嘴直径d,并将数据填入表格");
                break;
            case 2:
                StepObj[1].SetActive(true);
                Tishixinxi("鼠标点击水桶，将其放置在磅秤上");
                break;
            case 3:
                StepObj[2].SetActive(true);
                Tishixinxi("鼠标点击插头，接入电源开动水泵");
                break;
            case 4:
                StepObj[3].SetActive(true);
                Tishixinxi("鼠标点击水桶,对孔口出流进行测定");
                break;
            case 5:
                StepObj[4].SetActive(true);
                Step5();
                Tishixinxi("根据公式ΔW=W-Wo计算水的净重,并将数据填入表格");
                break;
            case 6:
                StepObj[5].SetActive(true);
                Tishixinxi("鼠标点击水桶,对管嘴出流进行测定");
                break;
            case 7:
                StepObj[6].SetActive(true);
                Step7();
                Tishixinxi("根据公式ΔW=W-Wo计算水的净重,并将数据填入表格");
                break;
            case 8:
                StepObj[7].SetActive(true);
                Step8();
                break;
        }
    }
    #region 步骤
    public void Step1()
    {
        StepObj[0].SetActive(false);
        ShiyanshujuOpen();
        Shiyanshuju.transform.FindChild("d").gameObject.SetActive(true);
        Tishixinxi("孔口和管嘴直径d为1.5cm,请正确输入.如输入数值错误将无法进行");
    }
    public void Step1_2()
    {
        ShiyanshujuClose();
        XiayibuOpen();
        Tishixinxi("请点击屏幕右侧下一步按钮继续进行");
    }
    public void Step2()
    {
        Model.animation.Play("Take 001");
        Invoke("Step2_2", 3);
    }
    public void Step2_2()
    {
        ShiyanshujuOpen();
        Biaopan.gameObject.SetActive(true);
        Shiyanshuju.transform.FindChild("Wo").gameObject.SetActive(true);
        Tishixinxi("查看水桶的净重Wo，并记录在数据记录表上");
    }
    public void Step2_3()
    {
        ShiyanshujuClose();
        XiayibuOpen();
        Biaopan.gameObject.SetActive(false);
        Tishixinxi("请点击屏幕右侧下一步按钮继续进行");
    }
    public void Step3()
    {
        Model.animation.Play("Take 002");
        Invoke("Step3_1", 3);
    }
    public void Step3_1()
    {
        Water_kongkou.animation.Play("Water_kongkou");
        Water_guanzui.animation.Play("Water_guanzui");
        Tishixinxi("水泵加水,等待水箱内水位上升");
        Invoke("Step3_2", 40);
    }
    public void Step3_2()
    {
        StepObj[2].transform.FindChild("ClickParticle2").gameObject.SetActive(true);
        Tishixinxi("待水箱水位稳定后，鼠标点击直尺，实测孔口的作用水头");
    }
    public void Step3_3()
    {
        Model.animation.Play("Take 003");
        Invoke("Step3_4", 5);
    }
    public void Step3_4()
    {
        ShiyanshujuOpen();
        Shuiwei.gameObject.SetActive(true);
        Shiyanshuju.transform.FindChild("H").gameObject.SetActive(true);
        Tishixinxi("将孔口的作用水头数值H记录在数据记录表上");
    }
    public void Step3_5()
    {
        ShiyanshujuClose();
        XiayibuOpen();
        Shuiwei.gameObject.SetActive(false);
        Tishixinxi("请点击屏幕右侧下一步按钮继续进行");
    }
    public void Step4()
    {
        Model.animation.Play("Take 004");
        Tishixinxi("对孔口出流进行测定,点击桶接水并记录时间Δt,测量水桶加水的重量W");
        Invoke("Step4_2", 20);
    }
    public void Step4_2()
    {
        ShiyanshujuOpen();
        Miaobiao.gameObject.SetActive(true);
        Shiyanshuju.transform.FindChild("t_kongkou").gameObject.SetActive(true);
        Tishixinxi("将时间Δt记录在数据记录表上");
    }
    public void Step4_3()
    {
        Miaobiao.gameObject.SetActive(false);
        Biaopan.gameObject.SetActive(true);
        Biaopan.sprite = AllSprite[0];
        Shiyanshuju.transform.FindChild("W_kongkou").gameObject.SetActive(true);
        Tishixinxi("将水桶加水的重量W记录在数据记录表上");
    }
    public void Step4_4()
    {
        XiayibuOpen();
        ShiyanshujuClose();
        Biaopan.gameObject.SetActive(false);
        Tishixinxi("请点击屏幕右侧下一步按钮继续进行");
    }
    public void Step5()
    {
        ShiyanshujuOpen();
        Gongshi.gameObject.SetActive(true);
        Heiban.SetActive(true);
        #region 计算
        float F_Wo = 0;
        float F_W_kongkou = 0;
        if (State.Wo != "")
        {
            F_Wo = float.Parse(State.Wo);
        }
        if (State.W_kongkou != "")
        {
            F_W_kongkou = float.Parse(State.W_kongkou);
        }
        State.T_W_kongkou = "" + (float)System.Math.Round(F_W_kongkou - F_Wo, 1);
        Gongshi.text = State.W_kongkou + "-" + State.Wo + "=" + State.T_W_kongkou + "(kg)";
        InputEndButton script = Shiyanshuju.transform.FindChild("T_W_kongkou").gameObject.GetComponent<InputEndButton>();
        script.Min = (float)System.Math.Round(F_W_kongkou - F_Wo, 1);
        script.Max = (float)System.Math.Round(F_W_kongkou - F_Wo, 1);
        #endregion
        Shiyanshuju.transform.FindChild("T_W_kongkou").gameObject.SetActive(true);
    }
    public void Step5_2()
    {
        float F_T_W_kongkou = 0;
        float F_t_kongkou = 0;
        #region 计算
        if (State.T_W_kongkou != "")
        {
            F_T_W_kongkou = float.Parse(State.T_W_kongkou);
        }
        if (State.t_kongkou != "")
        {
            F_t_kongkou = float.Parse(State.t_kongkou);
        }
        State.Q_kongkou = "" + (float)System.Math.Round(((1000 * F_T_W_kongkou) / F_t_kongkou), 1);
        Gongshi.text = "1000×" + State.T_W_kongkou + "/" + State.t_kongkou + "=" + State.Q_kongkou + "(cm3/S)";
        InputEndButton script = Shiyanshuju.transform.FindChild("Q_kongkou").gameObject.GetComponent<InputEndButton>();
        script.Min = (float)System.Math.Round(((1000 * F_T_W_kongkou) / F_t_kongkou), 1);
        script.Max = (float)System.Math.Round(((1000 * F_T_W_kongkou) / F_t_kongkou), 1);
        #endregion
        Shiyanshuju.transform.FindChild("Q_kongkou").gameObject.SetActive(true);
        Tishixinxi("根据公式Q=1000ΔW/Δt计算计算流量Q(cm³/s),并将数据填入表格");
    }
    public void Step5_3()
    {
        float F_Q_kongkou = 0;
        float F_H = 0;
        #region 计算
        if (State.Q_kongkou != "")
        {
            F_Q_kongkou = float.Parse(State.Q_kongkou);
        }
        if (State.H != "")
        {
            F_H = float.Parse(State.H);
        }
        State.u_kongkou = "" + (float)System.Math.Round((F_Q_kongkou / ((Mathf.PI * Mathf.Pow(0.75f, 2)) * Mathf.Sqrt(2 * 9.8f * F_H * 100))), 3);
        Gongshi.text = State.Q_kongkou + "÷" + "(" + "A√2g×" + State.H + ")=" + State.u_kongkou;
        InputEndButton script = Shiyanshuju.transform.FindChild("u_kongkou").gameObject.GetComponent<InputEndButton>();
        script.Min = (float)System.Math.Round((F_Q_kongkou / ((Mathf.PI * Mathf.Pow(0.75f, 2)) * Mathf.Sqrt(2 * 9.8f * F_H * 100))), 3);
        script.Max = (float)System.Math.Round((F_Q_kongkou / ((Mathf.PI * Mathf.Pow(0.75f, 2)) * Mathf.Sqrt(2 * 9.8f * F_H * 100))), 3);
        #endregion
        Shiyanshuju.transform.FindChild("u_kongkou").gameObject.SetActive(true);
        Tishixinxi("根据公式μ=Q/(A√2gH)计算流量系数μ(其中A为出水口截面积π*(d÷2)²,g为重力加速度980(cm/s²)),并将数据填入表格");
    }
    public void Step5_4()
    {
        float F_u_kongkou = 0;
        #region 计算
        if (State.u_kongkou != "")
        {
            F_u_kongkou = float.Parse(State.u_kongkou);
        }
        State.o_kongkou = "" + (float)System.Math.Round((F_u_kongkou / 0.64f), 3);
        Gongshi.text = State.u_kongkou + "÷" + "0.64" + "=" + State.o_kongkou;
        InputEndButton script = Shiyanshuju.transform.FindChild("o_kongkou").gameObject.GetComponent<InputEndButton>();
        script.Min = (float)System.Math.Round((F_u_kongkou / 0.64f), 3);
        script.Max = (float)System.Math.Round((F_u_kongkou / 0.64f), 3);
        #endregion
        Shiyanshuju.transform.FindChild("o_kongkou").gameObject.SetActive(true);
        Tishixinxi("根据公式μ= φ×ε计算出流速系数φ（其中收缩系数ε=0.64),并将数据填入表格");
    }
    public void Step5_5()
    {
        float F_o_kongkou = 0;
        #region 计算
        if (State.o_kongkou != "")
        {
            F_o_kongkou = float.Parse(State.o_kongkou);
        }
        State.y_kongkou = "" + (float)System.Math.Round((1 / Mathf.Pow(F_o_kongkou, 2) - 1), 2);
        Gongshi.text = "1/" + State.o_kongkou + "²" + "-1=" + State.y_kongkou;
        InputEndButton script = Shiyanshuju.transform.FindChild("y_kongkou").gameObject.GetComponent<InputEndButton>();
        script.Min = (float)System.Math.Round((1 / Mathf.Pow(F_o_kongkou, 2) - 1), 2);
        script.Max = (float)System.Math.Round((1 / Mathf.Pow(F_o_kongkou, 2) - 1), 2);
        #endregion
        Shiyanshuju.transform.FindChild("y_kongkou").gameObject.SetActive(true);
        Tishixinxi("根据公式ξ=1/φ²-1 ,计算出局部阻力系数ξ,并将数据填入表格");
    }
    public void Step5_6()
    {
        Shiyanshuju.transform.FindChild("kongkou2_3").gameObject.SetActive(true);
        Tishixinxi("重复上述步骤,测三组数据,记入表格");
        Invoke("Step5_7", 3);
    }
    public void Step5_7()
    {
        Heiban.SetActive(false);
        Gongshi.gameObject.SetActive(false);
        ShiyanshujuClose();
        XiayibuOpen();
        Tishixinxi("请点击屏幕右侧下一步按钮继续进行");
    }
    public void Step6()
    {
        Model.animation.Play("Take 005");
        Tishixinxi("对管嘴出流进行测定,点击水桶接水并记录时间Δt,测量水桶加水的重量W");
        Invoke("Step6_2", 13);
    }
    public void Step6_2()
    {
        ShiyanshujuOpen();
        Miaobiao.gameObject.SetActive(true);
        Miaobiao.sprite = AllSprite[1];
        Shiyanshuju.transform.FindChild("t_guanzui").gameObject.SetActive(true);
        Tishixinxi("将时间Δt记录在数据记录表上");
    }
    public void Step6_3()
    {
        Miaobiao.gameObject.SetActive(false);
        Biaopan.gameObject.SetActive(true);
        Biaopan.sprite = AllSprite[2];
        Shiyanshuju.transform.FindChild("W_guanzui").gameObject.SetActive(true);
        Tishixinxi("将水桶加水的重量W记录在数据记录表上");
    }
    public void Step6_4()
    {
        XiayibuOpen();
        ShiyanshujuClose();
        Biaopan.gameObject.SetActive(false);
        Tishixinxi("请点击屏幕右侧下一步按钮继续进行");
    }
    public void Step7()
    {
        ShiyanshujuOpen();
        Gongshi.gameObject.SetActive(true);
        Heiban.SetActive(true);
        #region 计算
        float F_Wo = 0;
        float F_W_guanzui = 0;
        if (State.Wo != "")
        {
            F_Wo = float.Parse(State.Wo);
        }
        if (State.W_guanzui != "")
        {
            F_W_guanzui = float.Parse(State.W_guanzui);
        }
        State.T_W_guanzui = "" + (float)System.Math.Round(F_W_guanzui - F_Wo, 1);
        Gongshi.text = State.W_guanzui + "-" + State.Wo + "=" + State.T_W_guanzui + "(kg)";
        InputEndButton script = Shiyanshuju.transform.FindChild("T_W_guanzui").gameObject.GetComponent<InputEndButton>();
        script.Min = (float)System.Math.Round(F_W_guanzui - F_Wo, 1);
        script.Max = (float)System.Math.Round(F_W_guanzui - F_Wo, 1);
        #endregion
        Shiyanshuju.transform.FindChild("T_W_guanzui").gameObject.SetActive(true);
    }
    public void Step7_2()
    {
        float F_T_W_guanzui = 0;
        float F_t_guanzui = 0;
        #region 计算
        if (State.T_W_guanzui != "")
        {
            F_T_W_guanzui = float.Parse(State.T_W_guanzui);
        }
        if (State.t_guanzui != "")
        {
            F_t_guanzui = float.Parse(State.t_guanzui);
        }
        State.Q_guanzui = "" + (float)System.Math.Round(((1000 * F_T_W_guanzui) / F_t_guanzui), 1);
        Gongshi.text = "1000×" + State.T_W_guanzui + "/" + State.t_guanzui + "=" + State.Q_guanzui + "(cm3/S)";
        InputEndButton script = Shiyanshuju.transform.FindChild("Q_guanzui").gameObject.GetComponent<InputEndButton>();
        script.Min = (float)System.Math.Round(((1000 * F_T_W_guanzui) / F_t_guanzui), 1);
        script.Max = (float)System.Math.Round(((1000 * F_T_W_guanzui) / F_t_guanzui), 1);
        #endregion
        Shiyanshuju.transform.FindChild("Q_guanzui").gameObject.SetActive(true);
        Tishixinxi("根据公式Q=1000ΔW/Δt计算计算流量Q(cm³/s),并将数据填入表格");
    }
    public void Step7_3()
    {
        float F_Q_guanzui = 0;
        float F_H = 0;
        #region 计算
        if (State.Q_guanzui != "")
        {
            F_Q_guanzui = float.Parse(State.Q_guanzui);
        }
        if (State.H != "")
        {
            F_H = float.Parse(State.H);
        }
        State.u_guanzui = "" + (float)System.Math.Round((F_Q_guanzui / ((Mathf.PI * Mathf.Pow(0.75f, 2)) * Mathf.Sqrt(2 * 9.8f * F_H * 100))), 3);
        Gongshi.text = State.Q_guanzui + "÷" + "(" + "A√2g×" + State.H + ")=" + State.u_guanzui;
        InputEndButton script = Shiyanshuju.transform.FindChild("u_guanzui").gameObject.GetComponent<InputEndButton>();
        script.Min = (float)System.Math.Round((F_Q_guanzui / ((Mathf.PI * Mathf.Pow(0.75f, 2)) * Mathf.Sqrt(2 * 9.8f * F_H * 100))), 3);
        script.Max = (float)System.Math.Round((F_Q_guanzui / ((Mathf.PI * Mathf.Pow(0.75f, 2)) * Mathf.Sqrt(2 * 9.8f * F_H * 100))), 3);
        #endregion
        Shiyanshuju.transform.FindChild("u_guanzui").gameObject.SetActive(true);
        Tishixinxi("根据公式μ=Q/(A√2gH)计算流量系数μ(其中A为出水口截面积π*(d÷2)²,g为重力加速度980(cm/s²)),并将数据填入表格");
    }
    public void Step7_4()
    {
        float F_u_guanzui = 0;
        #region 计算
        if (State.u_guanzui != "")
        {
            F_u_guanzui = float.Parse(State.u_guanzui);
        }
        State.o_guanzui = "" + F_u_guanzui;
        Gongshi.text = "μ=" + State.o_guanzui;
        InputEndButton script = Shiyanshuju.transform.FindChild("o_guanzui").gameObject.GetComponent<InputEndButton>();
        script.Min = F_u_guanzui;
        script.Max = F_u_guanzui;
        #endregion
        Shiyanshuju.transform.FindChild("o_guanzui").gameObject.SetActive(true);
        Tishixinxi("对于管嘴出流μ=φ,将数据填入表格");
    }
    public void Step7_5()
    {
        float F_o_guanzui = 0;
        #region 计算
        if (State.o_guanzui != "")
        {
            F_o_guanzui = float.Parse(State.o_guanzui);
        }
        State.y_guanzui = "" + (float)System.Math.Round((1 / Mathf.Pow(F_o_guanzui, 2) - 1), 2);
        Gongshi.text = "1/" + State.o_guanzui + "²" + "-1=" + State.y_guanzui;
        InputEndButton script = Shiyanshuju.transform.FindChild("y_guanzui").gameObject.GetComponent<InputEndButton>();
        script.Min = (float)System.Math.Round((1 / Mathf.Pow(F_o_guanzui, 2) - 1), 2);
        script.Max = (float)System.Math.Round((1 / Mathf.Pow(F_o_guanzui, 2) - 1), 2);
        #endregion
        Shiyanshuju.transform.FindChild("y_guanzui").gameObject.SetActive(true);
        Tishixinxi("根据公式ξ=1/φ²-1 ,计算出局部阻力系数ξ,并将数据填入表格");
    }
    public void Step7_6()
    {
        Shiyanshuju.transform.FindChild("guanzui2_3").gameObject.SetActive(true);
        Tishixinxi("重复上述步骤,测三组数据,记入表格");
        Invoke("Step7_7", 3);
    }
    public void Step7_7()
    {
        Heiban.SetActive(false);
        Gongshi.gameObject.SetActive(false);
        ShiyanshujuClose();
        XiayibuOpen();
        Tishixinxi("请点击屏幕右侧下一步按钮重新开始");
    }
    public void Step8()
    {
        Application.LoadLevel("Shiyanchangjing");
    }
    #endregion
    public void ShiyanshujuOpen()
    {
        Shiyanshuju.animation.Play("ShiyanshujuOpen");
    }
    public void ShiyanshujuClose()
    {
        Shiyanshuju.animation.Play("ShiyanshujuClose");
    }
    public void XiayibuOpen()
    {
        XiayibuButton.gameObject.SetActive(true);
    }
    public void XiayibuClose()
    {
        XiayibuButton.gameObject.SetActive(false);
    }
    //public void AnimationState()
    //{
    //    switch (State.Step)
    //    {
    //        case 1:
    //            Water_kongkou.animation.AddClip(Water_kongkou.animation.clip, "Remove", 0, 1, false);
    //            Water_kongkou.animation.Play("Remove");
    //            Water_guanzui.animation.AddClip(Water_guanzui.animation.clip, "Remove", 0, 1, false);
    //            Water_guanzui.animation.Play("Remove");
    //            Debug.Log("动画状态1");
    //            break;
    //        case 2:
    //            Water_kongkou.animation["Water_kongkou"].normalizedTime = 1;
    //            Water_kongkou.animation.Play("Water_kongkou");
    //            Water_guanzui.animation["Water_guanzui"].normalizedTime = 1;
    //            Water_guanzui.animation.Play("Water_guanzui");
    //            Debug.Log("动画状态2");
    //            break;
    //        case 3:
    //            Debug.Log("动画状态3");
    //            break;
    //        case 4:
    //            Debug.Log("动画状态4");
    //            break;
    //        case 5:
    //            Debug.Log("动画状态5");
    //            break;
    //    }
    //}
    private void Tishixinxi(string text)
    {
        Xinxitext.text = text;
        Xinxitext.animation.Rewind("XinxiText");
        Xinxitext.animation.Play("XinxiText");
    }
    #region 实验数据
    public void d(string text)
    {
        State.d = text;
        Invoke("Step1_2", 3);
        Debug.Log("d数值为" + State.d);
        Tishixinxi("孔口和管嘴直径输入正确");
    }
    public void H(string text)
    {
        State.H = text;
        Invoke("Step3_5", 3);
        Debug.Log("H数值为" + State.H);
        Tishixinxi("孔口的作用水头数值H输入正确");
    }
    public void Wo(string text)
    {
        State.Wo = text;
        Debug.Log("Wo数值为" + State.Wo);
        Invoke("Step2_3", 3);
        Tishixinxi("水桶净重Wo输入正确");
    }
    public void t_kongkou(string text)
    {
        State.t_kongkou = text;
        Debug.Log("孔口出流测量时间为" + State.t_kongkou);
        Invoke("Step4_3", 3);
        Tishixinxi("孔口出流测量时间Δt输入正确");
    }
    public void t_guanzui(string text)
    {
        State.t_guanzui = text;
        Debug.Log("管嘴出流测量时间为" + State.t_guanzui);
        Invoke("Step6_3", 3);
        Tishixinxi("管嘴出流测量时间Δt输入正确");
    }
    public void W_kongkou(string text)
    {
        State.W_kongkou = text;
        Debug.Log("水桶加水的重量W为" + State.W_kongkou);
        Invoke("Step4_4", 3);
        Tishixinxi("水桶加水的重量W输入正确");
    }
    public void W_guanzui(string text)
    {
        State.W_guanzui = text;
        Debug.Log("水桶加水的重量W为" + State.W_guanzui);
        Invoke("Step6_4", 3);
        Tishixinxi("水桶加水的重量W输入正确");
    }
    public void T_W_kongkou(string text)
    {
        Debug.Log("水净重ΔW为" + State.T_W_kongkou);
        Invoke("Step5_2", 3);
        Tishixinxi("水净重ΔW输入正确");
    }
    public void T_W_guanzui(string text)
    {
        Debug.Log("水净重ΔW为" + State.T_W_guanzui);
        Invoke("Step7_2", 3);
        Tishixinxi("水净重ΔW输入正确");
    }
    public void Q_kongkou(string text)
    {
        Debug.Log("流量Q为" + State.Q_kongkou);
        Invoke("Step5_3", 3);
        Tishixinxi("流量Q输入正确");
    }
    public void Q_guanzui(string text)
    {
        Debug.Log("流量Q为" + State.Q_guanzui);
        Invoke("Step7_3", 3);
        Tishixinxi("流量Q输入正确");
    }
    public void u_kongkou(string text)
    {
        Debug.Log("流量系数μ为" + State.u_kongkou);
        Invoke("Step5_4", 3);
        Tishixinxi("流量系数μ输入正确");
    }
    public void u_guanzui(string text)
    {
        Debug.Log("流量系数μ为" + State.u_guanzui);
        Invoke("Step7_4", 3);
        Tishixinxi("流量系数μ输入正确");
    }
    public void o_kongkou(string text)
    {
        Debug.Log("流速系数φ为" + State.o_kongkou);
        Invoke("Step5_5", 3);
        Tishixinxi("流速系数φ输入正确");
    }
    public void o_guanzui(string text)
    {
        Debug.Log("流速系数φ为" + State.o_guanzui);
        Invoke("Step7_5", 3);
        Tishixinxi("流速系数φ输入正确");
    }
    public void y_kongkou(string text)
    {
        Debug.Log("局部阻力系数ξ" + State.o_kongkou);
        Invoke("Step5_6", 3);
        Tishixinxi("局部阻力系数ξ输入正确");
    }
    public void y_guanzui(string text)
    {
        Debug.Log("局部阻力系数ξ" + State.o_guanzui);
        Invoke("Step7_6", 3);
        Tishixinxi("局部阻力系数ξ输入正确");
    }
    #endregion
}
