using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GalleryUI : MonoBehaviour
{
    public Transform contentParent;
    public GameObject photoPrefab;
    public GameObject videoPrefab;

    private List<string> capturedMedia = new List<string>();

    public void AddPhoto(string path)
    {
        capturedMedia.Add(path);
        RefreshGallery();
    }

    public void AddVideo(string path)
    {
        capturedMedia.Add(path);
        RefreshGallery();
    }

    void RefreshGallery()
    {
        // Clear and repopulate UI elements
        foreach (Transform child in contentParent)
            Destroy(child.gameObject);

        foreach (var media in capturedMedia)
        {
            // Instantiate photo or video prefab based on extension
        }
    }

    public void ViewMedia(int index)
    {
        // Show full screen view
    }
}
