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
using System.Collections;
using Java.Lang;
using Newtonsoft.Json;

namespace LifeTime_Android.ViewModel
{
    class ListViewAdapter : BaseAdapter, IListAdapter
    {
        private List<Model.Goal> goalList = new List<Model.Goal>(); //Android.Resource.String
        private Context context;
        public ListViewAdapter(List<Model.Goal> goalList, Context _context)
        {
            this.goalList = goalList;
            this.context = _context;
        }

        public override int Count
        {
            get
            {
                return this.goalList.Count;
            }
        }
        

        public override Java.Lang.Object GetItem(int position)
        {
            //try
            //{
            //    return goalList.ElementAt(position);
            //}
            //catch (System.NullReferenceException e)
            //{

            //    return null;//TODO
            //}
            throw new NotImplementedException();
        }

        public override long GetItemId(int index)
        {
            try
            {
                return 0;
            }
            catch (System.NullReferenceException e)
            {

                return 0;//TODO
            }
        }

        

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;
            if (view == null)
            {
                LayoutInflater inflater = (LayoutInflater)context.GetSystemService(Context.LayoutInflaterService);
                view = inflater.Inflate(Resource.Layout.GoalListElementTemplate, null);
            }

            TextView ListElementText = (TextView)view.FindViewById(Resource.Id.ListViewTextview);
            ListElementText.Text = goalList.ElementAt(position).GoalName;
            ListElementText.Click += (sender, e) => {
                Intent intent = new Intent(context, typeof(GoalActivity));
                intent.PutExtra("GoalPassed", JsonConvert.SerializeObject(goalList.ElementAt(position)));
                context.StartActivity(intent);
            };
            return view;
        }
    }
}