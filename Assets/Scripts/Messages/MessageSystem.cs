using UnityEngine;
using System.Collections.Generic;

public class MessageSystem : MonoBehaviour
{
    public List<PhoneMessage> messages = new List<PhoneMessage>();

    public void AddMessage(string sender, string content)
    {
        messages.Add(new PhoneMessage { sender = sender, content = content, isRead = false });
        // Notify PhoneNotification
    }

    public void MarkAsRead(int index)
    {
        if (index >= 0 && index < messages.Count)
            messages[index].isRead = true;
    }
}

[System.Serializable]
public class PhoneMessage
{
    public string sender;
    public string content;
    public bool isRead;
    public string timestamp;
}
