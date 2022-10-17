using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

/// <summary>
/// Estados do Game Manager
/// </summary>
public enum GameState
{
    Initialization,
    Running,
    Victory,
    GameOver
}

/// <summary>
/// Classe usada para gerenciar o jogo
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // instancia do singleton

    public GameState GameState
    {
        get => _gameState;
        set
        {
            if (value == _gameState) return;
            _gameState = value;
            OnGameStateChanged();
        }
    }
    
    public int coinsToWin;
    public float timeToLose;
    
    [SerializeField] 
    private string guiName; // nome da fase de interface

    [SerializeField] 
    private string levelName; // nome da fase de jogo

    [SerializeField] 
    private GameObject playerAndCameraPrefab; // referencia pro prefab do jogador + camera

    private GameState _gameState; // variavel que guarda o estado atual do game manager
    private float _currentTime;
    private void OnEnable()
    {
        PlayerObserverManager.OnCoinsChanged += PlayerCoinsUpdate;
    }

    private void OnDisable()
    {
        PlayerObserverManager.OnCoinsChanged -= PlayerCoinsUpdate;
    }

    private void Awake()
    {
        // Condição de criação do singleton
        if (Instance == null)
        {
            Instance = this;
            
            // Impede que o objeto indicado entre parenteses seja destruido
            DontDestroyOnLoad(this.gameObject); // referencia pro objeto que contem o GameManager
        }
        else Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        // Se estiver iniciando a cena a partir de Initialization, carregue o jogo
        // do jeito de antes
        if (SceneManager.GetActiveScene().name == "Initialization")
            StartGameFromInitialization();
        else // caso contrario, esta iniciando a partir do level, carregue o jogo
             // do modo apropriado
            StartGameFromLevel();
    }

    private void StartGameFromLevel()
    {
        // 1 - Carrega a cena de interface de modo aditivo para nao apagar a cena do level
        // da memoria ram
        SceneManager.LoadScene(guiName, LoadSceneMode.Additive);
        
        // 2 - Precisa instanciar o jogador na cena
        // começa procurando o objeto PlayerStart na cena do level
        Vector3 playerStartPosition = GameObject.Find("PlayerStart").transform.position;
    
        // instancia o prefab do jogador na posicao do player start com rotação zerada
        Instantiate(playerAndCameraPrefab, playerStartPosition, Quaternion.identity);
        
        // 3 - Inicia o jogo
        // coloca o estado inicial como Running (Rodando)
        GameState = GameState.Running;
    }
    
    public void StartGame()
    {
        // 1 - Carregar a cena de interface e do jogo
        SceneManager.LoadScene(guiName);
        //SceneManager.LoadScene(levelName, LoadSceneMode.Additive); // Additive carrega uma nova cena sem descarregar a anterior

        SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive).completed += operation =>
        {
            // Inicializa a variavel para guardar a cena do level com o valor padrao (default)
            Scene levelScene = default;
            
            // encontrar a cena de level que está carregada
            // for que itera no array de cenas abertas
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                // se o nome da cena na posição i do array for igual ao nome do level
                if (SceneManager.GetSceneAt(i).name == levelName)
                {
                    // associe a cena na posição i do array à variável 
                    levelScene = SceneManager.GetSceneAt(i);
                    break;
                }
            }
            // se a variável tiver um valor diferente do padrão, significa que ela foi alterada
            // e a cena do level atual foi encontrada no array, entao faça ela ser a
            // nova cena ativa
            if(levelScene != default) SceneManager.SetActiveScene(levelScene);
            
            // 2 - Precisa instanciar o jogador na cena
            // começa procurando o objeto PlayerStart na cena do level
            Vector3 playerStartPosition = GameObject.Find("PlayerStart").transform.position;
    
            // instancia o prefab do jogador na posicao do player start com rotação zerada
            Instantiate(playerAndCameraPrefab, playerStartPosition, Quaternion.identity);
            
            // 3 - Começar a partida
            GameState = GameState.Running;
        };

    }

    private void StartGameFromInitialization()
    {
        GameState = GameState.Initialization;
        SceneManager.LoadScene("Splash");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }
    
    private void PlayerCoinsUpdate(int obj)
    {
        if (obj >= coinsToWin) GameState = GameState.Victory;
    }

    private void OnGameStateChanged()
    {
        switch (_gameState)
        {
            case GameState.Running:
                ResumeGame();
                ResetTimer();
                break;
            case GameState.Victory:
                PauseGame();
                LoadVictoryScene();
                break;
            case GameState.GameOver:
                PauseGame();
                LoadGameOverScene();
                break;
        }
    }

    private void LoadVictoryScene()
    {
        SceneManager.LoadScene("Victory", LoadSceneMode.Additive);
    }

    private void ResetTimer()
    {
        _currentTime = timeToLose;
    }

    private void Update()
    {
        if (GameState == GameState.Running)
        {
            _currentTime -= Time.deltaTime;
            if (_currentTime <= 0)
            {
                GameState = GameState.GameOver;
            }
        }

        if (GameState == GameState.Victory)
        {
            if (Keyboard.current.anyKey.wasPressedThisFrame)
            {
                StartGame();
            }
        }

        if (GameState == GameState.GameOver)
        {
            if (Keyboard.current.anyKey.wasPressedThisFrame)
            {
                GameState = GameState.Initialization;
                ResumeGame();
                LoadMainMenu();
            }
        }
    }

    private void LoadGameOverScene()
    {
        SceneManager.LoadScene("GameOver", LoadSceneMode.Additive);
    }
}
