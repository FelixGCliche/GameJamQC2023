using GameJamQC2023.Player;
using Godot;

namespace GameJamQC2023.Door
{
	public partial class Door : Area2D
	{
		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			if((bool)GetMeta("isStartDoor"))
				GetParent().GetNode<PlayerController>("Player").Position = Position;
		}
		private void OnCollisionEnter(Node2D body)
		{
		
			if (!(bool)GetMeta("isStartDoor"))
			{
				GetTree().ChangeSceneToFile((string)GetMeta("newScenePath"));
			}
            
		}

	}
}
