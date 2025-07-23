using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int MaxNumberOfShots = 3;
    [SerializeField] private float _secondToWaitBeforeDeathCheck = 5f;
    [SerializeField] private GameObject _restartScreenObject;
    [SerializeField] private SlingShotHandler _slingShotHandler;
    [SerializeField] private Image _nextLevelImage;
    [SerializeField] private GameObject _congratulationText;
    [SerializeField] private AudioSource _winAudio;
    private int _usedNumberOfShots = 0;

    private IconHandler _iconHandler;

    private List<Baddie> _baddies = new List<Baddie>();

    private void Start()
    {
        // Reset UI khi vào màn chơi mới
        _restartScreenObject.SetActive(false);
        _nextLevelImage.enabled = false;
        _congratulationText.SetActive(false);

        _usedNumberOfShots = 0;
    }


    private void Awake()
    {
        // Singleton pattern
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        _iconHandler = FindObjectOfType<IconHandler>();
    }

    // 📌 Gọi từ Baddie.Start()
    public void RegisterBaddie(Baddie baddie)
    {
        _baddies.Add(baddie);

        _nextLevelImage.enabled = false;
    }

    // 📌 Gọi từ Baddie.Die()
    public void RemoveBaddies(Baddie baddie)
    {
        _baddies.Remove(baddie);
        CheckForAllDeathBaddies();
    }

    // 📌 Gọi mỗi khi bắn
    public void UseShot()
    {
        _usedNumberOfShots++;
        _iconHandler.UseShot(_usedNumberOfShots);
    }

    // 📌 Kiểm tra còn bắn được không
    public bool HasEnoughShots()
    {
        return _usedNumberOfShots < MaxNumberOfShots;
    }

    // 📌 Gọi sau mỗi phát bắn để kiểm tra đã hết lượt chưa
    public void CheckForLastShot()
    {
        if (_usedNumberOfShots == MaxNumberOfShots)
        {
            StartCoroutine(CheckAfterWaitTime());
        }
    }

    // 📌 Sau khi bắn hết, chờ vài giây rồi xem còn Baddie không
    private IEnumerator CheckAfterWaitTime()
    {
        yield return new WaitForSeconds(_secondToWaitBeforeDeathCheck);


        if (_baddies.Count == 0)
        {
            WinGame();
        }
        else
        {
            RestartGame();
        }
    }

    private void CheckForAllDeathBaddies()
    {
        if (_baddies.Count == 0)
        {
            WinGame();
        }
    }

    private void WinGame()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int maxLevels = SceneManager.sceneCountInBuildSettings;

        if (SceneManager.GetActiveScene().name == "End")
        {
            _restartScreenObject.SetActive(false);
            _nextLevelImage.enabled = false;
            _congratulationText.SetActive(true);
            _winAudio.Play();
            
            StartCoroutine(ReturnToLevel1());
        }
        else
        {
            _restartScreenObject.SetActive(true);
            _nextLevelImage.enabled = true;
        }

        _slingShotHandler.enabled = false;
    }






    public void RestartGame()
    {
        DOTween.Clear(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private IEnumerator ReturnToLevel1()
    {
        yield return new WaitForSeconds(5f); 
        SceneManager.LoadScene("End");
    }

}
