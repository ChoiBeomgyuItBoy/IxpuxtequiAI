using UnityEngine;

namespace RainbowAssets.BehaviourTree.Actions
{
    public class PlaySoundEffect : ActionNode
    {
        [SerializeField] AudioClip[] clips; 
        [SerializeField] float delayBetweenClips = 10f; 
        [SerializeField] bool stopWhenAbort = false;
        AudioSource audioSource;
        float currentTime = 0f;
        bool clipPlaying = false;

        public override void Abort()
        {
            if(stopWhenAbort)
            {
                audioSource.Stop();
            }

            base.Abort();
        }

        protected override void OnEnter()
        {
            audioSource = controller.GetComponent<AudioSource>();
            currentTime = delayBetweenClips;
        }

        protected override Status OnTick()
        {
            currentTime -= Time.deltaTime;

            if(currentTime <= 0f && !clipPlaying)
            {
                AudioClip randomClip = clips[Random.Range(0, clips.Length)];

                audioSource.PlayOneShot(randomClip);

                clipPlaying = true;

                currentTime = randomClip.length + delayBetweenClips;
            }

            if(clipPlaying && !audioSource.isPlaying)
            {
                clipPlaying = false;
            }

            return Status.Running;
        }

        protected override void OnExit() { }
    }
}
