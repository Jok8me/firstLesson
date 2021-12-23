using firstLesson.models.Users;
using firstLesson.enums;
using firstLesson.views;

User.users.Add(new Admin("admin", "admin", Role.Admin));
User.users.Add(new StandardUser("user", "user"));

MainMenuView mainMenuView = new MainMenuView();
