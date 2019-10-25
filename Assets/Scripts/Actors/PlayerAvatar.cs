using UnityEngine;

namespace Actors
{
	/// <summary>
	/// Player binding that possesses a tornado
	/// </summary>
	public class PlayerAvatar : MonoBehaviour
	{
		[SerializeField] private Tornado tornado;
		[SerializeField] private Color playerColor;

		private void Start()
		{
			this.tornado.SetCursorColor(this.playerColor);
		}

		private void Update()
		{
			this.tornado.SetRageActive(Input.GetButton("Fire1"));
			this.tornado.SetMoveDirection(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
		}
	}
}