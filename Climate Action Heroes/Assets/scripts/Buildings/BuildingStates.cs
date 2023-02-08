
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BuildingStates
{
    public enum States
    {
        windmill_plot,
        windmill_lvl1,
        windmill_lvl2,
        windmill_lvl3,
        hydrogenerator_plot,
        hydrogenerator_lvl1,
        hydrogenerator_lvl2,
        hydrogenerator_lvl3,
        solar_plot,
        solar_lvl1,
        solar_lvl2,
        solar_lvl3,
    }

    public States states;

    public BuildingStates()
    {

    }

    public BuildingStates(States states)
    {
        this.states = states;
    }

    public String GetNextUpgradeName()
    {
        switch(states)
        {
            default:
            case States.windmill_plot: return "Windmill Lvl 1";
            case States.windmill_lvl1: return "Windmill Lvl 2";
            case States.windmill_lvl2: return "Windmill Lvl 3";
            case States.windmill_lvl3: return null;

            case States.hydrogenerator_plot: return "Dam Lvl 1";
            case States.hydrogenerator_lvl1: return "Dam Lvl 2";
            case States.hydrogenerator_lvl2: return "Dam Lvl 3";
            case States.hydrogenerator_lvl3: return null;

            case States.solar_plot: return "Solar Panel Lvl 1";
            case States.solar_lvl1: return "Solar Panel Lvl 2";
            case States.solar_lvl2: return "Solar Panel Lvl 3";
            case States.solar_lvl3: return null;
        }
    }

    public static String GetNextUpgradeName(States states)
    {
        switch (states)
        {
            default:
            case States.windmill_plot: return "Windmill Lvl 1";
            case States.windmill_lvl1: return "Windmill Lvl 2";
            case States.windmill_lvl2: return "Windmill Lvl 3";
            case States.windmill_lvl3: return null;

            case States.hydrogenerator_plot: return "Dam Lvl 1";
            case States.hydrogenerator_lvl1: return "Dam Lvl 2";
            case States.hydrogenerator_lvl2: return "Dam Lvl 3";
            case States.hydrogenerator_lvl3: return null;

            case States.solar_plot: return "Solar Panel Lvl 1";
            case States.solar_lvl1: return "Solar Panel Lvl 2";
            case States.solar_lvl2: return "Solar Panel Lvl 3";
            case States.solar_lvl3: return null;
        }
    }

    public States GetState()
    {
        return states;
    }
}
