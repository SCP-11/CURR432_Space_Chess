using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorsePiece : ChessPiece
{
	public HorsePiece(){
		this.Type = "horse";
		this.LockRange = 2;
	}
    public override ArrayList GetPossibleMoves(ChessPiece[,] pieces, int startX, int startY){
		ArrayList possibleMoves = new ArrayList();

		// checkAndAddMove(pieces,possibleMoves, new Vector2(startX+1, startY));
		// checkAndAddMove(pieces,possibleMoves, new Vector2(startX-1, startY));
		// checkAndAddMove(pieces,possibleMoves, new Vector2(startX, startY+1));
		// checkAndAddMove(pieces,possibleMoves, new Vector2(startX, startY-1));

		if(isInBounds(startX+1, startY)){
			if(pieces[startX+1, startY]==null){
				checkAndAddMove(pieces, possibleMoves, new Vector2(startX+2, startY-1));
				checkAndAddMove(pieces, possibleMoves, new Vector2(startX+2, startY+1));
			}
		}
		if(isInBounds(startX-1, startY)){
			if(pieces[startX-1, startY]==null){
				checkAndAddMove(pieces, possibleMoves, new Vector2(startX-2, startY-1));
				checkAndAddMove(pieces, possibleMoves, new Vector2(startX-2, startY+1));
			}
		}
		if(isInBounds(startX, startY-1)){
			if(pieces[startX, startY-1]==null){
				checkAndAddMove(pieces, possibleMoves, new Vector2(startX+1, startY-2));
				checkAndAddMove(pieces, possibleMoves, new Vector2(startX-1, startY-2));
			}
		}
		if(isInBounds(startX, startY+1)){
			if(pieces[startX, startY+1]==null){
				checkAndAddMove(pieces, possibleMoves, new Vector2(startX+1, startY+2));
				checkAndAddMove(pieces, possibleMoves, new Vector2(startX-1, startY+2));
			}
		}


		return possibleMoves;
    }
	
	public override ArrayList GetPossibleAttacks(ChessPiece[,] pieces, int[] frontLines, int startX, int startY){
		ArrayList possibleAttacks = new ArrayList();
		
		if(isInBounds(startX+1, startY)){
			if(pieces[startX+1, startY]==null){
				checkAndAddMove(pieces, possibleAttacks, new Vector2(startX+2, startY-1));
				checkAndAddMove(pieces, possibleAttacks, new Vector2(startX+2, startY+1));
			}
		}
		if(isInBounds(startX-1, startY)){
			if(pieces[startX-1, startY]==null){
				checkAndAddMove(pieces, possibleAttacks, new Vector2(startX-2, startY-1));
				checkAndAddMove(pieces, possibleAttacks, new Vector2(startX-2, startY+1));
			}
		}
		if(isInBounds(startX, startY-1)){
			if(pieces[startX, startY-1]==null){
				checkAndAddMove(pieces, possibleAttacks, new Vector2(startX+1, startY-2));
				checkAndAddMove(pieces, possibleAttacks, new Vector2(startX-1, startY-2));
			}
		}
		if(isInBounds(startX, startY+1)){
			if(pieces[startX, startY+1]==null){
				checkAndAddMove(pieces, possibleAttacks, new Vector2(startX+1, startY+2));
				checkAndAddMove(pieces, possibleAttacks, new Vector2(startX-1, startY+2));
			}
		}

		RemoveOutRangeAttack(possibleAttacks, frontLines);
		return possibleAttacks;
	}
}
