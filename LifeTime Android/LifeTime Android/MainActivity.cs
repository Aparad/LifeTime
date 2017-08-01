using Android.App;
using Android.Widget;
using Android.OS;
using LifeTime_Android.ViewModel;
using Android.Views;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace LifeTime_Android
{
    [Activity(Label = "LifeTime_Android", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            GoalMenu goalMenu = new GoalMenu();
            goalMenu.AddGoal(new Model.Goal("Practice guitar"));
            goalMenu.AddGoal(new Model.Goal("Study for exams next week"));
            goalMenu.AddGoal(new Model.Goal("Relax", status : true));

            ViewGroup goalsLayout = (LinearLayout)FindViewById(Resource.Id.goalsLayout);
            foreach (var goal in goalMenu.MyGoals)
            {
                Button goalButton = new Button(this);
                goalButton.Text = goal.GoalName;
                goalsLayout.AddView(goalButton);
                goalButton.Click += (sender, e) =>
                 {
                     var intent = new Android.Content.Intent(this, typeof(GoalActivity));
                     intent.PutExtra("GoalPassed", JsonConvert.SerializeObject(goal));
                     StartActivity(intent);
                 };
            }
        }
    }
}

