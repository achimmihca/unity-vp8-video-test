using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class SampleSceneControl : MonoBehaviour
{
    public TextAsset videoAsset;
    public VideoPlayer videoPlayer;

    public TMP_Text uiText;
    public TMP_Dropdown uriDropdown;

    private void Start()
    {
        List<string> paths = new List<string>
        {
            Application.persistentDataPath + "video.webm",
            Application.persistentDataPath + "video.vp8",
        };

        // Copy video to persistent data path
        paths.ForEach(path => File.WriteAllBytes(path, videoAsset.bytes));

        // URI to play video
        List<string> uris = paths.Select(path => $"file://{path}")
            // Absolute path without "file://" scheme
            .Concat(paths)
            .ToList();

        // Change VideoPlayer uri when dropdown changes
        uriDropdown.options = uris.Select(url => new TMP_Dropdown.OptionData(url))
            .ToList();
        uriDropdown.onValueChanged.AddListener(index => SetUrl(uris[index]));

        // Show persistent data path in UI
        uiText.text = "persistentDataPath: " + Application.persistentDataPath;
    }

    private void SetUrl(string url)
    {
        videoPlayer.url = url;
        uiText.text = "url: " + videoPlayer.url;
    }
}