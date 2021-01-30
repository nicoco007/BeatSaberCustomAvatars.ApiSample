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
            // ShouldAddSampleComponent can be omitted if you always want the component to be added
            _avatarSpawner.RegisterComponent<SampleAvatarComponent>(ShouldAddSampleComponent);
        }

        public void Dispose()
        {
            _avatarSpawner.DeregisterComponent<SampleAvatarComponent>();
        }

        private bool ShouldAddSampleComponent(LoadedAvatar avatar)
        {
            // add some actual logic here instead
            return true;
        }
    }
}
