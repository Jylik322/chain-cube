using System;
using System.IO;
using UnityEngine;

public enum UI_Texts { play, score, record, gameOver, restart, menu}
public class Language : MonoBehaviour
{
    [SerializeField]private GameObject englishButton;
    [SerializeField]private GameObject russianButton;
    public static Action LanguageChanged = delegate { };
    private void Start()
    {
        if (PlayerPrefs.GetString("Language")=="English")
        {
            russianButton.SetActive(false);
            englishButton.SetActive(true);
        }
        else
        {
            russianButton.SetActive(true);
            englishButton.SetActive(false);
        }
    }
    public void SetLanguage(string value)
    {
        PlayerPrefs.SetString("Language", value);
        LanguageChanged.Invoke();
    }

}
[Serializable]
class LanguageClass
{
    public string play;
    public string score;
    public string record;
    public string gameOver;
    public string restart;
    public string menu;
}