using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
public class Check : MonoBehaviour
{
    public Button yourButton,button2;

    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(LoadByIndex);

        Button btn2 = button2.GetComponent<Button>();
        btn2.onClick.AddListener(Quit);
    }

    void TaskOnClick()
    {
        Debug.Log("You have clicked the button!");
    }
    public void LoadByIndex()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit ();
#endif
    }
}