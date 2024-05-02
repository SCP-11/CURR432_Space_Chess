using System;
using System.Collections;
using Unity.Barracuda;
using UnityEngine;

public class Soldier : ChessPiece {

    public Soldier(){
        this.Type = "soldier";
        this.LockRange = 1;
        this.MoveAttack = true;
    }

    void Update(){
        UpdateShield();
    }

	public override ArrayList GetPossibleMoves(ChessPiece[,] pieces, int startX, int startY){
        bool crossedRiver = false;
        ArrayList possibleMoves = new ArrayList();
        if(GetRed()){
            checkAndAddMove(pieces, possibleMoves, new Vector2(startX-1, startY));
            if(startX<=4){crossedRiver=true;}
        }else{
            checkAndAddMove(pieces, possibleMoves, new Vector2(startX+1, startY));
            if(startX>=5){crossedRiver=true;}
        }
        if(crossedRiver){
            checkAndAddMove(pieces,possibleMoves, new Vector2(startX, startY-1));
            checkAndAddMove(pieces,possibleMoves, new Vector2(startX, startY+1));
        }else{
            if(GetRed()){
                checkAndAddMove(pieces, possibleMoves, new Vector2(startX+1, startY));
            }else{
                checkAndAddMove(pieces, possibleMoves, new Vector2(startX-1, startY));
            }
        }
        return possibleMoves;
	}
	public override ArrayList GetPossibleAttacks(ChessPiece[,] pieces, int[] frontLines, int startX, int startY){
		bool crossedRiver = false;
        ArrayList possibleAttacks = new ArrayList();
        if(GetRed()){
            checkAndAddAttack(pieces, possibleAttacks, new Vector2(startX-1, startY));
            if(startX<=4){crossedRiver=true;}
        }else{
            checkAndAddAttack(pieces, possibleAttacks, new Vector2(startX+1, startY));
            if(startX>=5){crossedRiver=true;}
        }
        if(crossedRiver){
            checkAndAddAttack(pieces,possibleAttacks, new Vector2(startX, startY-1));
            checkAndAddAttack(pieces,possibleAttacks, new Vector2(startX, startY+1));
        }
        return possibleAttacks;
	}


	public override void CheckSpecial(Board board, ChessPiece[,] pieces){
        int selfX = (int) boardPosition.x;
        int selfY = (int) boardPosition.y;
		ChessPiece back1 = pieces[selfX+(red? 1: -1), selfY];
        ChessPiece back2 = pieces[selfX+(red? 2: -2), selfY];

        String d = $"";
        if(back1!=null && back1.GetRed()==GetRed()){
            if(back1.Type == "cannon"){
                d = $"Soldier received sheild from cannon!";
                hasShield = true;
                shield.SetActive(true);
            }
        }
        if(back2!=null && back2.GetRed()==GetRed()){
            if(back2.Type == "cannon"){
                d = $"Soldier received sheild from cannon!";
                hasShield = true;
                shield.SetActive(true);
            }
        }
	}

    public override String GetInfo()
    {
        String info = "This is Fighter Ship.\n";
        info += "It can move 1 step forward or backward.\n";
        info += "Can move left and right but no longer backward after crossing the middle.\n";
        info += "Will receive shield from Cannon behind.\n";
        return info;
    }

}
