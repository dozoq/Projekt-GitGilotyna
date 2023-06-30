using System;
using Code.General.States.StateFactory;
using Code.Player.States.StateFactory;
using Code.Utilities;
using MySql.Data.MySqlClient;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.General
{
    public class GameManager : Singleton<GameManager>
    {
        public CashSystem cashSystem { get; private set; }
        public SoundSystem soundSystem { get; private set; }

        private int _killCount = 0;

        public int killCount
        {
            get
            {
                return _killCount;
            }
            set
            {
                _killCount = value;
                onStatsChangedAction?.Invoke();
            }
        }

        private int _remainingCount = 0;
        public int remainingCount
        {
            get
            {
                return _remainingCount;
            }
            set
            {
                _remainingCount = value;
                onStatsChangedAction?.Invoke();
            }
        }

        public Action onStatsChangedAction;

        private DateTime _sessionStartTime;
        private DateTime _sessionEndTime;

        private IState<GameManager> _menuState, _playState;
        private StateContext<IState<GameManager>, GameManager> _stateContext;

        protected override void Awake()
        {
            base.Awake();
            _sessionStartTime = DateTime.Now;
            _stateContext = new StateContext<IState<GameManager>, GameManager>(this);
            _menuState = GameStateFactory.Get("MenuState");
            _playState = GameStateFactory.Get("PlayState");            
            if (gameObject.GetComponent<CashSystem>() == null) gameObject.AddComponent<CashSystem>();
            cashSystem = this.GetComponent<CashSystem>();
            soundSystem = this.GetComponent<SoundSystem>();
            if (!soundSystem) throw new Exception("Sound System Not Set");
        }

        private void Start()
        {
            //testDB();
        }

        private void OnEnable()
        {
            GetComponent<RandomTimeInvoker>().enabled = SceneManager.GetActiveScene().buildIndex != 0;
            _remainingCount = 0;
        }

        private void OnApplicationQuit()
        {
            _sessionEndTime = DateTime.Now;

            TimeSpan timeDifference = _sessionEndTime.Subtract(_sessionStartTime);
        }

        private void LoadNextScene()
        {
            SceneManager.LoadScene(
                SceneManager.GetActiveScene().buildIndex + 1
            );
        }

        public void GoToMainMenu()
        {
            SceneManager.LoadScene(0);
            _stateContext.Transition(_menuState);
        }

        public static void QuitGame()
        {
            Application.Quit();
        }
        private void testDB()
        {
            Debug.Log("Starting Connection...");
            string connStr = "";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                Debug.Log("Connecting to MySQL...");
                conn.Open();
                var stm = "SELECT VERSION()";
                var cmd = new MySqlCommand(stm, conn);

                var version = cmd.ExecuteScalar().ToString();
                Debug.Log($"MySQL version: {version}");
            }
            catch (Exception ex)
            {
                Debug.Log(ex.ToString());
            }
            conn.Close();
            Debug.Log("Done.");
        }
        
    }
}