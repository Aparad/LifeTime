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
using Newtonsoft.Json;

namespace LifeTime_Android
{
    [Activity(Label = "GoalAddActivity")]
    public class GoalAddActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.GoalAddLayout);

            Button addButton = (Button)FindViewById(Resource.Id.AddButtonInside);
            EditText goalNameField = (EditText)FindViewById(Resource.Id.GoalNameEditText);
            EditText goalDescriptionField = (EditText)FindViewById(Resource.Id.GoalDescriptionEditText);
            addButton.Click += delegate
            {
                try //trying to create a new goal, catching for correct goal field formats
                {
                    Model.Goal goal = new Model.Goal();
                    goal.GoalName = goalNameField.Text;
                    goal.GoalDescription = goalDescriptionField.Text;
                    Intent goalData = new Intent();
                    goalData.PutExtra("GoalPassed", JsonConvert.SerializeObject(goal));
                    SetResult(Result.Ok, goalData);
                    Finish();
                }
                catch (System.FormatException fE)
                {
                    Toast.MakeText(this, fE.Message, ToastLength.Short).Show();
                }
            };

        }
    }
}