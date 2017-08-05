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
using Newtonsoft.Json;

namespace LifeTime_Android
{
    [Activity(Label = "GoalActivity")]
    public class GoalActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.GoalLayout);

            TextView goalName = (TextView)FindViewById(Resource.Id.GoalName); //Find TextField with goal name
            var PassedGoal = Intent.GetStringExtra("GoalPassed");
            Goal PassedGoalDeserialized = JsonConvert.DeserializeAnonymousType(PassedGoal, new Goal()); //Deserialization of incoming goal

            //Assigning goal values to activity fields

            goalName.Text = PassedGoalDeserialized.GoalName;
        }
    }
}