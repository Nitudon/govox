using UniRx;

namespace govox
{
    public class VoxelBuilderModel
    {
        private VoxelHitInfo _rayHitInfo;
        public VoxelHitInfo RayHitInfo => _rayHitInfo;

        private bool _isHit;
        public bool IsHit => _isHit;

        public void SetRayHitInfo(VoxelHitInfo info)
        {
            _isHit = true;
            _rayHitInfo = info;
        }

        public void ClearHitInfo()
        {
            _isHit = false;
        }
    }
}