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
    public class AddUserView
    {
        public AddUserView()
        {
            IDictionary<string, string> inputOptions = InputService.InputOptions();
            
            if(!String.IsNullOrEmpty(inputOptions["Login"]) && !String.IsNullOrEmpty(inputOptions["Password"]))
            {
                User.users.Add(new StandardUser(inputOptions["Login"], inputOptions["Password"]));
            }
            else
            {
                throw new Exception(StringResources.WRONG_INPUT);
            }
        }
    }
}
