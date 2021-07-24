// Author: David Barnes
// Class: CIS 237
// Assignment: 5
using System;

namespace cis237_assignment_5
{
    class UserInterface
    {
        const int MAX_MENU_CHOICES = 6;

        /*
        |----------------------------------------------------------------------
        | Public Methods
        |----------------------------------------------------------------------
        */

        // Display Welcome Greeting
        public void DisplayWelcomeGreeting()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Welcome to the wine program!");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        // Display Menu And Get Response
        public int DisplayMenuAndGetResponse()
        {
            // Declare variable to hold the selection
            string selection;

            // Display menu, and prompt
            this.DisplayMenu();
            this.DisplayPrompt();

            // Get the selection they enter
            selection = this.GetSelection();

            // While the response is not valid
            while (!this.VerifySelectionIsValid(selection))
            {
                // Display error message
                this.DisplayErrorMessage();

                // Display the prompt again
                this.DisplayPrompt();

                // Get the selection again
                selection = this.GetSelection();
            }
            // Return the selection casted to an integer
            return Int32.Parse(selection);
        }

        // Get the search query from the user
        public string GetSearchQuery()
        {
            Console.WriteLine();
            Console.WriteLine("What would you like to search for?");
            Console.Write("> ");
            return Console.ReadLine();
        }

        // Get the search query from the user
        public string GetUpdateSearchQuery()
        {
            Console.WriteLine();
            Console.WriteLine("What beverage would you like to update (beverage id)?");
            Console.Write("> ");
            return Console.ReadLine();
        }

        // Get New Item Information From The User.
        public string[] GetNewItemInformation()
        {
            string id = this.GetStringField("Id");
            string name = this.GetStringField("Name");
            string pack = this.GetStringField("Pack");
            string price = this.GetDecimalField("Price");
            string active = this.GetBoolField("Active");

            return new string[] { id, name, pack, price, active };
        }

        // Get New Item Information From The User.
        public string[] GetUpdatedItemInformation()
        {
            //Answer string to get input from the user
            string answer;

            //String for each field that might get updated
            string name = "";
            string pack = "";
            string price = "";
            string active = "";

            //*************************Update Item Description***************************
            //See about updating the item's description
            answer = this.GetBoolField("Name", "Do you want to update the item's {0}? (y/n)");
            if (answer == "True")
            {
                name = this.GetStringField("Name", "What is the Item's new {0}?");
            }

            //***********************Update Item Pack****************************
            //See about updating the item's pack
            answer = this.GetBoolField("Pack", "Do you want to update the Item's {0}? (y/n)");
            if (answer == "True")
            {
                pack = this.GetStringField("Pack", "What is the Item's new {0}?");
            }

            //*************************Update the Price****************************
            //See about updating the item's price
            answer = this.GetBoolField("Price", "Do you want to update the Item's {0}? (y/n)");
            if (answer == "True")
            {
                price = this.GetDecimalField("Price", "What is the Item's new {0}?");
            }

            //**********************Update Active Status*****************************
            //See about updating whether the item is active or not
            answer = this.GetBoolField("Active", "Do you want to update the Item's {0} status? (y/n)");
            if (answer == "True")
            {
                active = this.GetBoolField("Active", "Is the Item now {0}? (y/n)");
            }

            //Return a string array containing all of the parts entered from the user
            return new string[] { name, pack, price, active };
        }

        //Get the search query from the user
        public string GetDeleteSearchQuery()
        {
            return this.GetStringField("Id", "What is the {0} of the item you would like to Delete?");
        }

        // Display All Items
        public void DisplayAllItems(string allItemsOutput)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Printing List");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(this.GetItemHeader());
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(allItemsOutput);
        }

