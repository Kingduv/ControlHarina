using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public Button logoutbutton;

    public void LogOut()
    {
        SceneManager.LoadScene(0);
    }
}
