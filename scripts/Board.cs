using Godot;
using System;

public partial class Board : Node2D
{
	[Export] protected Node tilesNode, piecesNode;
	protected Tile[,] tiles = new Tile[8, 8];
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		PackedScene tilePackedScene = GD.Load<PackedScene>("res://scenes/tile.tscn");
		for(int position = 1; position < 64; position += 2)
		{
			Tile tile = (Tile) tilePackedScene.Instantiate();
			AddChild(tile);
			int tileRow = position/8;
			int tileColumn = position%8 - tileRow%2;
			tile.Position = TilePositionToBoardPosition(tileColumn, tileRow);
			tiles[tileColumn, tileRow] = tile;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public static Vector2 TilePositionToBoardPosition(int tileColumn, int tileRow)
	{
		return new Vector2(50.0f * (tileColumn - 3.5f), 50.0f * (tileRow - 3.5f));
	}
}
