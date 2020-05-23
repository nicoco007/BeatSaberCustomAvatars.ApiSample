using CustomAvatar.Tracking;
using UnityEngine;

namespace BeatSaberCustomAvatars.ApiSample
{
    internal class DummyInput : AvatarInput
    {
        public override bool TryGetHeadPose(out Pose pose)
        {
            pose = new Pose(new Vector3(0, 1.665f, -1), Quaternion.Euler(0, 0, 0));
            return true;
        }

        public override bool TryGetLeftHandPose(out Pose pose)
        {
            pose = new Pose(new Vector3(-0.3f, 0.6f, -1), Quaternion.Euler(0, 0, 0));
            return true;
        }

        public override bool TryGetRightHandPose(out Pose pose)
        {
            pose = new Pose(new Vector3(0.3f, 0.6f, -1), Quaternion.Euler(0, 0, 0));
            return true;
        }
    }
}
