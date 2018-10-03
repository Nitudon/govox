using UdonLib.Commons;

namespace govox
{
    public class VoxelCreator : InitializableMono
    {
        private VoxelFactory _voxelFactory;
        
        public override void Initialize()
        {
            _voxelFactory = new VoxelFactory();
        }
    }
}