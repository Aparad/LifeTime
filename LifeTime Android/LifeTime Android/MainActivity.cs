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
        GoalMenu goalMenu;
        Context _context;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            _context = this;
            goalMenu = new GoalMenu(_context);

            SetContentView(Resource.Layout.Main);
            InsertTestGoals();
            SetBindings();
        }

        //Adding sample goals and data
        public void InsertTestGoals()
        {
            ViewGroup goalsLayout = (LinearLayout)FindViewById(Resource.Id.goalsLayout);
            var GoalActivityIntent = new Android.Content.Intent(this, typeof(GoalActivity));

            Model.Goal goal1 = new Model.Goal("Practice guitar");
            Model.DailyActivity act1 = new Model.DailyActivity("Arpeggios", "Shred some arpeggios!", 1, true);
            goal1.GoalActivities.Add(act1);
            goalMenu.AddGoal(goal1, GoalActivityIntent, this, goalsLayout);

            Model.Goal goal2 = new Model.Goal("Study for exams next week");
            Model.DailyActivity act2 = new Model.DailyActivity("Explosions", "BOOMbombombombombom", 1, true);
            Model.DailyActivity act3 = new Model.DailyActivity("Chemistry n' shit", "DO some magic.", 1, false);
            goal2.GoalActivities.Add(act2);
            goal2.GoalActivities.Add(act3);
            goalMenu.AddGoal(goal2, GoalActivityIntent, this, goalsLayout);
            goalMenu.AddGoal(new Model.Goal("Relax", status: false), GoalActivityIntent, this, goalsLayout);
        }

        //Add behaviour to main screen buttons etc.
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

        //Called back from GoalAddActivity's Add button to add newly created goal to the list (goalsLayout)
        protected override void OnActivityResult(int requestCode, Result resultCode, Android.Content.Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (resultCode == Result.Ok)
            {
                ViewGroup goalsLayout = (LinearLayout)FindViewById(Resource.Id.goalsLayout);
                var GoalActivityIntent = new Android.Content.Intent(this, typeof(GoalActivity));
                var PassedGoal = data.GetStringExtra("GoalPassed");
                Model.Goal GoalPassedBack = JsonConvert.DeserializeAnonymousType(PassedGoal, new Model.Goal()); ;
                GoalPassedBack._context = _context;
                goalMenu.AddGoal(GoalPassedBack, GoalActivityIntent, _context, goalsLayout);
            }
        }
    }
}
