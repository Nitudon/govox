namespace UdonLib.Commons
{
    public interface IRayHandler
    {
        void OnRayHit(UnityEngine.RaycastHit hit);
    }

    public interface IRayExitHandler
    {
        void OnRayExit();
    }

    public interface IRayTriggerHandler : IRayHandler, IRayExitHandler
    {

    }
}