﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

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
                return _goalStatus;
            }
            set
            {
                _goalStatus = value;
            }
        }

        public Goal(String name = "My goal", String description = "I should describe my goal...", bool status = false)
        {
            _goalName = name;
            _goalDescription = description;
            _goalStatus = status;
        }
        public Goal(SerializationInfo info, StreamingContext context)
        {
            _goalName = info.GetString("GoalName");
            _goalDescription = info.GetString("GoalDescription");
            _goalStatus = info.GetBoolean("GoalStatus");
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("GoalName", _goalName);
            info.AddValue("GoalDescription", _goalDescription);
            info.AddValue("GoalStatus", _goalStatus);
        }
    }
}