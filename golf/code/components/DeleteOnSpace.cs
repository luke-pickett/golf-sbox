using Sandbox;

public sealed class DeleteOnSpace : Component
{
	protected override void OnUpdate()
	{
		if (Input.Down( "jump" ) )
		{
			GameObject.Destroy();
		}
	}
}
