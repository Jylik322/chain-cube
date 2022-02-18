using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class LoadText : MonoBehaviour
{
    private Text textReference;
    [SerializeField]private UI_Texts ui_text;
    private void Start()
    {
        textReference = GetComponent<Text>();
        ChangeText();
    }
    private void OnEnable()
    {
        Language.LanguageChanged += ChangeText;
    }
    private void OnDisable()
    {
        Language.LanguageChanged -= ChangeText;
    }
    private void ChangeText()
    {
        string language = PlayerPrefs.GetString("Language");
        string json = File.ReadAllText(Application.dataPath + "/" + language.ToLower() + ".json",encoding:Encoding.UTF8);
        LanguageClass languageClass = JsonUtility.FromJson<LanguageClass>(json);
        Debug.Log(languageClass.score);
       switch (ui_text)
        {
            case UI_Texts.play:
                textReference.text = languageClass.play;
                break;

            case UI_Texts.record:
                textReference.text = languageClass.record;
                break;

            case UI_Texts.score:
                textReference.text = languageClass.score;
                break;
            case UI_Texts.gameOver:
                textReference.text = languageClass.gameOver;
                break;

            case UI_Texts.restart:
                textReference.text = languageClass.restart;
                break;

            case UI_Texts.menu:
                textReference.text = languageClass.menu;
                break;
        }
    }
}
