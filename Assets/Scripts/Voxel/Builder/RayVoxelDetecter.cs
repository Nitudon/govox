using UdonLib.Commons;

namespace govox
{
    public class RayVoxelDetecter : UdonBehaviour, IRayHandler
    {
        private RayCastMeshCollisionUseCase _pointDetectUseCase;

        private const float RAY_LENGTH = 200f;
    }
}