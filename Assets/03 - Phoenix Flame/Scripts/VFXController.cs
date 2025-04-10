using System;
using UnityEngine;

namespace _03___Phoenix_Flame.Scripts
{
    [RequireComponent(typeof(Animator))]
    public class VFXController : MonoBehaviour
    {
        static readonly int Start = Animator.StringToHash("start");
        static readonly int End = Animator.StringToHash("end");
        Animator _animator;

        void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void TriggerStateChange()
        {
            var clipName = _animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;

            var trigger = Start;
            
            if(clipName is "Start" or "Idle")
                trigger = End;
            
            _animator.SetTrigger(trigger);
        }
    }
}