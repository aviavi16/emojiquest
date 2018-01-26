﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StageDef  {

    [System.Serializable]
    public class Substage
    {
        public Clickable[] toActivate;

        private int step;

        public void Reset()
        {
            for (int i = 0; i < step; ++i)
            {
                toActivate[i].UndoAction();
            }
            step = 0;
        }

        internal bool NotifyClicked(Clickable c)
        {
            if (step < toActivate.Length && c==toActivate[step]) {
            
                ++step;
                c.DoAction();
                return true;
            }
            return false;

        }

        public bool IsDone()
        {
            return step >= toActivate.Length;
        }
    }

    public Substage[] substages;


   private int substage = 0;

    public void Reset()
    {
        if (substage<substages.Length)
           substages[substage].Reset();
    }

    internal bool IsDone()
    {
        return substage >= substages.Length;
    }

    public void NotifyClicked(Clickable c)
    {
        if (substage >= substages.Length)
            return ;

        Substage sstage = substages[substage];

        if (sstage.NotifyClicked(c))
        {
            if (sstage.IsDone())
            {
             
                ++substage;
            }
        }
        else
            Reset();

    }

}
