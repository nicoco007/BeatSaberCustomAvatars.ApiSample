using CustomAvatar.Avatar;
using UnityEngine;
using Zenject;

namespace CustomAvatar.ApiSample
{
    public class SampleAvatarComponent : MonoBehaviour
    {
        private SpawnedAvatar _avatar;

        [Inject]
        public void Construct(SpawnedAvatar avatar)
        {
            _avatar = avatar;
        }

        public void Start()
        {
            Debug.Log($"Spawned avatar's name is '{_avatar.avatar.descriptor.name}'");
        }
    }
}
