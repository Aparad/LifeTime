using Android.App;
using Android.Widget;
using Android.OS;
using LifeTime_Android.ViewModel;
using Android.Views;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace LifeTime_Android
{
    [Activity(Label = "LifeTime", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            //Example data
            GoalMenu goalMenu = new GoalMenu();
            Model.Goal goal1 = new Model.Goal("Practice guitar");
            Model.DailyActivity act1 = new Model.DailyActivity("Arpeggios", "Shred some arpeggios!", 1, true);
            goal1.GoalActivities.Add(act1);
            GoalMenu.AddGoal(goal1);

            Model.Goal goal2 = new Model.Goal("Study for exams next week");
            Model.DailyActivity act2 = new Model.DailyActivity("Explosions", "BOOMbombombombombom", 1, true);
            Model.DailyActivity act3 = new Model.DailyActivity("Chemistry n' shit", "DO some magic.", 1, false);
            goal2.GoalActivities.Add(act2);
            goal2.GoalActivities.Add(act3);
            GoalMenu.AddGoal(goal2);
            GoalMenu.AddGoal(new Model.Goal("Relax", status: false));

            ViewGroup goalsLayout = (LinearLayout)FindViewById(Resource.Id.goalsLayout);
            var intent = new Android.Content.Intent(this, typeof(GoalActivity));
            foreach (var goal in GoalMenu.MyGoals)
            {
                GoalMenu.PopulateGoalMenuList(goal, intent, this, goalsLayout);
            }

            Button addButton = (Button)FindViewById(Resource.Id.addButtonOutside);
            addButton.Click += delegate
            {
                var addGoalIntent = new Android.Content.Intent(this, typeof(GoalAddActivity));
                addGoalIntent.PutExtra("GoalsList", JsonConvert.SerializeObject(GoalMenu.MyGoals));
                StartActivityForResult(addGoalIntent, 0);
            };
        }

        public void onActivityResult(int requestCode, Result resultCode, Android.Content.Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (resultCode == Result.Ok)
            {
                var GoalPassedBack = new Model.Goal();
                GoalPassedBack.GoalName = data.GetStringExtra("GoalName");
                GoalPassedBack.GoalDescription = data.GetStringExtra("GoalDescription");
                GoalMenu.AddGoal(GoalPassedBack);
            }
        }
    }
}
