using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class ChatUIController : MonoBehaviour
{
    [Header("UI组件")]
    public TMPro.TMP_InputField inputField;
    public TMPro.TMP_Text chatText;
    public UnityEngine.UI.Button sendButton;
    public UnityEngine.UI.ScrollRect scrollRect;
    
    private StringBuilder chatHistory = new StringBuilder();
    
    private void Start()
    {
        sendButton.onClick.AddListener(SendMessage);
        inputField.onValueChanged.AddListener(OnInputChanged);
        
        // 回车发送
        inputField.onSubmit.AddListener((text) => SendMessage());
    }
    
    private void OnInputChanged(string text)
    {
        sendButton.interactable = !string.IsNullOrEmpty(text);
    }
    
    public void SendMessage()
    {
        string message = inputField.text.Trim();
        if (string.IsNullOrEmpty(message)) return;
        
        // 添加到聊天显示
        AddMessageToChat("你", message);
        inputField.text = "";
        sendButton.interactable = false;
        
        // 发送到DeepSeek
        DeepSeekChatManager.Instance.SendMessage(
            message,
            OnReceiveResponse,
            OnReceiveError
        );
    }
    
    private void OnReceiveResponse(string response)
    {
        AddMessageToChat("AI", response);
    }
    
    private void OnReceiveError(string error)
    {
        AddMessageToChat("系统", $"错误: {error}");
    }
    
    private void AddMessageToChat(string sender, string message)
    {
        chatHistory.AppendLine($"<b>{sender}:</b> {message}");
        chatHistory.AppendLine();
        chatText.text = chatHistory.ToString();
        
        // 滚动到底部
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0f;
    }
    
    // 清空聊天
    public void ClearChat()
    {
        chatHistory.Clear();
        chatText.text = "";
        DeepSeekChatManager.Instance.ClearConversation();
    }
}