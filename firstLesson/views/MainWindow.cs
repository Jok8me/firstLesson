using firstLesson.services.ConsoleServices;
using firstLesson.views;
using firstLesson.Views.BookViews;

namespace firstLesson.Views
{
    public class MainWindow
    {
        public MainMenuView _mainMenuView;
        public LoginVeiw _loginView;
        public AdminPanelVeiw _adminPanelView;
        public MessageView _messageView;

        //User
        public SearchUserView _searchUserView;
        public ModifyUserView _modifyUserView;
        public ManageUsersView _manageUsersView;
        public AddUserView _addUserView;
        public EditUserView _editUserView;

        //Book
        public ManageBooksView _manageBooksView;
        public AddItemView _addBookView;
        public SearchBookView _searchBookView;

        //Configuration
        public AppConfigurationView _appConfigurationView;

        //Drawing
        public ConsoleRectangle _consoleRectangle;

        public MainWindow()
        {
            //Views
            _mainMenuView = new MainMenuView(this);
            _loginView = new LoginVeiw(this);
            _addUserView = new AddUserView(this);
            _adminPanelView = new AdminPanelVeiw(this);
            _manageUsersView = new ManageUsersView(this);
            _searchUserView = new SearchUserView(this);
            _modifyUserView = new ModifyUserView(this);
            _editUserView = new EditUserView(this);
            _messageView = new MessageView(this);
            _manageBooksView = new ManageBooksView(this);
            _addBookView = new AddItemView(this);
            _searchBookView = new SearchBookView(this);
            _appConfigurationView = new AppConfigurationView(this);

            //Drawing
            _consoleRectangle = new ConsoleRectangle(35, 10, new System.Drawing.Point(0,0), ConsoleColor.White);
        }

        public void Start()
        {
            _mainMenuView.Run();
        }
    }
}
