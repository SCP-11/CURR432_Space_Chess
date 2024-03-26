using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CannonPiece : ChessPiece
{

	public GameObject beamParticleEffect;

	private GameObject currentBeam;
	private float timer = -1f;
	private LineRenderer lr;
	public CannonPiece(){
		this.Type = "cannon";
		this.LockRange = 2;
	}


    public override ArrayList GetPossibleMoves(ChessPiece[,] pieces, int startX, int startY){
		ArrayList possibleMoves = new ArrayList();

		/**
		for (int i = startX + 1; i < 10; i++){
			if(pieces[i, startY] != null){
				break;
			}
			possibleMoves.Add(new Vector2(i, startY));
		}
		for (int i = startX - 1; i > -1; i--){
			if(pieces[i, startY] != null){
				break;
			}
			possibleMoves.Add(new Vector2(i, startY));
		}
		for (int i = startY + 1; i < 9; i++){
			if(pieces[startX, i] != null){
				break;
			}
			possibleMoves.Add(new Vector2(startX, i));
		}
		for (int i = startY - 1; i > -1; i--){
			if(pieces[startX, i] != null){
				break;
			}
			possibleMoves.Add(new Vector2(startX, i));
		}
		int start;
		int end;
		//X attack
		for (int i = 0; i < 10; i++){
			if(pieces[i, startY] != null){
				int countBetween = 0;
				if(pieces[i, startY].GetRed() == GetRed()){}
				else{
					if(startX<i){start=startX; end=i;}else{start=i; end=startX;}
					for(int j=start+1; j<end; j++){
						if(pieces[j, startY] != null)countBetween++;
					}
					if(countBetween == 1){
						possibleMoves.Add(new Vector2(i, startY));
					}
				}
			}
		}
		//Y attack
		for (int i = 0; i < 9; i++){
			if(pieces[startX, i] != null){
				int countBetween = 0;
				if(pieces[startX, i].GetRed() ==GetRed()){}
				else{
					if(startY<i){start=startY; end=i;}else{start=i; end=startY;}
					for(int j=start+1; j<end; j++){
						if(pieces[startX, j] != null)countBetween++;
					}
					if(countBetween == 1){
						possibleMoves.Add(new Vector2(startX, i));
					}
				}
			}
		}
		**/
		checkAndAddMove(pieces,possibleMoves, new Vector2(startX+1, startY));
		checkAndAddMove(pieces,possibleMoves, new Vector2(startX-1, startY));
		checkAndAddMove(pieces,possibleMoves, new Vector2(startX, startY+1));
		checkAndAddMove(pieces,possibleMoves, new Vector2(startX, startY-1));

		return possibleMoves;
    }
	public override ArrayList GetPossibleAttacks(ChessPiece[,] pieces, int[] frontLines, int startX, int startY){
			ArrayList possibleAttacks = new ArrayList();

			int start;
			int end;
			//X attack
			// String debug = $"Check Attack Position: ";	///////////	DEBUG
			for (int i = 0; i < 10; i++){
				if(pieces[i, startY] != null){
					int countBetween = 0;
					if(pieces[i, startY].GetRed() == GetRed()){}
					else{
						if(startX<i){start=startX; end=i;}else{start=i; end=startX;}
						for(int j=start+1; j<end; j++){
							if(pieces[j, startY] != null)countBetween++;
						}
						if(countBetween == 1){
							// debug += $"{new Vector2(i, startY)}, ";	/////////	DEBUG
							// Debug.Log()
							checkAndAddAttack(pieces, possibleAttacks, new Vector2(i, startY));
						}
					}
				}
			}
			// Debug.Log(debug);	/////////////////////////////	DEBUG

			//Y attack
			for (int i = 0; i < 9; i++){
				if(pieces[startX, i] != null){
					int countBetween = 0;
					if(pieces[startX, i].GetRed() == GetRed()){}
					else{
						if(startY<i){start=startY; end=i;}else{start=i; end=startY;}
						for(int j=start+1; j<end; j++){
							if(pieces[startX, j] != null)countBetween++;
						}
						if(countBetween == 1){
							checkAndAddAttack(pieces, possibleAttacks, new Vector2(startX, i));
						}
					}
				}
			}
			RemoveOutRangeAttack(possibleAttacks, frontLines);
			return possibleAttacks;
		}
		
	public override void AttackAnimation(Vector3 startPosition, Vector2 start, Vector2 target){
		currentBeam = Instantiate(beamParticleEffect, startPosition, Quaternion.identity);
		
		// Set the end point of the beam
        LineRenderer lineRenderer = currentBeam.GetComponentInChildren<LineRenderer>();
        if (lineRenderer != null)
        {
            lineRenderer.SetPosition(0, startPosition);
            lineRenderer.SetPosition(1, startPosition + new Vector3((target.y - start.y) * 20f, (target.x - start.x) * 20f, 0.0f));
        }

		timer = 0f;
	}

	public void Update(){
		if(currentBeam != null){
			LineRenderer lineRenderer = currentBeam.GetComponentInChildren<LineRenderer>();

			if(timer >= 0 && timer < 0.5){
				timer += Time.deltaTime;
			}else{
				timer = -1f;
				lineRenderer.enabled = false;
			}
		}
	}
}
