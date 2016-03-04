using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Manager : MonoBehaviour
{
    public GameObject[] StepObj;
    public GameObject Model;
    public GameObject Water;
    public Text Xinxitext;
    public Image Shiyanshuju;
    public Image XiayibuButton;
    public Text LiuliangjiText;
    public GameObject Shuiguan;
    public GameObject Ceyaguan;
    public GameObject Ceyaguan2;
    public GameObject Wenduji;
    public GameObject Heiban;
    public Text Gongshi;
    void Start()
    {
        print(System.Math.Round(0.0179f / (1 + (0.0357f * 10.3f) + (0.00018f * Mathf.Pow(10.3f, 2))), 4));
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
                Tishixinxi("点击电源插头,接通电源,打开水泵");
                break;
            case 2:
                StepObj[1].SetActive(true);
                Tishixinxi("观察测压管中是否有起泡存在,如有,需将起泡排出");
                break;
            case 3:
                StepObj[2].SetActive(true);
                Step3();
                Tishixinxi("待测压管中水位稳定后,分别读两个测压管的读数h1,h2,记录数据");
                break;
            case 4:
                StepObj[3].SetActive(true);
                Tishixinxi("关闭放空阀");
                break;
            case 5:
                StepObj[4].SetActive(true);
                Tishixinxi("将水嘴调到流量计一侧");
                break;
            case 6:
                StepObj[5].SetActive(true);
                Tishixinxi("打开放空阀,放空计量箱");
                break;
            case 7:
                StepObj[6].SetActive(true);
                Tishixinxi("点击温度计测量水温");
                break;
            case 8:
                StepObj[7].SetActive(true);
                Step8();
                break;
            case 9:
                Step9();
                break;
        }
    }
    #region 步骤
    public void Step1()
    {
        Model.animation.Play("Take 001");
        Invoke("Step1_2", 2);
    }
    public void Step1_2()
    {
        Water.animation.Play("Water1");
        Invoke("Step1_3", 4);
    }
    public void Step1_3()
    {
        StepObj[0].transform.FindChild("ClickParticle1").gameObject.SetActive(true);
        Tishixinxi("调节阀门,使流量达到最大");
    }
    public void Step1_4()
    {
        Model.animation.Play("Take 002");
        Invoke("Step1_5", 2);
    }
    public void Step1_5()
    {
        XiayibuOpen();
        StepObj[0].SetActive(false);
        Tishixinxi("请点击屏幕右侧下一步按钮继续进行");
    }
    public void Step2()
    {
        Ceyaguan.SetActive(true);
        StepObj[1].transform.FindChild("ClickParticle1").gameObject.SetActive(true);
        Tishixinxi("对折硅胶管,双手快速挤压,将气泡排出");
    }
    public void Step2_2()
    {
        Shuiguan.SetActive(true);
        Ceyaguan.animation.Play("Ceyaguan");
        Invoke("Step2_3", 3);
    }
    public void Step2_3()
    {
        Ceyaguan.SetActive(false);
        Shuiguan.SetActive(false);
        XiayibuOpen();
        StepObj[1].SetActive(false);
        Tishixinxi("请点击屏幕右侧下一步按钮继续进行");
    }
    public void Step3()
    {
        Ceyaguan2.SetActive(true);
        ShiyanshujuOpen();
        Shiyanshuju.transform.FindChild("h1").gameObject.SetActive(true);
    }
    public void Step3_2()
    {
        Shiyanshuju.transform.FindChild("h2").gameObject.SetActive(true);
    }
    public void Step3_3()
    {
        Ceyaguan2.SetActive(false);
        ShiyanshujuClose();
        XiayibuOpen();
        StepObj[2].SetActive(false);
        Tishixinxi("请点击屏幕右侧下一步按钮继续进行");
    }
    public void Step4()
    {
        Model.animation.Play("Take 003");
        Invoke("Step4_2", 2);
    }
    public void Step4_2()
    {
        StepObj[3].transform.FindChild("ClickParticle1").gameObject.SetActive(true);
        Tishixinxi("接通流量计电源");
    }
    public void Step4_3()
    {
        Model.animation.Play("Take 004");
        Invoke("Step4_4", 2);
    }
    public void Step4_4()
    {
        LiuliangjiText.gameObject.SetActive(true);
        StepObj[3].transform.FindChild("ClickParticle2").gameObject.SetActive(true);
        LiuliangjiText.text = "010.0L";
        Tishixinxi("屏幕显示10L,按'设置'键");
    }
    public void Step4_5()
    {
        LiuliangjiText.text = "0000.00";
        Tishixinxi("待屏幕显示000.0L,方可开始测流量");
        Invoke("Step4_6", 2);
    }
    public void Step4_6()
    {
        LiuliangjiText.text = "000.0L";
        XiayibuOpen();
        StepObj[3].SetActive(false);
        Tishixinxi("请点击屏幕右侧下一步按钮继续进行");
    }
    public void Step5()
    {
        Model.animation.Play("Take 005");
        Water.animation.Play("Water2");
        Invoke("Step5_2", 1);
    }
    public void Step5_2()
    {
        Water.animation.Play("Water3");
        Invoke("Step5_3", 1);
    }
    public void Step5_3()
    {
        Tishixinxi("当水触碰到下浮子时,流量开始计时");
        StartCoroutine("Step5_3IE");
    }
    IEnumerator Step5_3IE()
    {
        string thisString = "";
        while (State.Q < 262.61)
        {
            State.Q += 1.21f;
            State.Q = (float)System.Math.Round(State.Q, 2);
            yield return new WaitForSeconds(0.017f);
            thisString = State.Q.ToString("0000.00");
            LiuliangjiText.text = thisString;
        }
        Step5_4();
    }
    public void Step5_4()
    {
        Tishixinxi("当水触碰到上浮子时,流量停止计时");
        Invoke("Step5_5", 2);
    }
    public void Step5_5()
    {
        Tishixinxi("将水嘴调回到流量计另一侧");
        Model.animation.Play("Take 0011");
        Water.animation["Water2"].time = Water.animation["Water2"].length;
        Water.animation["Water2"].speed = -1;
        Water.animation.Play("Water2");
        Invoke("Step5_6", 2);
    }
    public void Step5_6()
    {
        Tishixinxi("记录流量计显示流量数据Q,填入表格");
        ShiyanshujuOpen();
        InputEndButton script = Shiyanshuju.transform.FindChild("Q").gameObject.GetComponent<InputEndButton>();
        script.Min = State.Q;
        script.Max = State.Q;
        Shiyanshuju.transform.FindChild("Q").gameObject.SetActive(true);
    }
    public void Step5_7()
    {
        ShiyanshujuClose();
        XiayibuOpen();
        StepObj[4].SetActive(false);
        Tishixinxi("请点击屏幕右侧下一步按钮继续进行");
    }
    public void Step6()
    {
        Model.animation.Play("Take 007");
        Invoke("Step6_2", 1);
    }
    public void Step6_2()
    {
        Water.animation["Water3"].time = Water.animation["Water3"].length;
        Water.animation["Water3"].speed = -1;
        Water.animation.Play("Water3");
        Invoke("Step6_3", 10);
    }
    public void Step6_3()
    {
        XiayibuOpen();
        StepObj[5].SetActive(false);
        Tishixinxi("请点击屏幕右侧下一步按钮继续进行");
    }
    public void Step7()
    {
        Model.animation.Play("Take 006");
        Invoke("Step7_2", 9);
    }
    public void Step7_2()
    {
        Shiyanshuju.transform.FindChild("T").gameObject.SetActive(true);
        ShiyanshujuOpen();
        Wenduji.SetActive(true);
        Tishixinxi("观察水温T,填入表格");
    }
    public void Step7_3()
    {
        XiayibuOpen();
        Wenduji.SetActive(false);
        StepObj[6].SetActive(false);
        ShiyanshujuClose();
        Tishixinxi("请点击屏幕右侧下一步按钮继续进行");
    }
    public void Step8()
    {
        ShiyanshujuOpen();
        Heiban.SetActive(true);
        Gongshi.gameObject.SetActive(true);
        #region 计算
        State.T_h = (float)System.Math.Round((State.h1 - State.h2), 1);
        Gongshi.text = State.h1 + "-" + State.h2 + "=" + State.T_h;
        InputEndButton script = Shiyanshuju.transform.FindChild("T_h").gameObject.GetComponent<InputEndButton>();
        script.Min = State.T_h;
        script.Max = State.T_h;
        #endregion
        Shiyanshuju.transform.FindChild("T_h").gameObject.SetActive(true);
        Tishixinxi("根据公式Δh=h1-h2,算出Δh,填入表格");
    }
    public void Step8_2()
    {
        #region 计算
        State.V = (float)System.Math.Round((4 * State.Q) / (Mathf.PI * Mathf.Pow(1.4f, 2)), 2);
        Gongshi.text = "(4×" + State.Q + ")/(π×1.4²)=" + State.V;
        InputEndButton script = Shiyanshuju.transform.FindChild("V").gameObject.GetComponent<InputEndButton>();
        script.Min = State.V;
        script.Max = State.V;
        #endregion
        Shiyanshuju.transform.FindChild("V").gameObject.SetActive(true);
        Tishixinxi("根据公式V=4Q/πd²计算流速值,填入表格");
    }
    public void Step8_3()
    {
        #region 计算
        State.u = (float)System.Math.Round(0.0179f / (1 + (0.0357f * State.T) + (0.00018f * Mathf.Pow(State.T, 2))), 4);
        Gongshi.text = "0.0179/1+0.0357×" + State.T + "+0.00018×" + State.T + "²=" + State.u;
        InputEndButton script = Shiyanshuju.transform.FindChild("u").gameObject.GetComponent<InputEndButton>();
        script.Min = State.u;
        script.Max = State.u;
        #endregion
        Shiyanshuju.transform.FindChild("u").gameObject.SetActive(true);
        Tishixinxi("根据公式u=0.0179/1+0.0357×T+0.00018×T²计算流体密度和运动粘性系数,填入表格");
    }
    public void Step8_4()
    {
        #region 计算
        State.Re = (float)System.Math.Round((State.V * 1.4) / State.u, 1);
        Gongshi.text = State.V + "×1.4/" + State.u + "=" + State.Re;
        InputEndButton script = Shiyanshuju.transform.FindChild("Re").gameObject.GetComponent<InputEndButton>();
        script.Min = State.Re;
        script.Max = State.Re;
        #endregion
        Shiyanshuju.transform.FindChild("Re").gameObject.SetActive(true);
        Tishixinxi("根据公式Re=Vd/u计算雷诺数,填入表格");
    }
    public void Step8_5()
    {
        #region 计算
        State.r = (float)System.Math.Round((2 * 9.8f * 0.014f * (State.T_h / 100)) / Mathf.Pow(State.V / 100, 2), 4);
        Gongshi.text = "2×9.8×0.014×" + (State.T_h / 100) + "/1×" + (State.V / 100) + "²=" + State.r;
        InputEndButton script = Shiyanshuju.transform.FindChild("r").gameObject.GetComponent<InputEndButton>();
        script.Min = State.r;
        script.Max = State.r;
        #endregion
        Shiyanshuju.transform.FindChild("r").gameObject.SetActive(true);
        Tishixinxi("根据公式λ= 2gdΔh/LV²计算沿程阻力系数(单位换算为米),填入表格");
    }
    public void Step8_6()
    {
        Shiyanshuju.transform.FindChild("Qita").gameObject.SetActive(true);
        Tishixinxi("减小阀门开度，改变流量，使流量由大到小变化。重复上述步骤，共测10组数据,填入表格");
        Invoke("Step8_7", 3);
    }
    public void Step8_7()
    {
        XiayibuOpen();
        ShiyanshujuClose();
        Heiban.SetActive(false);
        Gongshi.gameObject.SetActive(false);
        Tishixinxi("请点击屏幕右侧下一步按钮重新开始");
    }
    public void Step9()
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
    private void Tishixinxi(string text)
    {
        Xinxitext.text = text;
        Xinxitext.animation.Rewind("XinxiText");
        Xinxitext.animation.Play("XinxiText");
    }
    #region 实验数据
    public void h1(string text)
    {
        State.h1 = float.Parse(text);
        Invoke("Step3_2", 1);
        Debug.Log("h1数值为" + State.h1);
        Tishixinxi("测压管的读数h1输入正确");
    }
    public void h2(string text)
    {
        State.h2 = float.Parse(text);
        Invoke("Step3_3", 1);
        Debug.Log("h2数值为" + State.h2);
        Tishixinxi("测压管的读数h2输入正确");
    }
    public void Q(string text)
    {
        State.Q = float.Parse(text);
        Invoke("Step5_7", 1);
        Debug.Log("Q数值为" + State.Q);
        Tishixinxi("流量数据Q输入正确");
    }
    public void T(string text)
    {
        State.T = float.Parse(text);
        Invoke("Step7_3", 1);
        Debug.Log("T数值为" + State.T);
        Tishixinxi("温度T输入正确");
    }
    public void T_h(string text)
    {
        State.T_h = float.Parse(text);
        Invoke("Step8_2", 1);
        Debug.Log("T_h数值为" + State.T_h);
        Tishixinxi("Δh输入正确");
    }
    public void V(string text)
    {
        State.V = float.Parse(text);
        Invoke("Step8_3", 1);
        Debug.Log("V数值为" + State.V);
        Tishixinxi("流速V输入正确");
    }
    public void u(string text)
    {
        State.u = float.Parse(text);
        Invoke("Step8_4", 1);
        Debug.Log("u数值为" + State.u);
        Tishixinxi("流体密度和运动粘性系数u输入正确");
    }
    public void Re(string text)
    {
        State.Re = float.Parse(text);
        Invoke("Step8_5", 1);
        Debug.Log("Re数值为" + State.u);
        Tishixinxi("雷诺数Re输入正确");
    }
    public void r(string text)
    {
        State.r = float.Parse(text);
        Invoke("Step8_6", 1);
        Debug.Log("r数值为" + State.r);
        Tishixinxi("沿程阻力系数r输入正确");
    }
    #endregion
}
