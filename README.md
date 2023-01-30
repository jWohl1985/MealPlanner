# MealPlanner

A Windows desktop application for diet and meal planning. Enter some basic information about yourself and the application will determine how many calories
and macronutrients (protein, carbs, fat) you should eat. You can set your profile up for weight loss, weight maintenance, or bulking.

You can enter food nutrition information and save foods to your Food Database. Construct meal plans for up to 7 different days by adding foods to eat from your database.
The program will add up what you've scheduled to eat and show you how it compares to the goals that have been set.

Technologies used:

1. C#
2. Entity Framework with SQL Server (for storing the user profiles, food information, and meal plans)
3. WPF and XAML for the UI

What I learned:

This is a complex project, I learned quite a bit. I'd say the main take away was really cementing the Model-View-ViewModel pattern in my mind. I know
a lot more about data-binding in WPF than I did when I started this. I'm really proud of the fact that there's no code in the views! All views bind to a 
ViewModel that implements the INotifyPropertyChanged interface for keeping the UI up to date. It was pretty tricky getting everything to update properly 
when certain changes are made. For example, editing the nutrition information for a food requires the Food Database UI to be updated, as well as the meal 
plan UI if that food is being used in any meals, which may also affect the scheduled calories/macronutrients and user goal comparison. This is typically 
handled by one ViewModel subscribing to the PropertyChanged event of another. 

All of the actions such as adding a meal or updating a food bind to Commands on the ViewModel, which implement the ICommand interface. There are
many of the same updating concerns here too, with needing to keep the CanExecute() method up to date.

The data validation is pretty involved as well. I needed to collect quite a bit of complex information from the user, often in the form of a text box.
Using the Model-View-ViewModel pattern, I was able to create error properties on the ViewModels and bind to them. This way, the user can see the errors
update in real time, and the ICommands cannot be executed until the errors are fixed.

Things I'm still working on:

1. Finishing the X-unit testing project
2. Weight log tracking. There is a model and views set up for this but it needs to be cleaned up and implemented
