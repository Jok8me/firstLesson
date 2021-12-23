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

            if (!String.IsNullOrEmpty(inputOptions["Login"])){
                for (int z = 0; z < User.users.Count; z++)
                {

                    if (User.users.ElementAt(z).getLogin().Equals(inputOptions["Login"]))
                    {
                        EditUserView editUserView = new EditUserView(User.users.ElementAt(z));
                    }
                }
                MessageView messageView = new MessageView("Can't find user '" + inputOptions["Login"] + "'.");
            } 
            else
            {
                throw new Exception(StringResources.WRONG_INPUT);
            }
        }
    }
}
