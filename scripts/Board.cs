using Godot;
using System;
using System.Net.NetworkInformation;

public partial class Board : Node2D
{
	[Signal] public delegate void TileCreatedEventHandler(Tile tile);
	[Signal] public delegate void PieceCreatedEventHandler(Piece piece);
	[Export] protected Node tilesNode, piecesNode;
	protected Tile[,] tiles = new Tile[8, 8];
	protected PackedScene tilePackedScene, piecePackedScene;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		tilePackedScene = GD.Load<PackedScene>("res://scenes/tile.tscn");
		piecePackedScene = GD.Load<PackedScene>("res://scenes/piece.tscn");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public void LoadBoard()
	{
		for(int position = 1; position < 64; position += 2)
		{
			LoadPosition(position);
		}
	}
	private void LoadPosition(int position)
	{
		Tile tile = (Tile) tilePackedScene.Instantiate();
		tilesNode.AddChild(tile);
		int tileRow = position/8;
		int tileColumn = position%8 - tileRow%2;
		tile.Position = TilePositionToBoardPosition(tileColumn, tileRow);
		tile.Row = tileRow;
		tile.Column = tileColumn;
		tiles[tileColumn, tileRow] = tile;

		Piece piece = (Piece) piecePackedScene.Instantiate();
		if((tileRow <= 2) || (tileRow >= 5))
		{
			piecesNode.AddChild(piece);
			piece.Position = TilePositionToBoardPosition(tileColumn, tileRow);
			tile.Piece = piece;
			tile.UpdateArea2D();
			piece.TileRow = tileRow;
			piece.TileColumn = tileColumn;

			if(tileRow <= 2)
			{
				piece.Side = Piece.PlayerSide.BLACK;
				piece.SetBlackTextureOn();
			}
			else
			{
				piece.Side = Piece.PlayerSide.RED;
				piece.SetRedTextureOn();
			}
		}
		EmitSignal(SignalName.TileCreated, tile);
		EmitSignal(SignalName.PieceCreated, piece);
	}
	public Vector2 TilePositionToBoardPosition(int tileColumn, int tileRow)
	{
		return Position + new Vector2(50.0f * (tileColumn - 3.5f), 50.0f * (tileRow - 3.5f));
	}

	public Tile GetTile(int column, int row)
	{
		return tiles[column, row];
	}
}
