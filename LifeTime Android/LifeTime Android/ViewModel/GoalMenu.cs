using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using LifeTime_Android.Model;

namespace LifeTime_Android.ViewModel
{
    class GoalMenu
    {
        public List<Goal> MyGoals = new List<Goal>();

        public void AddGoal(Goal goal)
        {
            MyGoals.Add(goal);
        }
        public void DeleteGoal(Goal goal)
        {
            MyGoals.Remove(goal);
        }
        //public void SelectGoal()
    }
}