﻿using Android.App;
using Android.OS;
using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;
using LifeTime_Android.Model;
using Newtonsoft.Json;
using System;

namespace LifeTime_Android.ViewModel
{
    class GoalMenu
    {
        public List<Goal> MyGoals = new List<Goal>();

        public void AddGoalButtonClick(Object sender, EventArgs e, Context context)
        {
            context.StartActivity(new Android.Content.Intent(context, typeof(GoalAddActivity)));
        }
        public void AddGoal(Goal goal)
        {
            MyGoals.Add(goal);
        }
        public void DeleteGoal(Goal goal)
        {
            MyGoals.Remove(goal);
        }
        public void PopulateGoalMenuList(Goal goal, Intent intent, Context context, ViewGroup Layout) //Add goal button to GoalMenu
        {
            Button goalButton = new Button(context);
            goalButton.Text = goal.GoalName;
            if (goal.GoalStatus)
                goalButton.SetBackgroundColor(Android.Graphics.Color.Green);
            else if(goal.GoalProgress != 0)
                goalButton.SetBackgroundColor(Android.Graphics.Color.Yellow);
            else if(!goal.GoalStatus)
                goalButton.SetBackgroundColor(Android.Graphics.Color.Red);
            goalButton.Click += (sender, e) =>
            {
                intent.PutExtra("GoalPassed", JsonConvert.SerializeObject(goal));
                context.StartActivity(intent);
            };
            Layout.AddView(goalButton);
        }
    }
}