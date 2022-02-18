using System;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
public class Score : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private Text recordText;
    [SerializeField] private Transform gameOverPanel;
    LanguageClass languageClass;

    public static Action<long> ScoreChanged = delegate { };
    public static Action OnGameOver = delegate { };
    private long _score;
    public long score
    {
        set
        {
            _score = value;
            if (_score > record)
            {
                record = score;
                PlayerPrefs.SetString("Record", _score.ToString());
            }
            scoreText.text = value.ToString(); 
            recordText.text = languageClass.record +": "+ record;
        }
        get { return _score; }
    }
    private long record;
    private void Start()
    {
        Time.timeScale = 1f;
        //scoreText.text = score.ToString();
        //recordText.text = languageClass.record + record;
        string language = PlayerPrefs.GetString("Language");
        string json = File.ReadAllText(Application.dataPath + "/" + language.ToLower() + ".json", encoding: Encoding.UTF8);
        languageClass = JsonUtility.FromJson<LanguageClass>(json);
        if (PlayerPrefs.HasKey("Record"))
        {
            record = long.Parse(PlayerPrefs.GetString("Record"));
            Debug.Log(record);
        }
        else
        {
            PlayerPrefs.SetInt("Record", 0);
            record = 0;
        }
        score = 0;
    }
    public void ChangeScore(long value)
    {
        score += value;
    }
    public void ActivateGameOverPanel()
    {
        gameOverPanel.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }
    private void OnEnable()
    {
        ScoreChanged += ChangeScore;
        OnGameOver += ActivateGameOverPanel;

    }
    private void OnDisable()
    {
        ScoreChanged -= ChangeScore;
        OnGameOver -= ActivateGameOverPanel;
    }
}
