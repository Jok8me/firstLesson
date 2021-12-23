using firstLesson.models.Users;
using firstLesson.resources;
using firstLesson.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace firstLesson.views
{
    internal class EditUserView : View
    {
        public EditUserView(int indexOfUser)
        {
            DrawViewBox();
            IDictionary<string, string> inputOptions = InputService.InputOptions();

            if (!String.IsNullOrEmpty(inputOptions["Login"]) && !String.IsNullOrEmpty(inputOptions["Password"]))
            {
                User.users.ElementAt(indexOfUser).UpdateUser(inputOptions["Login"], inputOptions["Password"]);
                MessageView message = new MessageView(inputOptions["Login"] + " correctly updated.");
            } else
            {
                throw new Exception(StringResources.WRONG_INPUT);
            }
        }
    }
}
