using Sandbox;
using System;
using System.Numerics;

public sealed class CameraController : Component
{
	[Property] public int Radius;
	[Property] public float PositionRotationDelta = 0.05f;
	[Property] public float HorizontalCameraRotationDelta = 0.05f;
	[Property] public float VeritcalCameraRotationDelta = 0.05f;


	private CameraComponent _camera;
	private float _angleOfRoatation = 0.0f;
	private float _currentPitch = 0.0f;
	private float _currentYaw = 0.0f;
	private float _currentRoll = 0.0f;


	protected override void OnStart()
	{
		_camera = Scene.Camera;
		_camera.LocalRotation = Rotation.From( _currentPitch, _currentYaw, _currentRoll );
	}

	protected override void OnUpdate()
	{
		if ( Input.Down( "moveCamLeft" ) )
		{
			_angleOfRoatation -= PositionRotationDelta;
		}
		else if ( Input.Down( "moveCamRight" ) )
		{
			_angleOfRoatation += PositionRotationDelta;
		}

		float fixedXPosition = GameObject.WorldPosition.x;
		float fixedYPosition = GameObject.WorldPosition.y;
		float fixedZPosition = GameObject.WorldPosition.z;

		float cameraOffsetX = Radius * MathF.Cos( _angleOfRoatation );
		float cameraOffsetY = Radius * MathF.Sin( _angleOfRoatation );
		float cameraOffsetZ = 50.0f;

		Vector3 updatedCameraPosition  = new Vector3( fixedXPosition + cameraOffsetX, fixedYPosition + cameraOffsetY, fixedZPosition + cameraOffsetZ );
		_camera.WorldPosition = updatedCameraPosition;

		Rotation lookAtRotation = Rotation.LookAt( _camera.WorldPosition, GameObject.WorldPosition );

		float newPitch = _currentPitch;
		float newYaw = _currentYaw;

		newYaw = lookAtRotation.Angles().yaw + 180;

		if (Input.Down("panCamUp")) {
			newPitch += 1;
			_currentPitch = newPitch;
		}
		else if ( Input.Down( "panCamDown" ) )
		{
			newPitch -= 1;
			_currentPitch = newPitch;
		}

		_currentPitch = Math.Clamp( _currentPitch, -45, 45 );
		Rotation newRotation = Rotation.From( newPitch, newYaw, _currentRoll );
		_camera.WorldRotation = newRotation;
	}
}
