using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance 
    {  
        get; 
        private set;
    }
    int _lives = 5;
    public int Lives
    {
        get 
        { 
            return _lives; 
        }
        set 
        {
            _lives = value;
            if(_livesText != null) 
            {
                _livesText.text = _lives.ToString();
            }
        }
    }
    const int _startingBricksLevelOne = 84;
    const int _startingBricksLevelTwo = 104;
    const int _startingBricksLevelThree = 50;
    int _bricks;
    bool levelIsTransitioning = false;
    int Bricks 
    {
        get
        {
            return _bricks;
        }
        set
        {
            _bricks = value;
            if(_bricksBrokenText != null)
            {
                if (SceneManager.GetActiveScene().name == "Level One")
                {
                    _bricksBrokenText.text = (_startingBricksLevelOne - _bricks).ToString();
                }
                if (SceneManager.GetActiveScene().name == "Level Two")
                {
                    _bricksBrokenText.text = (_startingBricksLevelTwo - _bricks).ToString();
                }
                if (SceneManager.GetActiveScene().name == "Level Three")
                {
                    _bricksBrokenText.text = (_startingBricksLevelThree - _bricks).ToString();
                }
                
            }
        }
    }
    float _resetDelay = 1f;

    [SerializeField] TMP_Text _livesText;
    [SerializeField] TMP_Text _bricksBrokenText;
    [SerializeField] GameObject _gameOverText;
    [SerializeField] GameObject _youWonText;
    [SerializeField] GameObject _bricksPrefabLevelOne;
    [SerializeField] GameObject _bricksPrefabLevelTwo;
    [SerializeField] GameObject _bricksPrefabLevelThree;
    [SerializeField] GameObject _paddlePrefab;
    private GameObject _clonePaddle;
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            for(int i = gameObject.transform.childCount - 1; i >= 0; i--)
            {
                Destroy(gameObject.transform.GetChild(i).gameObject);
            }
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.N)) 
        {
            Bricks = 0;
            CheckGameOver();
        }
    }
    public void Setup()
    {
        _clonePaddle = Instantiate(_paddlePrefab, transform.position, Quaternion.identity);
        if(SceneManager.GetActiveScene().name == "Level One")
        {
            Lives = 5;
            Instantiate(_bricksPrefabLevelOne, transform.position, Quaternion.identity);
            Bricks = _startingBricksLevelOne;
        }
        if (SceneManager.GetActiveScene().name == "Level Two")
        {
            Instantiate(_bricksPrefabLevelTwo, transform.position, Quaternion.identity);
            Bricks = _startingBricksLevelTwo;
        }
        if (SceneManager.GetActiveScene().name == "Level Three")
        {
            Instantiate(_bricksPrefabLevelThree, transform.position, Quaternion.identity);
            Bricks = _startingBricksLevelThree;
        }
    }
    void CheckGameOver()
    {
        if (!levelIsTransitioning)
        {
            if (Bricks < 1)
            {
                if (_youWonText != null)
                {
                    _youWonText.SetActive(true);
                }
                Time.timeScale = .25f;
                Invoke("TransitionToNextLevel", _resetDelay);
                levelIsTransitioning = true;
            }
            if (Lives < 1)
            {
                if (_gameOverText != null)
                {
                    _gameOverText.SetActive(true);
                }
                Time.timeScale = .25f;
                Invoke("GoToMainMenu", _resetDelay);
                levelIsTransitioning = true;
            }
        }
    }
    public void LoseLife()
    {
        if (!levelIsTransitioning)
        {
            Lives--;
            Destroy(_clonePaddle);
            Invoke("SetupPaddle", _resetDelay);
            CheckGameOver();
        }
    }
    void SetupPaddle()
    {
        _clonePaddle = Instantiate(_paddlePrefab, transform.position, Quaternion.identity);
    }
    public void DestroyBrick()
    {
        Bricks--;
        CheckGameOver();
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode) 
    {
        levelIsTransitioning = false;
        Setup();
    }
    void TransitionToNextLevel()
    {
        _gameOverText.SetActive(false);
        _youWonText.SetActive(false);
        Time.timeScale = 1f;
        if (SceneManager.GetActiveScene().name == "Level One")
        {
            SceneManager.LoadScene("Level Two");
        }
        else if (SceneManager.GetActiveScene().name == "Level Two")
        {
            SceneManager.LoadScene("Level Three");
        }
        else if (SceneManager.GetActiveScene().name == "Level Three")
        {
            SceneManager.LoadScene("Main Menu");
        }
    }
    void GoToMainMenu()
    {
        _gameOverText.SetActive(false);
        _youWonText.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }
}


