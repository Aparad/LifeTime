using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using Newtonsoft.Json;

namespace LifeTime_Android.Model
{
    [Serializable]
    class Goal  : ISerializable
    {
        public List<DailyActivity> GoalActivities = new List<DailyActivity>();
        private String _goalName = String.Empty;
        public String GoalName
        {
            get
            {
                return _goalName;
            }
            set
            {
                if (value.ToString().Length > 100)
                {
                    //TODO messagebox "too long name"...
                }
                _goalName = value;
            }
        }
        private String _goalDescription = String.Empty;
        public String GoalDescription
        {
            get
            {
                return _goalDescription;
            }
            set
            {
                _goalDescription = value;
            }
        }
        private bool _goalStatus = false;
        public bool GoalStatus
        {
            get
            {
                if (GoalActivities.Count != 0)
                {
                    var status = !GoalActivities.Any(x => x.ActivityStatus == false);
                    _goalStatus = status;
                }
                return _goalStatus;
            }
            set
            {
                _goalStatus = value;
            }
        }

        private int _goalProgress;
        public int GoalProgress
        {
            get
            {
                return GoalActivities.Count(x => x.ActivityStatus == true); ;
            }
            set
            {
                _goalProgress = value;
            }
        }

        public Goal(String name = "My goal", String description = "I should describe my goal...", bool status = false, int progress = 0)
        {
            _goalName = name;
            _goalDescription = description;
            _goalStatus = status;
            _goalProgress = progress;
        }
        public Goal(SerializationInfo info, StreamingContext context)
        {
            _goalName = info.GetString("GoalName");
            _goalDescription = info.GetString("GoalDescription");
            _goalStatus = info.GetBoolean("GoalStatus");
            GoalActivities = (List<DailyActivity>)info.GetValue("GoalActivities",(new List<DailyActivity>()).GetType());
            _goalProgress = info.GetInt32("GoalProgress");
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("GoalName", _goalName);
            info.AddValue("GoalDescription", _goalDescription);
            info.AddValue("GoalStatus", _goalStatus);
            info.AddValue("GoalActivities", GoalActivities);
            info.AddValue("GoalProgress", _goalProgress);
        }
    }
}