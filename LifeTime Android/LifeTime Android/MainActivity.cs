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
            SetListViewAdapter();
            SetBindings();
        }

        public void SetListViewAdapter()
        {
            ListView goalsListView = (ListView)FindViewById(Resource.Id.goalsListView);
            ListViewAdapter ListViewAdapter = new ListViewAdapter(goalMenu.MyGoals, _context);
            goalsListView.Adapter = ListViewAdapter;
            ListViewAdapter.NotifyDataSetChanged();
            
        }


        //Adding sample goals and data
        public void InsertTestGoals()
        {
           //Get list to add goals to and create intent
            ViewGroup goalsLayout = (LinearLayout)FindViewById(Resource.Id.goalsLayout);
            ViewGroup goalsListView = (ListView)FindViewById(Resource.Id.goalsListView);
            var GoalActivityIntent = new Android.Content.Intent(this, typeof(GoalActivity));

            //Create sample goals
            Model.Goal goal1 = new Model.Goal("Practice guitar");
            Model.DailyActivity act1 = new Model.DailyActivity("Arpeggios", "Shred some arpeggios!", 1, true);
            goal1.GoalActivities.Add(act1);
            Model.Goal goal2 = new Model.Goal("Study for exams next week");
            Model.DailyActivity act2 = new Model.DailyActivity("Explosions", "BOOMbombombombombom", 1, true);
            Model.DailyActivity act3 = new Model.DailyActivity("Chemistry n' shit", "DO some magic.", 1, false);
            goal2.GoalActivities.Add(act2);
            goal2.GoalActivities.Add(act3);

            //Add goals to a list
            goalMenu.AddGoal(goal1, GoalActivityIntent, this, goalsListView);
            goalMenu.AddGoal(goal2, GoalActivityIntent, this, goalsListView);
            goalMenu.AddGoal(new Model.Goal("Relax", status: false), GoalActivityIntent, this, goalsListView);
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

            Button delButton = (Button)FindViewById(Resource.Id.delButton);
            delButton.Click += (sender, e) =>
            {
                ListView goalsListView = (ListView)FindViewById(Resource.Id.goalsListView);
                var SelectedCount = 0;
                for (int i = 0; i < goalsListView.Count; i++)
                {
                    CheckBox goalCheckbox = (CheckBox)goalsListView.GetChildAt(i).FindViewById(Resource.Id.ListViewCheckbox);
                    if (goalCheckbox.Checked)
                    {
                        SelectedCount++;
                        goalMenu.MyGoals.RemoveAt(i); //TODO Removing by index sucks
                    }
                }
                ListViewAdapter Adapter = (ListViewAdapter)goalsListView.Adapter;
                Adapter.NotifyDataSetChanged();
                string DelMessage = "Deleted " + SelectedCount + " items.";
                Toast.MakeText(_context, DelMessage, ToastLength.Short).Show();
            };
        } 

        //Called back from GoalAddActivity's Add button to add newly created goal to the list (goalsLayout)
        protected override void OnActivityResult(int requestCode, Result resultCode, Android.Content.Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (resultCode == Result.Ok)
            {
                ViewGroup goalsLayout = (LinearLayout)FindViewById(Resource.Id.goalsLayout);
                ViewGroup goalsListView = (ListView)FindViewById(Resource.Id.goalsListView);
                var GoalActivityIntent = new Android.Content.Intent(this, typeof(GoalActivity));
                var PassedGoal = data.GetStringExtra("GoalPassed");
                Model.Goal GoalPassedBack = JsonConvert.DeserializeAnonymousType(PassedGoal, new Model.Goal()); ;
                GoalPassedBack._context = _context;
                goalMenu.AddGoal(GoalPassedBack, GoalActivityIntent, _context, goalsListView);
            }
        }
    }
}