        // Display All Items Error
        public void DisplayAllItemsError()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("There are no items in the list to print");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        // Display Item Found Success
        public void DisplayItemFound(string itemInformation)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Item Found!");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(this.GetItemHeader());
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(itemInformation);
        }

        // Display Item Found Error
        public void DisplayItemFoundError()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("A Match was not found");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        // Display Add Wine Item Success
        public void DisplayAddWineItemSuccess()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("The Item was successfully added");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        // Display Item Already Exists Error
        public void DisplayItemAlreadyExistsError()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("An Item With That Id Already Exists");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        // Display Item Update Success
        public void DisplayItemUpdateSuccess()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("The Item was successfully updated");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        // Display Item Update Error
        public void DisplayItemUpdateError()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("The Item could not be updated");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        //Display Item Delete Success
        public void DisplayItemDeleted()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Item Deleted Successfully!");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        //Display Item Delete Error
        public void DisplayItemDeleteError()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Item could not be deleted");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        /*
        |----------------------------------------------------------------------
        | Private Methods
        |----------------------------------------------------------------------
        */

        // Display the Menu
        private void DisplayMenu()
        {
            Console.WriteLine();
            Console.WriteLine("What would you like to do?");
            Console.WriteLine();
            Console.WriteLine("1. Print The Entire List Of Items");
            Console.WriteLine("2. Search For An Item");
            Console.WriteLine("3. Add New Item To The List");
            Console.WriteLine("4. Update Item In The List");
            Console.WriteLine("5. Delete Item From The List");
            Console.WriteLine("6. Exit Program");
        }

        // Display the Prompt
        private void DisplayPrompt()
        {
            Console.WriteLine();
            Console.Write("Enter Your Choice: ");
        }

        // Display the Error Message
        private void DisplayErrorMessage()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("That is not a valid option. Please make a valid choice");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        // Get the selection from the user
        private string GetSelection()
        {
            return Console.ReadLine();
        }

        // Verify that a selection from the main menu is valid
        private bool VerifySelectionIsValid(string selection)
        {
            // Declare a returnValue and set it to false
            bool returnValue = false;

            try
            {
                // Parse the selection into a choice variable
                int choice = Int32.Parse(selection);

                // If the choice is between 0 and the maxMenuChoice
                if (choice > 0 && choice <= MAX_MENU_CHOICES)
                {
                    // Set the return value to true
                    returnValue = true;
                }
            }
            // If the selection is not a valid number, this exception will be thrown
            catch (Exception e)
            {
                // Set return value to false even though it should already be false
                returnValue = false;
            }

            // Return the reutrnValue
            return returnValue;
        }

        // Get a valid string field from the console
        private string GetStringField(string fieldName, string message = "What is the new Item's {0}")
        {
            Console.WriteLine(message, fieldName);
            string value = null;
            bool valid = false;
            while (!valid)
            {
                value = Console.ReadLine();
                if (!String.IsNullOrWhiteSpace(value))
                {
                    valid = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You must provide a value.");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine();
                    Console.WriteLine(message, fieldName);
                    Console.Write("> ");
                }
            }
            return value;
        }

        // Get a valid decimal field from the console
        private string GetDecimalField(string fieldName, string message = "What is the new Item's {0}")
        {
            Console.WriteLine(message, fieldName);
            decimal value = 0;
            bool valid = false;
            while (!valid)
            {
                try
                {
                    value = decimal.Parse(Console.ReadLine());
                    valid = true;
                }
                catch (Exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("That is not a valid Decimal. Please enter a valid Decimal.");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine();
                    Console.WriteLine(message, fieldName);
                    Console.Write("> ");
                }
            }

            return value.ToString();
        }

        // Get a valid bool field from the console
        private string GetBoolField(string fieldName, string message = "Should the Item be {0} (y/n)")
        {
            Console.WriteLine(message, fieldName);
            string input = null;
            bool value = false;
            bool valid = false;
            while (!valid)
            {
                input = Console.ReadLine();
                if (input.ToLower() == "y" || input.ToLower() == "n")
                {
                    valid = true;
                    value = (input.ToLower() == "y");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("That is not a valid Entry.");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine();
                    Console.WriteLine(message, fieldName);
                    Console.Write("> ");
                }
            }

            return value.ToString();
        }

        // Get a string formatted as a header for items
        private string GetItemHeader()
        {
            return String.Format(
                "{0,-6} {1,-55} {2,-15} {3,6} {4,-6}",
                "Id",
                "Name",
                "Pack",
                "Price",
                "Active"
            ) +
            Environment.NewLine +
            String.Format(
                "{0,-6} {1,-55} {2,-15} {3,6} {4,-6}",
                new String('-', 6),
                new String('-', 40),
                new String('-', 15),
                new String('-', 6),
                new String('-', 5)
            );
        }
    }
}
