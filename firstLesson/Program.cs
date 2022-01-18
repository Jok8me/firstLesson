﻿using firstLesson.models.Users;
using firstLesson.enums;
using firstLesson.views;
using firstLesson.models.Books;
using firstLesson.services.DiscountServices;
using firstLesson.services.BookServices;
using firstLesson.Views;
using firstLesson.services.ConsoleServices;
using DatabaseConnection;
using DatabaseConnection.UsersTableServices;

UserDBService userDBService = new UserDBService();


Console.CursorVisible = false;
MainWindow mainWindow = new MainWindow();

mainWindow.Start();
