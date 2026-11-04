using UnityEngine;

public interface IState
{
    void Enter();
    void UpdateState();
    void Exit();
}

public class SoldierFSM : MonoBehaviour
{
    private IState currentState;

    public void ChangeState(IState newState)
    {
        if (currentState != null) currentState.Exit();
        currentState = newState;
        if (currentState != null) currentState.Enter();
    }

    void Update()
    {
        if (currentState != null)
        {
            currentState.UpdateState();
        }
    }
}

public class PatrolState : IState
{
    private SoldierFSM soldier;
    private Transform[] waypoints;
    private int currentIndex;

    public PatrolState(SoldierFSM soldier, Transform[] waypoints)
    {
        this.soldier = soldier;
        this.waypoints = waypoints;
    }

    public void Enter() 
    { 
        Debug.Log("Entered Patrol State");
    }

    public void UpdateState()
    {
        // Patrol logic here...
    }
    public void Exit() 
    {

    }
