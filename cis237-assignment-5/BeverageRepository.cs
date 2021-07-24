// Author: David Barnes
// Class: CIS 237
// Assignment: 5
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using cis237_assignment_5.Models;

namespace cis237_assignment_5
{
    class BeverageRepository : IBeverageRepository
    {
        // Private Variables
        private DbSet<Beverage> beverages;
        private BeverageContext beverageContext;

        // Constructor.
        public BeverageRepository()
        {
            beverageContext = new BeverageContext();
            beverages = beverageContext.Beverages;
        }

        // ToString override method to convert the collection to a string
        public override string ToString()
        {
            // Create a list to hold all of the printed strings
            string returnString = "";

            // For each item in the collection
            foreach (Beverage beverage in beverages)
            {
                returnString += getBeverageString(beverage) + Environment.NewLine;
            }

            // Return the returnString
            return returnString;
        }

        // Determine if the id passed in is for a valid Beverage
        public bool ItemExists(string id)
        {
            return (FindById(id) != null);
        }

        // Find an item by it's Id
        public string FindById(string id)
        {
            // Get out the beverage being looked for by the id
            Beverage beverageToFind = beverages.Find(id);
            // If it is not null, return the item
            if (beverageToFind != null)
            {
                return getBeverageString(beverageToFind);
            }
            // Not found, return null
            return null;
        }

        // Find an item by it's name
        public string FindByName(string searchName)
        {
            // Get out the beverage being looked for by the name of the item
            Beverage beverageToFind = beverages.Where(b => b.Name.Equals(searchName)).First();
            // If the beverage is not null, return it
            if (beverageToFind != null)
            {
                return getBeverageString(beverageToFind);
            }
            // Else return null
            return null;
        }

        // Add a new item to the collection
        public void AddNewItem(
            string id,
            string name,
            string pack,
            string price,
            string active
        )
        {
            // Make a new beverage
            Beverage newBeverage = new Beverage();

            // Assign the properties that are passed in to the new item
            newBeverage.Id = id;
            newBeverage.Name = name;
            newBeverage.Pack = pack;
            newBeverage.Price = decimal.Parse(price);
            newBeverage.Active = (active == "True");

            // Add the new beverage to the collection of beverages
            beverages.Add(newBeverage);

            // Save the changes to the Entities
            beverageContext.SaveChanges();
        }

        // Update a Beverage by id sending in the input from the UI
        public bool UpdateById(
            string id,
            string name,
            string pack,
            string price,
            string active
        )
        {
            // Find the Beverage to update
            Beverage beverageToUpdate = beverages.Find(id);

            // If the beverage is not null, we can update it.
            if (beverageToUpdate != null)
            {
                //Make sure a value was entered for each field before updating it.
                if (name != "")
                {
                    beverageToUpdate.Name = name;
                }
                if (pack != "")
                {
                    beverageToUpdate.Pack = pack;
                }
                if (price != "")
                {
                    beverageToUpdate.Price = decimal.Parse(price);
                }
                if (active != "")
                {
                    beverageToUpdate.Active = (active == "True");
                }
            }

            // Save the changes and get the number of updates that occured. Should be 1
            int numberOfAltered = beverageContext.SaveChanges();

            // Return the evaluation of making sure the number of altered records is greater than zero.
            return (numberOfAltered > 0);
        }

        // Delete a Beverage by id
        public bool DeleteById(string id)
        {
            // Get the beverage to delete from the Beverages collection
            Beverage beverageToFind = beverages.Find(id);

            // If the beverage is not null we can delete it.
            if (beverageToFind != null)
            {
                // Remove the beverage
                beverages.Remove(beverageToFind);
                // Save the changes and get the number of changes that happened on the save
                int numberDeleted = beverageContext.SaveChanges();
                // Return the evaluation of making sure the number of altered records is more than zero.
                return (numberDeleted > 0);
            }
            // Didn't find an item, return false
            return false;
        }

        //Take the beverage and create a string version of the properties.
        private string getBeverageString(Beverage beverage)
        {
            //Return the formatted string
            return String.Format(
                "{0,6} {1,-55} {2,-15} {3,6} {4,-6}",
                beverage.Id.TrimEnd(),
                beverage.Name.TrimEnd(),
                beverage.Pack.TrimEnd(),
                beverage.Price,
                beverage.Active ? "True" : "False"
            );
        }
    }
}
