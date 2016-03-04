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
                Tishixinxi("点击流量计插头,接通电源");
                break;
            case 2:
                StepObj[1].SetActive(true);
                Tishixinxi("关闭泄空阀门");
                break;
            case 3:
                StepObj[2].SetActive(true);
                Tishixinxi("关闭进水口阀门");
                break;
            case 4:
                Step4();
                break;
        }
    }
    #region 步骤
    public void Step1()
    {
        Model.animation.Play("Take 001");
        Invoke("Step1_2", 1.5f);
    }
    public void Step1_2()
    {
        LiuliangjiText.text = "0008.0L";
        StepObj[0].transform.FindChild("ClickParticle1").gameObject.SetActive(true);
        Tishixinxi("按'设置'键,待屏幕显示0000.00,可开始测量流量");
    }
    public void Step1_3()
    {
        XiayibuOpen();
        StepObj[0].SetActive(false);
        LiuliangjiText.text = "0000.00";
        StepObj[0].transform.FindChild("ClickParticle1").gameObject.SetActive(true);
        Tishixinxi("请点击屏幕右侧下一步按钮继续进行");
    }
    public void Step2()
    {
        Model.animation.Play("Take 002");
        StepObj[1].transform.FindChild("ClickParticle1").gameObject.SetActive(true);
        Tishixinxi("接通电源");
    }
    public void Step2_1()
    {
        Model.animation.Play("Take 003");
        StepObj[1].transform.FindChild("ClickParticle3").gameObject.SetActive(true);
        Tishixinxi("打开进水口阀门");
    }
    public void Step2_1_2()
    {
        Model.animation.Play("Take 004");
        Invoke("Step2_2", 0.5f);
    }
    public void Step2_2()
    {
        Water.animation.Play("Water");
        Invoke("Step2_3", 5);
    }
    public void Step2_3()
    {
        StepObj[1].transform.FindChild("Xiafuzi").gameObject.SetActive(true);
        Tishixinxi("当水触碰到下浮子时,流量开始计时");
        StartCoroutine("Step2_3IE");
    }
    IEnumerator Step2_3IE()
    {
        string thisString = "";
        while (State.Q < 149.3)
        {
            State.Q += 0.31f;
            yield return new WaitForSeconds(0.03f);
            thisString = State.Q.ToString("0000.00");
            LiuliangjiText.text = thisString;
        }
        Step2_4();
    }
    public void Step2_4()
    {
        StepObj[1].transform.FindChild("Shangfuzi").gameObject.SetActive(true);
        Tishixinxi("当水触碰到上浮子时,停止计时");
        Invoke("Step2_5", 2);
    }
    public void Step2_5()
    {
        ShiyanshujuOpen();
        InputEndButton script = Shiyanshuju.transform.FindChild("Q").gameObject.GetComponent<InputEndButton>();
        script.Min = (float)System.Math.Round(State.Q, 2);
        script.Max = (float)System.Math.Round(State.Q, 2);
        Shiyanshuju.transform.FindChild("Q").gameObject.SetActive(true);
        Tishixinxi("当水触碰到上浮子时停止计时，此时数显表屏幕显示流量Q，记录数据");
    }
    public void Step2_6()
    {
        ShiyanshujuClose();
        StepObj[1].transform.FindChild("ClickParticle2").gameObject.SetActive(true);
        Tishixinxi("在流量显示稳定后5秒内按'显示'键");
    }
    public void Step2_7()
    {
        ShiyanshujuOpen();
        LiuliangjiText.text = "0000.42";
        Shiyanshuju.transform.FindChild("R").gameObject.SetActive(true);
        Tishixinxi("此时数显表屏幕显示力值R，记录数据");
    }
    public void Step2_8()
    {
        Shiyanshuju.transform.FindChild("V").gameObject.SetActive(true);
        Tishixinxi("根据公式V=V1x-V2x(V1x=Q/πr²,V2x=0)得出V=2.97(m/s)，记录数据");
    }
    public void Step2_9()
    {
        Shiyanshuju.transform.FindChild("RX").gameObject.SetActive(true);
        Tishixinxi("根据公式Rx=ρQ(V1x-V2x)= ρQV1x(其中ρ为水的密度=1000[kg/m])得出水流对平板的作用力Rx=0.443(N)，记录数据");
    }
    public void Step2_10()
    {
        Tishixinxi("第一组数据记录计算完成");
        Invoke("Step2_11", 2);
    }
    public void Step2_11()
    {
        XiayibuOpen();
        ShiyanshujuClose();
        Tishixinxi("请点击屏幕右侧下一步按钮继续进行");
    }
    public void Step3()
    {
        StepObj[2].transform.FindChild("ClickParticle1").gameObject.SetActive(true);
        Model.animation["Take 004"].time = Model.animation["Take 004"].length;
        Model.animation["Take 004"].speed = -1;
        Model.animation.Play("Take 004");
        Water.animation.Play("WaterStop");
        Tishixinxi("打开泄空阀门将水放空");
    }
    public void Step3_2()
    {
        Model.animation["Take 002"].time = Model.animation["Take 002"].length;
        Model.animation["Take 002"].speed = -1;
        Model.animation.Play("Take 002");
        Water.animation.Play("WaterChu");
        Invoke("Step3_3", 10);
    }
    public void Step3_3()
    {
        ShiyanshujuOpen();
        Tishixinxi("重复步骤测量三组数据,记录");
        Invoke("Step3_4", 2);
    }
    public void Step3_4()
    {
        Shiyanshuju.transform.FindChild("Other").gameObject.SetActive(true);
        Invoke("Step3_5", 3);
    }
    public void Step3_5()
    {
        ShiyanshujuClose();
        XiayibuOpen();
        Tishixinxi("请点击屏幕右侧下一步按钮重新开始");
    }
    public void Step4()
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

    #endregion
}
