using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.MLAgents;
using UnityEngine;
// using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
public class AI : Agent
{
    public Board board;
    public bool isRed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if(board.isRedTurn == isRed){
        //     this.RequestDecision();
        // }else{

        // }
    }

    private int TypeToInt(string type){
        switch (type){
            case "chariot":
                return 1;
            case "cannon":
                return 2;
            case "horse":
                return 3;
            case "elephant":
                return 4;
            case "advisor":
                return 5;
            case "general": 
                return 6;
            case "soldier":
                return 7;
            default:
                return 0;
        }
    }
    IEnumerator WaitHalfSecond()
    {
        Debug.Log("Waiting for half a second...");
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Done waiting for half a second");
    }

    
    public override void CollectObservations(VectorSensor sensor)
    {
        if(board.isRedTurn == isRed){
            ChessPiece[,] pieces = board.pieces;
            int[,] processedBoard = new int[10, 9];

            for (int i = 0; i < 10; i++){
                for (int j = 0; j < 9; j++){
                    if (pieces[i, j] != null){
                        int ind = TypeToInt(pieces[i, j].Type);
                        processedBoard[i, j] = pieces[i, j].GetRed()==isRed ? ind : -ind;
                    }else{
                        processedBoard[i, j] = 0;
                    }
                }
            }
            for (int i = 0; i < 10; i++){
                for (int j = 0; j < 9; j++){
                    sensor.AddObservation(processedBoard[i, j]);
                }
            }
            
            int[] redPiecesFrontLines = board.redPiecesFrontLines;
            int[] bluePiecesFrontLines = board.bluePiecesFrontLines;
            for (int i = 0; i < 9; i++){
                if(isRed){
                    sensor.AddObservation(redPiecesFrontLines[i]);
                    sensor.AddObservation(bluePiecesFrontLines[i]);
                }else{
                    sensor.AddObservation(bluePiecesFrontLines[i]);
                    sensor.AddObservation(redPiecesFrontLines[i]);
                }
            }

            int[,] debris = board.spaceDebrisCount;
            foreach (int i in debris){
                sensor.AddObservation(i);
            }
            // base.CollectObservations(sensor);
        }
    }

    public override void OnActionReceived(ActionBuffers actions)
    {

        if(board.isRedTurn == isRed){
            Debug.Log("OnActionReceived" + actions.DiscreteActions[0]
            + actions.DiscreteActions[1]
            + actions.DiscreteActions[2]
            + actions.DiscreteActions[3]
            );

            if(board.SelectPiece(actions.DiscreteActions[0], actions.DiscreteActions[1])){
                SetReward(1.0f);
            }else{
                SetReward(-1.0f);
            }

            
            // Wait half a second
            // WaitHalfSecond();
            if(board.SelectPiece(actions.DiscreteActions[2], actions.DiscreteActions[3])){
                SetReward(1.0f);
            }else{
                SetReward(-1.0f);
            }
            // take_action(actions.DiscreteActions[0]);
            // update_graphic();
        }
    }
}
