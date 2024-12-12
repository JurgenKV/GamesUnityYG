using UnityEngine;
using YG;

public class SelectLanguage : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void OnSelectLanguageButton(string language)
    {
        YG2.lang = language;
    }
}
