// Author: David Barnes
// Class: CIS 237
// Assignment: 5
using System;

namespace cis237_assignment_5
{
    interface IBeverageRepository
    {
        bool ItemExists(string id);

        string FindById(string id);

        string FindByName(string name);

        void AddNewItem(string id, string description, string pack, string price, string active);

        bool UpdateById(string id, string name, string pack, string price, string active);

        bool DeleteById(string deleteSearchQuery);
    }
}
