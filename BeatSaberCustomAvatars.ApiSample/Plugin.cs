using System.IO;
using CustomAvatar;
using CustomAvatar.Avatar;
using IPA;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using Logger = IPA.Logging.Logger;

namespace BeatSaberCustomAvatars.ApiSample
{
    [Plugin(RuntimeOptions.DynamicInit)]
    internal class Plugin
    {
        private readonly Logger _logger;

        private AvatarLoader _avatarLoader;
        private AvatarSpawner _avatarSpawner;
        private GameScenesManager _gameScenesManager;

        private bool _avatarSpawned;
        private SpawnedAvatar _spawnedAvatar;

        [Init]
        public Plugin(Logger logger)
        {
            _logger = logger;
        }

        [OnEnable]
        public void OnEnable()
        {
            _logger.Info("Plugin enabled");

            SceneManager.sceneLoaded += OnSceneLoaded;
            
            // handle enable when game is already running
            if (_gameScenesManager && _gameScenesManager.IsSceneInStack("MainMenu") && !_gameScenesManager.IsSceneInStack("GameCore"))
            {
                SpawnAvatar();
            }
        }

        [OnDisable]
        public void OnDisable()
        {
            _logger.Info("Plugin disabled");

            SceneManager.sceneLoaded -= OnSceneLoaded;

            DestroyAvatar();
        }

        // you can also use [Inject] on the fields, but using an [Inject] method/constructor is considered best practice
        [Inject]
        private void Inject(AvatarLoader avatarLoader, AvatarSpawner avatarSpawner, GameScenesManager gameScenesManager)
        {
            _avatarLoader = avatarLoader;
            _avatarSpawner = avatarSpawner;
            _gameScenesManager = gameScenesManager;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "PCInit")
            {
                ZenjectHelper.GetMainSceneContextAsync(OnSceneContextInstalled);
            }
        }

        private void OnSceneContextInstalled(SceneContext sceneContext)
        {
            sceneContext.Container.Inject(this);

            _gameScenesManager.transitionDidFinishEvent += OnTransitionDidFinish;
        }

        private void OnTransitionDidFinish(ScenesTransitionSetupDataSO setupData, DiContainer container)
        {
            // only show in main menu (MainMenu scene is still loaded when in game)
            if (_gameScenesManager.IsSceneInStack("MainMenu") && !_gameScenesManager.IsSceneInStack("GameCore"))
            {
                SpawnAvatar();
            }
            else
            {
                DestroyAvatar();
            }
        }

        private void SpawnAvatar()
        {
            if (_avatarSpawned) return;

            _logger.Info("Spawning avatar");

            // inside a MonoBehaviour you can simply call StartCoroutine
            SharedCoroutineStarter.instance.StartCoroutine(_avatarLoader.FromFileCoroutine(
                Path.Combine(Directory.GetCurrentDirectory(), "CustomAvatars", "TemplateFullBody.avatar"),
                loadedAvatar =>
                {
                    _spawnedAvatar = _avatarSpawner.SpawnAvatar(loadedAvatar, new DummyInput());
                    _spawnedAvatar.scale = 1.665f / _spawnedAvatar.eyeHeight;
                }));

            // prevent multiple spawns if OnTransitionDidFinish is called quickly since coroutine might take a few frames
            _avatarSpawned = true;
        }

        private void DestroyAvatar()
        {
            _logger.Info("Destroying avatar");

            Object.Destroy(_spawnedAvatar);
            _avatarSpawned = false;
        }
    }
}
