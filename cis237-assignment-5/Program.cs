// Author: David Barnes
// Class: CIS 237
// Assignment: 5
using System;

namespace cis237_assignment_5
{
    class Program
    {
        static void Main(string[] args)
        {
            // Set Console Window Size
            Console.BufferHeight = Int16.MaxValue - 1;
            Console.WindowHeight = 28;
            Console.WindowWidth = 120;

            // Create an instance of the UserInterface class
            UserInterface userInterface = new UserInterface();

            // Create an instance of the BeverageCollection class
            IBeverageRepository beverageRepository = new BeverageRepository();

            // Display the Welcome Message to the user
            userInterface.DisplayWelcomeGreeting();

            // Display the Menu and get the response. Store the response in the choice integer
            // This is the 'primer' run of displaying and getting.
            int choice = userInterface.DisplayMenuAndGetResponse();

            // While the choice is not exit program
            while (choice != 6)
            {
                switch (choice)
                {
                    case 1:
                        // Print Entire List Of Items
                        string allItemsString = beverageRepository.ToString();
                        if (!String.IsNullOrWhiteSpace(allItemsString))
                        {
                            // Display all of the items
                            userInterface.DisplayAllItems(allItemsString);
                        }
                        else
                        {
                            // Display error message for all items
                            userInterface.DisplayAllItemsError();
                        }
                        break;

                    case 2:
                        // Search For An Item
                        string searchQuery = userInterface.GetSearchQuery();
                        string itemInformation = beverageRepository.FindById(searchQuery);
                        if (itemInformation != null)
                        {
                            userInterface.DisplayItemFound(itemInformation);
                        }
                        else
                        {
                            userInterface.DisplayItemFoundError();
                        }
                        break;

                    case 3:
                        // Add A New Item To The List
                        string[] newItemInformation = userInterface.GetNewItemInformation();
                        if (beverageRepository.FindById(newItemInformation[0]) == null)
                        {
                            beverageRepository.AddNewItem(
                                newItemInformation[0],
                                newItemInformation[1],
                                newItemInformation[2],
                                newItemInformation[3],
                                newItemInformation[4]
                            );
                            userInterface.DisplayAddWineItemSuccess();
                        }
                        else
                        {
                            userInterface.DisplayItemAlreadyExistsError();
                        }
                        break;

                    case 4:
                        //Search For An Item to update
                        string updateSearchQuery = userInterface.GetUpdateSearchQuery();
                        //Check to see if the item we want to update exists in the system
                        bool success = beverageRepository.ItemExists(updateSearchQuery);

                        //If it does exist
                        if (success)
                        {
                            //Get the properties to update
                            string[] updatedProperties = userInterface.GetUpdatedItemInformation();

                            //Update the item and get back a bool as the result
                            bool updateSuccess = beverageRepository.UpdateById(
                                updateSearchQuery,
                                updatedProperties[0],
                                updatedProperties[1],
                                updatedProperties[2],
                                updatedProperties[3]
                            );

                            //If successfull display success, else error
                            if (updateSuccess)
                            {
                                //Display the success message
                                userInterface.DisplayItemUpdateSuccess();
                            }
                            else
                            {
                                //Display the error message
                                userInterface.DisplayItemUpdateError();
                            }
                        }
                        //Item does not exist, obviously can't update
                        else
                        {
                            //Display item not found error message
                            userInterface.DisplayItemFoundError();
                        }
                        break;


                    case 5:

                        //Search For An Item
                        string deleteSearchQuery = userInterface.GetDeleteSearchQuery();
                        //Attempt to delete the item
                        success = beverageRepository.DeleteById(deleteSearchQuery);
                        //If delete succeeded, show success message, else error
                        if (success)
                        {
                            //Display delete success message
                            userInterface.DisplayItemDeleted();
                        }
                        else
                        {
                            //Display delete error message
                            userInterface.DisplayItemDeleteError();
                        }
                        break;
                }

                // Get the new choice of what to do from the user
                choice = userInterface.DisplayMenuAndGetResponse();
            }
        }
    }
}
