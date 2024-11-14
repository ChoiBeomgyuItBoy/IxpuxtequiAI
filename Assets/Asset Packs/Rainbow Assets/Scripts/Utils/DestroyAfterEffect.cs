using UnityEngine;
using UnityEngine.VFX;

namespace RainbowAssets.Utils
{
    public class DestroyAfterEffect : MonoBehaviour
    {
        VisualEffect visualEffect;

        void Awake()
        {
            visualEffect = GetComponent<VisualEffect>();
        }

        void Update()
        {
            if(visualEffect.aliveParticleCount == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}