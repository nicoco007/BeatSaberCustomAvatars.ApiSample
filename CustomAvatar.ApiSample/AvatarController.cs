﻿using CustomAvatar.Avatar;
using CustomAvatar.Player;
using System;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace CustomAvatar.ApiSample
{
    internal class AvatarController : IInitializable, IDisposable
    {
        private GameScenesManager _gameScenesManager;
        private AvatarSpawner _avatarSpawner;
        private PlayerAvatarManager _avatarManager;
        private VRPlayerInput _playerInput;
        private FloorController _floorController;

        private GameObject _container;
        private SpawnedAvatar _spawnedAvatar;

        public void Initialize()
        {
            _gameScenesManager.transitionDidFinishEvent += OnTransitionDidFinish;
            _avatarManager.avatarChanged += OnAvatarChanged;
            _avatarManager.avatarScaleChanged += OnAvatarScaleChanged;
            _floorController.floorPositionChanged += OnFloorPositionChanged;

            _container = new GameObject(nameof(AvatarController));
            Object.DontDestroyOnLoad(_container);

            _container.transform.position = new Vector3(0, 0, -2);
            _container.transform.rotation = Quaternion.Euler(0, 180, 0);

            OnAvatarChanged(_avatarManager.currentlySpawnedAvatar);
        }

        public void Dispose()
        {
            Object.Destroy(_container);

            _gameScenesManager.transitionDidFinishEvent -= OnTransitionDidFinish;
            _avatarManager.avatarChanged -= OnAvatarChanged;
        }

        [Inject]
        internal void Inject(GameScenesManager gameScenesManager, AvatarSpawner avatarSpawner, PlayerAvatarManager avatarManager, VRPlayerInput playerInput, FloorController floorController)
        {
            _gameScenesManager = gameScenesManager;
            _avatarSpawner = avatarSpawner;
            _avatarManager = avatarManager;
            _playerInput = playerInput;
            _floorController = floorController;
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

        private void OnAvatarChanged(SpawnedAvatar avatar)
        {
            Object.Destroy(_spawnedAvatar);

            if (!avatar) return;

            _spawnedAvatar = _avatarSpawner.SpawnAvatar(avatar.prefab, new MirrorInput(_playerInput), _container.transform);
            _spawnedAvatar.scale = avatar.scale;
            _spawnedAvatar.SetFirstPersonVisibility(FirstPersonVisibility.Visible);

            // call our custom component's method
            // you can just use GetComponent if you're sure your component will always be there (i.e. not using a condition when registering the component)
            if (_spawnedAvatar.TryGetComponent(out SampleAvatarComponent comp)) comp.DoSomething();
        }

        private void OnAvatarScaleChanged(float scale)
        {
            if (!_spawnedAvatar) return;

            _spawnedAvatar.scale = scale;
        }

        private void OnFloorPositionChanged(float verticalPosition)
        {
            if (!_spawnedAvatar) return;

            _container.transform.position = new Vector3(0, verticalPosition, -2);
        }

        private void SetAvatarActive(bool active)
        {
            if (!_spawnedAvatar) return;

            _spawnedAvatar.gameObject.SetActive(active);
        }
    }
}
