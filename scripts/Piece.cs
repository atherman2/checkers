using Godot;
using System;

public partial class Piece : Node2D
{
	[Signal] public delegate void SelectedEventHandler(Piece piece); 
	public enum PlayerSide
	{
		BLACK,
		RED
	}
	protected PlayerSide side;
	public PlayerSide Side
	{
		get => side;
		set => side = value;
	}
	[Export] protected int tileRow, tileColumn;
	public int TileRow
	{
		get => tileRow;
		set => tileRow = value;
	}
	public int TileColumn
	{
		get => tileColumn;
		set => tileColumn = value;
	}
	[Export] protected Area2D area2D;
	[Export] protected Sprite2D sprite2D;
	[Export] protected Texture2D SpriteTexture
	{
		get => sprite2D.Texture;
		set => sprite2D.Texture = value;
	}
	[Export] protected Texture2D blackTexture, redTexture;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		area2D.InputEvent += OnArea2DInputEvent;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void OnArea2DInputEvent(Node viewPort, InputEvent inputEvent, long shapeIndex)
	{
		if(inputEvent.IsActionPressed("select"))
		{
			GD.Print($"Selected {Name}");
		}
		EmitSignal(SignalName.Selected, this);
	}
	public void SetBlackTextureOn()
	{
		SpriteTexture = blackTexture;
	}

	public void SetRedTextureOn()
	{
		SpriteTexture = redTexture;
	}
}
