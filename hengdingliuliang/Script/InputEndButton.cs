using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//(float)System.Math.Round();四舍五入取值准确判断数值
public class InputEndButton : MonoBehaviour
{
    private InputField thisInput;
    public float Min;
    public float Max;
    public GameObject FunctionObj;
    public string FunctionName;
    void OnEnable()
    {
        thisInput = gameObject.GetComponent<InputField>();
        thisInput.onEndEdit.AddListener(OnInputEnd);
    }
    public void OnInputEnd(string text)
    {
        print(Min);
        print(Max);
        float textFloat = -1;
        if (text != "")
        {
            textFloat = float.Parse(text);
        }
        if (textFloat >= Min && textFloat <= Max)
        {
            FunctionObj.SendMessage(FunctionName, text, SendMessageOptions.DontRequireReceiver);
            thisInput.enabled = false;
        }
        else
        {
            thisInput.text = "";
            gameObject.transform.FindChild("Placeholder").GetComponent<Text>().text = "输入错误";
        }
    }
}
