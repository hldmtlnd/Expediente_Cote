using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MessageUI : MonoBehaviour
{
    public MessageSystem messageSystem;
    public Transform messageListParent;
    public GameObject messagePrefab;

    public void RefreshMessages()
    {
        foreach (Transform child in messageListParent)
            Destroy(child.gameObject);

        foreach (var msg in messageSystem.messages)
        {
            // Instantiate messagePrefab and set text
        }
    }

    public void OpenMessage(int index)
    {
        messageSystem.MarkAsRead(index);
        // Show full message
    }
}
