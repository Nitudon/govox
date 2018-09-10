using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Animations;

namespace UdonLib.Commons
{
    public class SimpleAnimation : UdonBehaviour
    {
        [SerializeField]
        private Animator _animator;

        private PlayableGraph _graph;
        private AnimationPlayableOutput _output;
        private AnimationClipPlayable _clip;
        private int _clipTotalTime;

        public int AnimationTime => (int)_clip.GetTime();
        public int LastAnimationTime => (int)(_clipTotalTime - AnimationTime);

        public bool IsPlaying => _graph.IsPlaying();

        public void Initialize()
        {
            _graph = PlayableGraph.Create();
        }

        public void SetAnimator(Animator animator)
        {
            _animator = animator;
        }

        public void SetAnimation(AnimationClip clip)
        {
            _clip = AnimationClipPlayable.Create(_graph, clip);
            _output = AnimationPlayableOutput.Create(_graph, "output", _animator);
            _output.SetSourcePlayable(_clip);
            _clipTotalTime = (int)clip.length;
        }

        public void PlayAnimation()
        {
            _graph.Play();
        }

        public void StopAnimation()
        {
            _graph.Stop();
        }

        void OnDestroy()
        {
            _graph.Destroy();
        }
    }
}