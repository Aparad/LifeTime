using System;
using System.Runtime.Serialization;

namespace LifeTime_Android.Model
{
    [Serializable]
    class DailyActivity : ISerializable
    {
        private String _activityName = String.Empty;
        private String _activityDescription = String.Empty;
        private String _activityType = "Quantity";
        private float _activityQuant = 0;
        private bool _activityStatus = false;

        public String ActivityName
        {
            get
            {
                return _activityName;
            }
            set
            {
                if (value.ToString().Length > 100)
                {
                    //TODO messagebox "too long name"...
                }
                _activityName = value;
            }
        }

        public String ActivityDescription
        {
            get
            {
                return _activityDescription;
            }
            set
            {
                _activityDescription = value;
            }
        }

        public String ActivityType
        {
            get
            {
                return _activityType;
            }
            set
            {
                _activityType = value;
            }
        }

        public float ActivityQuant
        {
            get
            {
                return _activityQuant;
            }
            set
            {
                if (_activityType != "Quantity")
                {
                    if (value > 24) //24 hours
                    {
                        //TODO messagebox
                    }
                    _activityQuant = value;
                }
                else
                {
                    _activityQuant = value;
                }
            }
        }

        public bool ActivityStatus
        {
            get
            {
                return _activityStatus;
            }
            set
            {
                _activityStatus = value;
            }
        }

        public DailyActivity(String name = "My activity", String description = "I should describe my activity...", float quant = 1, bool status = false, String type = "Quantity")
        {
            _activityName = name;
            _activityDescription = description;
            _activityQuant = quant;
            _activityStatus = status;
            _activityType = type;
        }

        public DailyActivity(SerializationInfo info, StreamingContext context)
        {
            _activityName = info.GetString("ActivityName");
            _activityDescription = info.GetString("ActivityDescription");
            _activityQuant = (float)info.GetDouble("ActivityQuant");
            _activityStatus = info.GetBoolean("ActivityStatus");
            _activityType = info.GetString("ActivityType");
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ActivityName", _activityName);
            info.AddValue("ActivityDescription", _activityDescription);
            info.AddValue("ActivityQuant", _activityQuant);
            info.AddValue("ActivityStatus", _activityStatus);
            info.AddValue("ActivityType", _activityType);
        }
    }
}