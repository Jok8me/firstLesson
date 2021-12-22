using firstLesson.models;
using firstLesson.services;
using firstLesson.enums;
using firstLesson.resources;

int selectedMenuOption = ConsoleService.MultipleChoice("Login", "Quit");
User.users.Add(new Admin("admin", "admin", Role.Admin));
User.users.Add(new StandardUser("user", "user"));

if (selectedMenuOption == 0){
    firstLesson.views.LoginView.Run();
}
else if(selectedMenuOption == 1){
    Environment.Exit(0);
}
else{
    throw new Exception(StringResources.WRONG_OPTION_SELECTED);
}
