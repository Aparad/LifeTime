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
                Intent goalData = new Intent();
                goalData.PutExtra("goalName", goalNameField.Text);
                goalData.PutExtra("goalDescription", goalDescriptionField.Text);
                SetResult(Result.Ok, goalData);
                Finish();
            };
        }
    }
}