using UniRx;

namespace govox
{
    public class VoxelBuilderModel
    {
        private VoxelHitInfo _rayHitInfo;
        public VoxelHitInfo RayHitInfo => _rayHitInfo;

        public void SetRayHitInfo(VoxelHitInfo info)
        {
            _rayHitInfo = info;
        }
    }
}