namespace Core.InputManagement.Buttons
{
	public interface IInputButton
	{
		float DeadZone { get; set; }
		float Value { get; }
		bool IsDown { get; }
		bool IsUp { get; }
		bool IsHeld { get; }
		bool IsInactive { get; }
		void UpdateState();
	}
}