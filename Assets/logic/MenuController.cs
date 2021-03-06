﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Button _startButton;

    private void Awake()
    {
        _startButton.onClick.AddListener(() => { SceneManager.LoadScene("Level1", LoadSceneMode.Single); });
    }

    private void OnDestroy()
    {
        _startButton.onClick.RemoveAllListeners();
    }
}
