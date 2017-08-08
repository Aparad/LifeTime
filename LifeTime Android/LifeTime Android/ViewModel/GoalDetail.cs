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

namespace LifeTime_Android.ViewModel
{
    class GoalDetail
    {
        public void PopulateGoalActivities(Model.DailyActivity DailyActivity, Intent intent, Context context, ViewGroup Layout)
        {
            Button DailyActivityButton = new Button(context);
            DailyActivityButton.Text = DailyActivity.ActivityName;
            if (DailyActivity.ActivityStatus)
                DailyActivityButton.SetBackgroundColor(Android.Graphics.Color.Green);
            else if (!DailyActivity.ActivityStatus)
                DailyActivityButton.SetBackgroundColor(Android.Graphics.Color.Red);
            DailyActivityButton.Click += (sender, e) =>
            {
                intent.PutExtra("DailyActivityPassed", JsonConvert.SerializeObject(DailyActivity));
                context.StartActivity(intent);
            };
            Layout.AddView(DailyActivityButton);
        }
    }
}