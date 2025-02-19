using Godot;
using System;

public partial class Game : Node2D
{
	[Export] protected Board board;
	protected Piece selectedPiece;
	protected Piece.PlayerSide playerTurn = Piece.PlayerSide.RED;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		board.TileCreated += OnTileCreated;
		board.PieceCreated += OnPieceCreated;
		board.LoadBoard();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public void OnTileCreated(Tile tile)
	{
		tile.Selected += OnTileSelected;
	}
	public void OnPieceCreated(Piece piece)
	{
		piece.Selected += OnPieceSelected;
	}
	public void OnTileSelected(Tile tile)
	{
		if(selectedPiece != null)
		{
			int rowDirection;
			if(playerTurn == Piece.PlayerSide.BLACK)
				rowDirection = 1;
			else
				rowDirection = -1;
			if(tile.Piece == null)
			{
				if(CanSelectedPieceSimpleMoveToTile(tile, rowDirection))
				{
					MoveSelectedPieceToTile(tile);
					if(playerTurn == Piece.PlayerSide.RED)
						playerTurn = Piece.PlayerSide.BLACK;
					else
						playerTurn = Piece.PlayerSide.RED;
				}
				else if(CanSelectedJumpMoveToTile(tile, rowDirection))
				{
					Tile jumpedTile = board.GetTile
					(
						selectedPiece.TileColumn + (tile.Column - selectedPiece.TileColumn) / 2,
						selectedPiece.TileRow + (tile.Row - selectedPiece.TileRow) / 2
					);
					jumpedTile.Piece.QueueFree();
					jumpedTile.Piece = null;
					jumpedTile.UpdateArea2D();
					MoveSelectedPieceToTile(tile);
				}
			}
		}
	}
	public void OnPieceSelected(Piece piece)
	{
		if(playerTurn == piece.Side)
			selectedPiece = piece;
		else
			selectedPiece = null;
	}
	public void MoveSelectedPieceToTile(Tile tile)
	{
		tile.Piece = selectedPiece;
		tile.UpdateArea2D();

		Tile oldTile = board.GetTile(selectedPiece.TileColumn, selectedPiece.TileRow);
		oldTile.Piece = null;
		oldTile.UpdateArea2D();

		selectedPiece.TileColumn = tile.Column;
		selectedPiece.TileRow = tile.Row;
		selectedPiece.Position = board.TilePositionToBoardPosition(tile.Column, tile.Row);

		if((tile.Row == 0) || (tile.Row == 7))
			selectedPiece.IsKing = true;
	}
	public bool CanSelectedPieceSimpleMoveToTile(Tile tile, int rowDirection)
	{
		return
		(
			(tile.Row == selectedPiece.TileRow + rowDirection)
			&&
			(
				(tile.Column == selectedPiece.TileColumn + 1)
				|| (tile.Column == selectedPiece.TileColumn - 1)
			)
		)
		||
		(
			selectedPiece.IsKing
			&&
			(tile.Row == selectedPiece.TileRow - rowDirection)
			&&
			(
				(tile.Column == selectedPiece.TileColumn + 1)
				|| (tile.Column == selectedPiece.TileColumn - 1)
			)
		);
	}
	public bool CanSelectedJumpMoveToTile(Tile tile, int rowDirection)
	{
		return
		(Math.Abs(tile.Row - selectedPiece.TileRow) == 2)
		&& (Math.Abs(tile.Column - selectedPiece.TileColumn) == 2)	
		&&
		(
			(rowDirection == Math.Sign(tile.Row - selectedPiece.TileRow))
			||
			selectedPiece.IsKing
		)
		&&
		(
			board.GetTile
			(
				selectedPiece.TileColumn + ((tile.Column - selectedPiece.TileColumn) / 2),
				selectedPiece.TileRow + ((tile.Row - selectedPiece.TileRow) / 2)
			)
			.Piece != null
		);
	}
}
