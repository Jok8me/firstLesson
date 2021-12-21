using firstLesson.services;

int selectedMenuOption = ConsoleService.MultipleChoice("Login", "Quit");

if(selectedMenuOption == 0){
    firstLesson.views.LoginView.Run();
}
else if(selectedMenuOption == 1){
    Environment.Exit(0);
}
else{
    Environment.Exit(-1);
}
