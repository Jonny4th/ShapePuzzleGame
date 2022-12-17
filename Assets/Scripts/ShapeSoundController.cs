using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ShapeSoundController : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    private void OnEnable()
    {
        ShapeMovementManager.Moved += OnMove;
    }

    private void OnDisable()
    {
        ShapeMovementManager.Moved -= OnMove;
    }

    private void OnMove(OnMovementInfo obj)
    {
        if (obj.shape == this.gameObject)
        {
            audioSource.Play();
        }
    }
}
