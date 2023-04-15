using System;
using Code.General.States.StateFactory;
using Code.Player.States.StateFactory;
using Code.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.General
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private AudioSource source;
        
        private DateTime _sessionStartTime;
        private DateTime _sessionEndTime;

        private IState<GameManager> _menuState, _playState;
        private StateContext<IState<GameManager>, GameManager> _stateContext;
        public CashSystem cashSystem { get; private set; }
        protected override void Awake()
        {
            base.Awake();
            _sessionStartTime = DateTime.Now;
            _stateContext = new StateContext<IState<GameManager>, GameManager>(this);
            _menuState = GameStateFactory.Get("MenuState");
            _playState = GameStateFactory.Get("PlayState");            
            if (gameObject.GetComponent<CashSystem>() == null) gameObject.AddComponent<CashSystem>();
            cashSystem = this.GetComponent<CashSystem>();
        }

        private void OnApplicationQuit()
        {
            _sessionEndTime = DateTime.Now;

            TimeSpan timeDifference = _sessionEndTime.Subtract(_sessionStartTime);
        }

        private void OnGUI()
        {
            if (GUILayout.Button("NextScene"))
            {
                LoadNextScene();
                _stateContext.Transition(_playState);
            }
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

        public void MuteGame()
        {
            source.volume = 0;
        }
    }
}