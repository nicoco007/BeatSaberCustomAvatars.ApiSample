using CustomAvatar.Avatar;
using System;
using System.IO;
using Zenject;
using Object = UnityEngine.Object;

namespace BeatSaberCustomAvatars.ApiSample
{
    internal class DummyAvatarController : IDisposable
    {
        private GameScenesManager _gameScenesManager;
        private AvatarLoader _avatarLoader;
        private AvatarSpawner _avatarSpawner;

        private SpawnedAvatar _spawnedAvatar;

        public void Dispose()
        {
            Object.Destroy(_spawnedAvatar);

            _gameScenesManager.transitionDidFinishEvent -= OnTransitionDidFinish;
        }

        [Inject]
        private void Inject(GameScenesManager gameScenesManager, AvatarLoader avatarLoader, AvatarSpawner avatarSpawner)
        {
            _gameScenesManager = gameScenesManager;
            _avatarLoader = avatarLoader;
            _avatarSpawner = avatarSpawner;

            _gameScenesManager.transitionDidFinishEvent += OnTransitionDidFinish;

            SpawnAvatar();
        }

        private void OnTransitionDidFinish(ScenesTransitionSetupDataSO setupData, DiContainer container)
        {
            // hide when in-game
            if (_gameScenesManager.IsSceneInStack("GameCore"))
            {
                SetAvatarActive(false);
            }
            else
            {
                SetAvatarActive(true);
            }
        }

        private void SpawnAvatar()
        {
            SharedCoroutineStarter.instance.StartCoroutine(
                _avatarLoader.FromFileCoroutine(Path.Combine(Directory.GetCurrentDirectory(), "CustomAvatars", "TemplateFullBody.avatar"), OnAvatarLoaded)
            );
        }

        private void OnAvatarLoaded(LoadedAvatar loadedAvatar)
        {
            _spawnedAvatar = _avatarSpawner.SpawnAvatar(loadedAvatar, new DummyInput());
            _spawnedAvatar.scale = 1.55f / _spawnedAvatar.eyeHeight;
            _spawnedAvatar.UpdateFirstPersonVisibility(FirstPersonVisibility.Visible);
        }

        private void SetAvatarActive(bool active)
        {
            if (!_spawnedAvatar) return;

            _spawnedAvatar.gameObject.SetActive(active);
        }
    }
}
