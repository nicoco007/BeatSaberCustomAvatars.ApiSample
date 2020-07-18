using CustomAvatar.Tracking;
using System;
using UnityEngine;

namespace BeatSaberCustomAvatars.ApiSample
{
    internal class DummyInput : IAvatarInput
    {
        public bool allowMaintainPelvisPosition => false;

        public event Action inputChanged;

        public bool TryGetHeadPose(out Pose pose)
        {
            pose = new Pose(new Vector3(0, 1.665f, -1), Quaternion.Euler(0, 0, 0));
            return true;
        }

        public bool TryGetLeftHandPose(out Pose pose)
        {
            pose = new Pose(new Vector3(-0.3f, 0.6f, -1), Quaternion.Euler(0, 0, 0));
            return true;
        }

        public bool TryGetRightHandPose(out Pose pose)
        {
            pose = new Pose(new Vector3(0.3f, 0.6f, -1), Quaternion.Euler(0, 0, 0));
            return true;
        }

        public bool TryGetWaistPose(out Pose pose)
        {
            pose = Pose.identity;
            return false;
        }

        public bool TryGetLeftFootPose(out Pose pose)
        {
            pose = Pose.identity;
            return false;
        }

        public bool TryGetRightFootPose(out Pose pose)
        {
            pose = Pose.identity;
            return false;
        }

        public bool TryGetLeftHandFingerCurl(out FingerCurl curl)
        {
            curl = null;
            return false;
        }

        public bool TryGetRightHandFingerCurl(out FingerCurl curl)
        {
            curl = null;
            return false;
        }

        public void Dispose() { }
    }
}
