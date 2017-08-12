using Android.App;
using Android.Widget;
using Android.OS;
using LifeTime_Android.ViewModel;
using Android.Views;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Android.Content;

namespace LifeTime_Android
{
    [Activity(Label = "LifeTime", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        GoalMenu goalMenu = new GoalMenu();
        Context _context;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            _context = this;
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            InsertTestGoals();
            SetBindings();
        }

        public void InsertTestGoals()
        {
            //Adding sample goals and  data
            ViewGroup goalsLayout = (LinearLayout)FindViewById(Resource.Id.goalsLayout);
            var GoalActivityIntent = new Android.Content.Intent(this, typeof(GoalActivity));

            Model.Goal goal1 = new Model.Goal("Practice guitar");
            Model.DailyActivity act1 = new Model.DailyActivity("Arpeggios", "Shred some arpeggios!", 1, true);
            goal1.GoalActivities.Add(act1);
            goalMenu.AddGoal(goal1, GoalActivityIntent, _context, goalsLayout);

            Model.Goal goal2 = new Model.Goal("Study for exams next week");
            Model.DailyActivity act2 = new Model.DailyActivity("Explosions", "BOOMbombombombombom", 1, true);
            Model.DailyActivity act3 = new Model.DailyActivity("Chemistry n' shit", "DO some magic.", 1, false);
            goal2.GoalActivities.Add(act2);
            goal2.GoalActivities.Add(act3);
            goalMenu.AddGoal(goal2, GoalActivityIntent, _context, goalsLayout);
            goalMenu.AddGoal(new Model.Goal("Relax", status: false), GoalActivityIntent, _context, goalsLayout);
        }

        public void SetBindings()
        {
            Button addButton = (Button)FindViewById(Resource.Id.addButtonOutside);
            addButton.Click += delegate
            {
                var addGoalIntent = new Android.Content.Intent(this, typeof(GoalAddActivity));
                addGoalIntent.PutExtra("GoalsList", JsonConvert.SerializeObject(goalMenu.MyGoals));
                StartActivityForResult(addGoalIntent, 1);
            };
        } 

        protected override void OnActivityResult(int requestCode, Result resultCode, Android.Content.Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (resultCode == Result.Ok)
            {
                ViewGroup goalsLayout = (LinearLayout)FindViewById(Resource.Id.goalsLayout);
                var GoalActivityIntent = new Android.Content.Intent(this, typeof(GoalActivity));
                var GoalPassedBack = new Model.Goal();
                GoalPassedBack.GoalName = data.GetStringExtra("goalName");
                GoalPassedBack.GoalDescription = data.GetStringExtra("goalDescription");
                goalMenu.AddGoal(GoalPassedBack, GoalActivityIntent, _context, goalsLayout);
            }
        }
    }
}
