using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ResNumbers : MonoBehaviour
{
    
    public GameObject Account;
    public GameObject PassWord;
    public Toggle IsHidePwd;
    private TMP_InputField CtrlAccount, CtrlPassWord;
    private bool isInputUserName, isInputUserPwd;
    private string userName, userPwd;

    public string USERNAME
    {
        get { return userName; }
    }

    public string USERPWD
    {
        get { return userPwd; }
    }
    
    private CanHideInputfiled ctrl;
    private void Start()
    {
        CtrlAccount = Account.GetComponent<TMP_InputField>();
        CtrlPassWord = PassWord.GetComponent<TMP_InputField>();
        ctrl = GetComponent<CanHideInputfiled>();
    }

    public void IsInputUserName()
    {
        isInputUserName = true;
        isInputUserPwd = false;
    }

    public void IsInputUserPwd()
    {
        isInputUserPwd = true;
        isInputUserName = false;
    }
    

    /// <summary>
    /// 输入数字显示
    /// </summary>
    public void PressNumber()
    {
        if (isInputUserName)
        {
            userName += UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
            CtrlAccount.text = userName;
        }

        if (isInputUserPwd)
        {
            userPwd += UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
            CtrlPassWord.text = userPwd;
        }
    }

    /// <summary>
    /// 响应REMOVE按键
    /// </summary>
    public void DeleteInfo()
    {
        if (isInputUserName)
        {
            userName = null;
            CtrlAccount.text = userName;
        }

        if (isInputUserPwd)
        {
            userPwd = null;
            CtrlPassWord.text = userPwd;
        }
    }
    
    /// <summary>
    /// 是否隐藏密码
    /// </summary>
    public void IsHidePassword(bool isSelected)
    {
        if (isSelected)
        {
            CtrlPassWord.inputType = TMP_InputField.InputType.Standard;
        }
        else
        {
            CtrlPassWord.inputType = TMP_InputField.InputType.Password;
        }
        CtrlPassWord.ForceLabelUpdate(); 
    }
}
