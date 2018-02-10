using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationController : MonoBehaviour
{
    #region Animation
    [Header("Animation")]
    [Tooltip("(Maximal) Animation speed in percent: < 1 slower, > 1 faster, 1 = normal speed")]
    public float animationSpeed = 1f;
    [Tooltip("Minimal animation speed in percent. Only relevant while using a controller.")]
    public float minAnimationSpeed = 0f;
    #endregion

    #region Components
    [HideInInspector]
    public Animator animator;
    #endregion

    void Start ()
    {
        animator = GetComponent<Animator>();
        animator.speed = animationSpeed;
    }
}
