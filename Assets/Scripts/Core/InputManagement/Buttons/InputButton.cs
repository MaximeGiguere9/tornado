using System;
using UnityEngine;

namespace Core.InputManagement.Buttons
{
	/// <summary>
	/// Encapsulates input for a single button or axis defined in Unity's InputManager
	/// </summary>
	public class InputButton : IInputButton
	{
        public string Key { get; }

        public InputButtonState State { get; private set; }
		public float Value { get; private set; }

		public bool IsDown => this.State == InputButtonState.Down;
		public bool IsUp => this.State == InputButtonState.Up;
		public bool IsHeld => this.State == InputButtonState.Held;
		public bool IsInactive => this.State == InputButtonState.Inactive;

		private float deadZone;

		public float DeadZone
		{
			get => this.deadZone;
			set => this.deadZone = Mathf.Clamp01(value);
		}

		public InputButton(string key)
		{
			try
			{
				Input.GetAxisRaw(key);
				Input.GetButton(key);
				this.Key = key;
			}
			catch (UnityException)
			{
				throw new ArgumentException($"{key} is not a valid axis or button.");
			}
		}

		public void UpdateState()
        {
	        bool isPressed = Input.GetButton(Key) || Math.Abs(Input.GetAxisRaw(Key)) > this.deadZone;

			if (isPressed)
            {
                if (State == InputButtonState.Down) State = InputButtonState.Held;
				else if (State != InputButtonState.Held) State = InputButtonState.Down;
            }
            else
            {
                if (State == InputButtonState.Down || State == InputButtonState.Held) State = InputButtonState.Up;
				else State = InputButtonState.Inactive;
            }

			this.Value = Math.Abs(Input.GetAxisRaw(Key)) > this.deadZone ? Input.GetAxisRaw(Key) : 0;
        }
	}
}
