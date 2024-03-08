using Godot;
using System;
using System.Diagnostics;


public partial class ControllerCamera : Camera2D
{
    public float CameraSpeed = 700.0f;
	
	public float ZoomStep = 0.03f;
	public float MinZoom = 1f;
	public float MaxZoom = 3;
	public float ZoomDefault = 1.5f;
    private bool isDragging = false;

    public override void _Input(InputEvent @event) // Traking camera if u press left click and Zoom
    {
        if (@event is InputEventMouseButton mouseButtonEvent)
        {
            if (mouseButtonEvent.ButtonIndex == MouseButton.Left)
            {
                if (mouseButtonEvent.Pressed)
                {
                    isDragging = true;
                }
                else if (mouseButtonEvent.IsReleased())
                {
                    isDragging = false;
                }
            }
			if (mouseButtonEvent.ButtonIndex == MouseButton.WheelDown)
			{
				if (ZoomDefault < MaxZoom)
				{
					Zoom = new Vector2(ZoomDefault, ZoomDefault);
					Debug.Print($"{Zoom}");
					ZoomDefault+= ZoomStep;
				}
			}
			else if (mouseButtonEvent.ButtonIndex == MouseButton.WheelUp)
			{
				if (ZoomDefault > MinZoom)
				{
					Zoom = new Vector2(ZoomDefault, ZoomDefault);
					Debug.Print($"{Zoom}");
					ZoomDefault-= ZoomStep;

				}
				
			}
        }
        else if (isDragging && @event is InputEventMouseMotion mouseMotionEvent)
        {
            Vector2 mouseDelta = mouseMotionEvent.Relative;

            Translate(new Vector2(-mouseDelta.X, -mouseDelta.Y) * CameraSpeed * (float)GetProcessDeltaTime());
        }
		
		
    }

	
}
