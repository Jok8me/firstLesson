using firstLesson.models;
using firstLesson.resources;
using firstLesson.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace firstLesson.views
{
    public class SearchUserView : View
    {
        public SearchUserView()
        {
            DrawViewBox();
            IDictionary<string, string> inputOptions = InputService.InputOptions(1);
            User? user = null;

            if (!String.IsNullOrEmpty(inputOptions["Login"])){
                for (int i = 0; i < User.users.Count; i++)
                {
                    if (User.users.ElementAt(i).getLogin() == inputOptions["Login"])
                    {
                        user = User.users.ElementAt(i);
                    }
                }

                if (user != null)
                {

                }
                else
                {
                    MessageView messageView = new MessageView("Can't find user '" + inputOptions["Login"] + "'.");
                }

            } 
            else
            {
                throw new Exception(StringResources.WRONG_INPUT);
            }
        }
    }
}
