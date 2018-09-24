namespace UdonLib.Commons
{
    public interface IRayHandler
    {
        void OnRayHit(UnityEngine.RaycastHit hit);
    }

    public interface IRayExitHandler
    {
        void OnRayExit(UnityEngine.RaycastHit hit);
    }

    public interface IRayTriggerHandler : IRayHandler, IRayExitHandler
    {

    }
}