using UnityEngine;

public class WinButton : MonoBehaviour
{
    public void OnClickWinButton(string text)
    {
        FindAnyObjectByType<SceneLoadController>().LoadSceneWithAnim(text);
    }
}
