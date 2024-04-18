using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ChariotPiece : ChessPiece
{

	private float timer = -1f;
	private LineRenderer lr;
	public ChariotPiece(){
		this.Type = "chariot";
		this.LockRange = 1;
	}


    public override ArrayList GetPossibleMoves(ChessPiece[,] pieces, int startX, int startY){
		ArrayList possibleMoves = new ArrayList();

		for (int i = startX + 1; i < 10; i++){
			if(pieces[i, startY] != null){
				// if(GetRed() != pieces[i, startY].GetRed()){
				// 	possibleMoves.Add(new Vector2(i, startY));
				// }
				break;
			}
			possibleMoves.Add(new Vector2(i, startY));
		}
		for (int i = startX - 1; i > -1; i--){
			if(pieces[i, startY] != null){
				// if(GetRed() != pieces[i, startY].GetRed()){
				// 	possibleMoves.Add(new Vector2(i, startY));
				// }
				break;
			}
			possibleMoves.Add(new Vector2(i, startY));
		}
		for (int i = startY + 1; i < 9; i++){
			if(pieces[startX, i] != null){
				// if(GetRed() != pieces[startX, i].GetRed()){
				// 	possibleMoves.Add(new Vector2(startX, i));
				// }
				break;
			}
			possibleMoves.Add(new Vector2(startX, i));
		}
		for (int i = startY - 1; i > -1; i--){
			if(pieces[startX, i] != null){
				// if(GetRed() != pieces[startX, i].GetRed()){
				// 	possibleMoves.Add(new Vector2(startX, i));
				// }
				break;
			}
			possibleMoves.Add(new Vector2(startX, i));
		}
		return possibleMoves;
    }
	public override ArrayList GetPossibleAttacks(ChessPiece[,] pieces, int[] frontLines, int startX, int startY){
			ArrayList possibleAttacks = new ArrayList();

			for (int i = startX + 1; i < 10; i++){
				if(pieces[i, startY] != null){
					checkAndAddAttack(pieces, possibleAttacks, new Vector2(i, startY));
					break;
				}
			}
			for (int i = startX - 1; i > -1; i--){
				if(pieces[i, startY] != null){
					checkAndAddAttack(pieces, possibleAttacks, new Vector2(i, startY));
					break;
				}
			}
			for (int i = startY + 1; i < 9; i++){
				if(pieces[startX, i] != null){
					checkAndAddAttack(pieces, possibleAttacks, new Vector2(startX, i));
					break;
				}
			}
			for (int i = startY - 1; i > -1; i--){
				if(pieces[startX, i] != null){
					checkAndAddAttack(pieces, possibleAttacks, new Vector2(startX, i));
					break;
				}
			}
			RemoveOutRangeAttack(possibleAttacks, frontLines);
			return possibleAttacks;
		}

}
