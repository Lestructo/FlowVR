using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Diagnostics.CodeAnalysis;

namespace VRCoordinationTraining
{
#nullable enable
	[RequireComponent(typeof(Animator))]
	public class HandAnimator : MonoBehaviour
	{
#pragma warning disable RCS1169 // Make field read-only.
		[SerializeField]
		private InputActionReference controllerActionGrip = null!, controllerActionTrigger = null!;
#pragma warning restore RCS1169 // Make field read-only.

		private Animator? animator;
		private Animator Animator
		{
#pragma warning disable RCS1084 // Use coalesce expression instead of conditional expression.
			get => animator = animator != null ? animator : GetComponent<Animator>();
#pragma warning restore RCS1084 // Use coalesce expression instead of conditional expression.
			set => animator = value;
		}

		private void Awake()
		{
			controllerActionGrip.action.performed += GripPress;
			controllerActionTrigger.action.performed += TriggerPress;

			Animator = GetComponent<Animator>();
		}

		private void TriggerPress(InputAction.CallbackContext obj) => Animator.SetFloat("Trigger", obj.ReadValue<float>());
		private void GripPress(InputAction.CallbackContext obj) => Animator.SetFloat("Grip", obj.ReadValue<float>());
	}
}
