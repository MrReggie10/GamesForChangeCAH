using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public static StateManager stateManager { get; private set; }
    private int state;

    private void Awake()
    {
        stateManager = this;
        state = 0;
    }

    public int GetState()
    {
        return state;
    }

    public void SetState(int state)
    {
        this.state = state;
    }

    /*
     * state 0
     * no progress
     * 
     * state 1
     * phase 2 unlocked
     * 
     * state 2
     * phase 3 unlocked
     * 
     * state 3
     * during mayor quest
     * 
     * state 4
     * after mayor quest
     * 
     * state 5
     * after completion
     */
}
