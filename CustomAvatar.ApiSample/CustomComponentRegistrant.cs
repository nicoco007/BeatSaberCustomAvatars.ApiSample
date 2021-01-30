using CustomAvatar.Avatar;
using System;
using Zenject;

namespace CustomAvatar.ApiSample
{
    internal class CustomComponentRegistrant : IInitializable, IDisposable
    {
        private AvatarSpawner _avatarSpawner;

        public CustomComponentRegistrant(AvatarSpawner avatarSpawner)
        {
            _avatarSpawner = avatarSpawner;
        }

        public void Initialize()
        {
            _avatarSpawner.RegisterComponent<SampleAvatarComponent>();
        }

        public void Dispose()
        {
            _avatarSpawner.DeregisterComponent<SampleAvatarComponent>();
        }
    }
}
